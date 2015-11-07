using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	private Vector3 mMoveTo;
	private Vector3 mStartFrom;
	private Vector3 mNormalPosition=new Vector3();
	private Vector3 mClosePosition=new Vector3();
	private float mMovingTime=0f;
	private bool moving=false;
	public float yBias=1f;
	public float approachTime=2f;
	public float normalApproachTime=1f;
	private void Start()
	{
		mNormalPosition=transform.localPosition;
		Transform farPos=transform.parent.GetChild(0);
		transform.localPosition=farPos.localPosition;//start far away, see whole background
	}
	private void Update() {
		if(moving) {
			mMovingTime+=Time.deltaTime;
			float ratio=Mathf.Clamp(mMovingTime/approachTime, 0f, 1f);
			Vector3 point=Vector3.Lerp(mStartFrom, mMoveTo, ratio);
			point.y+=Mathf.Sin(ratio*Mathf.PI*yBias);
			transform.localPosition=point;
			if(mMovingTime>=approachTime) {
				moving=false;
				mMovingTime=0f;
				approachTime=normalApproachTime;
			}
		}

	}
	public void CamApproach()
	{
		moving=true;
		mStartFrom=transform.localPosition;
		mMoveTo=mClosePosition;
	}
	public void CamGetBack()
	{
		moving=true;
		mStartFrom=transform.localPosition;
		mMoveTo=mNormalPosition;
	}
}
