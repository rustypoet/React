using UnityEngine;
using System.Collections;

public class PreloadTile : MonoBehaviour {
	private float mTileOffset;
	private int mProgress=0;
	private GameObject lastTile;
	private GameObject curTile;
	private GameObject nextTile;
	public Camera movingCam;
	public Texture[] tileTextures;

	private void Start () {
		lastTile = transform.GetChild(0).gameObject;
		curTile = transform.GetChild(1).gameObject;
		nextTile = transform.GetChild(2).gameObject;
		mTileOffset=lastTile.transform.localScale.x*10;
	}
	
	private void FixedUpdate() {
		if(movingCam.transform.position.x>=curTile.transform.position.x)
		{
			GameObject temp = lastTile;
			lastTile=curTile;
			curTile=nextTile;
			nextTile=temp;
			Vector3 nextTilePos = nextTile.transform.localPosition;
			nextTilePos.x+=mTileOffset*3;
			nextTile.transform.localPosition=nextTilePos;
			mProgress+=1;
			Debug.Log (nextTilePos);
		}
	}
}
