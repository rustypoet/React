using UnityEngine;
using System.Collections;

public class Meniu : MonoBehaviour
{
	private const float cLowVolume=0.25f;
	private const float cNormalVolume=0.85f;
	private static Meniu mInstance=null;
	private GameObject mStartedScene=null;
	public PlayerBehaviour player;
	public CameraMove movingCam;
	public AudioSource audioStart;
	public AudioSource audioGame;
	public SceneUI scenesGUI;

	Meniu()
	{
		if(mInstance!=null) {
			Destroy(this);
		}
		mInstance=this;
	}
	private void Start()
	{
		player.Stop ();
		movingCam.CamApproach();
		Screen.autorotateToLandscapeLeft=true;
	}
	public static Meniu Singleton()
	{
		return mInstance;
	}
	public void StartGame()
	{
 		Canvas canva=player.GetComponentInChildren<Canvas>();
		canva.gameObject.SetActive(false);
		movingCam.CamGetBack();
		player.Continue();
		audioStart.gameObject.SetActive(false);
		SetVolume(cNormalVolume);
		audioGame.gameObject.SetActive(true);
	}
	public void StartScene(GameObject scene) {
		player.Stop();
		mStartedScene=scene;
		movingCam.CamApproach(StartChoice);
		Lerper.RegisterLerper(SetVolume, cNormalVolume, cLowVolume, movingCam.approachTime);
	}
	public void StartChoice() {
		SetVolume(cLowVolume);
		scenesGUI.StartScene(mStartedScene.name, ChoiceTaken);
	}
	public void ChoiceTaken() {
		Lerper.RegisterLerper(SetVolume, cLowVolume, cNormalVolume, movingCam.approachTime);
		movingCam.CamGetBack(player.Continue);
	}
	public void SetVolume(float volume)
	{
		audioGame.volume=volume;
	}
 }
