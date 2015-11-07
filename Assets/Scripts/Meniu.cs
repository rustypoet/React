using UnityEngine;
using System.Collections;

public class Meniu : MonoBehaviour
{
    public PlayerBehaviour player;
	public CameraMove movingCam;
	public GameObject audioStart;
	public GameObject audioGame;
	private void Start()
	{
		movingCam.CamApproach();
	}
	public void StartGame()
	{
 		Canvas canva=player.GetComponentInChildren<Canvas>();
		canva.gameObject.SetActive(false);
		movingCam.CamGetBack();
		player.stopped = false;
		audioStart.SetActive(false);
		audioGame.SetActive(true);
	}
	public void ShowScene(GameObject scene)
	{
		movingCam.CamApproach();
	}
 }
