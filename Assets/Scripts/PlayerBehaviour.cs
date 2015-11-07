using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	private float mMorality=5f;
	private Color cStartColor=new Color(0f, 0.2f, 0f , 0f);
	private Color cEndColor = new Color(1f, 1f, 1f, 1f);
	private Color mCachedColor=new Color();
	private float mCachedStartSpeed;
	private float mCachedStartPitch;
	private Vector3 cachedPos;
	public float moralityMax=20f;
	public float speedPerSecond=2f;
	public float speedMax=10f;
	public bool stopped=false;
	public AudioSource audioSource;
	public MeshRenderer beam;

	private void Start()
	{
		mCachedStartSpeed=speedPerSecond;
		mCachedStartPitch=audioSource.pitch;
	}
	private void Update () {
		if(stopped) {
			return;
		}
		cachedPos=transform.localPosition;
		cachedPos.x+=speedPerSecond*Time.deltaTime;
		transform.localPosition=cachedPos;
	}
	public void SetMorality(float value)
	{
		mMorality=Mathf.Clamp(value, 0f, moralityMax);
		mCachedColor=Color.Lerp(cStartColor, cEndColor, mMorality/moralityMax);
		beam.material.SetColor ("_TintColor", mCachedColor);
		Debug.Log (mMorality);
	}
	public void SetSpeed(float value)
	{
		speedPerSecond=Mathf.Clamp(value, 0f, speedMax);
		audioSource.pitch=Mathf.Clamp(mCachedStartPitch*speedPerSecond/mCachedStartSpeed, 0f, mCachedStartPitch);
	}
	public void IncreaseMorality(float value=1f)
	{
		SetMorality(mMorality+value);
	}
	public void DecreaseMorality(float value=1f)
	{
		SetMorality (mMorality-value);
	}
	public void DecreaseHealth(float value=1f)
	{
		SetSpeed (speedPerSecond-value);
	}
	public void IncreaseHealth(float value=1f)
	{
		SetSpeed (speedPerSecond+value);
	}
}