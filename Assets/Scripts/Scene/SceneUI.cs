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
	private bool mChoiceDone=true;
	public float minAlpha=0.2f;
	public float warpTime=3f;
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
		imageToSet.overrideSprite=mParentUI.transform.Find(sceneName).gameObject.GetComponent<SceneData>().sceneImage;
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
		StartCoroutine(TimerChoice(mPlayer.stressSpeed, DefaultChoice));
		mChoiceDone=false;
	}
	IEnumerator TimerChoice(float seconds, CallbackDelegate cb) {
		yield return new WaitForSeconds(seconds);
		cb();
	}
	public void DefaultChoice()
	{
		if(!mChoiceDone) {
			ChoiceDone(defaultWalkChoice);
		}
	}
	public void ChoiceDone(SceneChoice who)
	{
		mChoiceDone=true;
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
	public void ChoiceText()
	{

	}
	public void ChoiceDoneEnd()
	{
		mBack.gameObject.SetActive(false);
		if(mCachedSceneUI!=null) {
			mCachedSceneUI.SetActive(false);
		}
		mCachedSceneUI=null;
		if(mActiveChoice!=null) {
			mActiveChoice.sound.gameObject.SetActive(false);
			mPlayer.DeltaStress(mActiveChoice.deltaStress);
			mPlayer.DeltaHealth(mActiveChoice.deltaHealth);
			mPlayer.DeltaMorality(mActiveChoice.deltaMorality);
			if(mActiveChoice.choiceText!="") {
				StartCoroutine(TimerChoice(mActiveChoice.choiceTextDelay, DefaultChoice));
			}
		}
		mActiveChoice=null;
		if(null!=mCallback) {
			mCallback();
		}
		mCallback=null;
	}
	public void TextOnBlackScreen(string text) {
		mBack.transform.GetChild(0).gameObject.SetActive(false);
		mBack.gameObject.SetActive(true);
		Lerper.RegisterLerper(SetBackAlpha, minAlpha, 1, warpTime, RestartGame);

	}
	public void RestartGame() {
		int level=Application.loadedLevel;
		Application.UnloadLevel(Application.loadedLevel);
		Application.LoadLevel (level);
	}
}
