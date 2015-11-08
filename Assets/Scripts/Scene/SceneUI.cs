using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneUI : MonoBehaviour {
	private Image mBack=null;
	private GameObject mParentUI;
	private GameObject mCachedSceneUI;
	private Color mCachedColor;
	private float mStartAlpha;
	private PlayerBehaviour mPlayer=null;
	private SceneChoice mActiveChoice=null;
	private CallbackDelegate mCallback=null;
	public float minAlpha=0.2f;
	public float warpTime=1.5f;
	public Image imageToSet;
	public SceneChoice defaultWalkChoice;

	private void Start () {
		mBack= transform.GetChild(0).gameObject.GetComponent<Image>();
		mStartAlpha=mBack.color.a;
		mParentUI=transform.GetChild (1).gameObject;
		mPlayer=PlayerBehaviour.Singleton();
		imageToSet.material.EnableKeyword("_EMISSION");
	}

	public void StartScene (string sceneName, CallbackDelegate dele) {
		mBack.gameObject.SetActive(true);
		mCachedColor=mBack.color;
		imageToSet.material.SetTexture("_MainTex", 
		                               mParentUI.transform.Find(sceneName).gameObject.GetComponent<SceneData>().sceneImage);
		Lerper.RegisterLerper(SetBackAlpha, mStartAlpha, minAlpha, warpTime, StartChoice);
		mCachedSceneUI=mParentUI.transform.Find (sceneName).gameObject;
		mCallback=dele;
	}
	public void SetBackAlpha(float alpha)
	{
		mCachedColor.a=alpha;
		mBack.color=mCachedColor;
	}
	public void StartChoice()
	{
		imageToSet.gameObject.SetActive(true);
		mCachedSceneUI.SetActive(true);
		StartCoroutine(TimerChoice(mPlayer.stressSpeed));
	}
	IEnumerator TimerChoice(float seconds) {
		yield return new WaitForSeconds(seconds);
		ChoiceDone(defaultWalkChoice);
	}
	public void ChoiceDone(SceneChoice who)
	{
		if(who==null) {
			ChoiceDoneEnd();
			return;
		}
		Lerper.RegisterLerper(SetBackAlpha, minAlpha, mStartAlpha, warpTime, ChoiceDoneEnd);
		if(who.sound!=null) {
			who.sound.gameObject.SetActive(true);
		}
		mActiveChoice=who;
	}
	public void ChoiceDoneEnd()
	{
		mBack.gameObject.SetActive(false);
		mCachedSceneUI.SetActive(false);
		mCachedSceneUI=null;
		if(mActiveChoice.sound!=null) {
			mActiveChoice.sound.gameObject.SetActive(false);
		}
		mPlayer.IncreaseStress(mActiveChoice.deltaStress);
		mPlayer.IncreaseHealth(mActiveChoice.deltaHealth);
		mPlayer.IncreaseMorality(mActiveChoice.deltaMorality);
		mActiveChoice=null;
		if(null!=mCallback) {
			mCallback();
		}
		mCallback=null;
	}
}
