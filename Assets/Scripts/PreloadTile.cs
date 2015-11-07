using UnityEngine;
using System.Collections.Generic;

public class PreloadTile : MonoBehaviour {
	private float mTileOffset;
	private float mTileLength;
	private float mCachedCenterPosition;
	private int mProgress=0;
	private List<GameObject> tiles=new List<GameObject>();
	public Camera movingCam;
	public Texture[] tileTextures;
	public int tilesNumberOn=2;

	private void Start () {
		int i=1;
		mTileLength=(float)tileTextures[0].width/10f;
		GameObject tileTemplate=transform.GetChild(0).gameObject;
		SetTileTexture (tileTemplate, tileTextures[0]);
		tiles.Add (tileTemplate);
		Vector3 tempPos=new Vector3();
		tempPos=tileTemplate.transform.localPosition;
		for( ; i<tilesNumberOn; ++i) {
			GameObject newTile=Instantiate(tileTemplate);
			newTile.transform.SetParent(tileTemplate.transform.parent, false);
			tempPos.x+=mTileLength;
			newTile.transform.localPosition=tempPos;
			SetTileTexture(newTile, tileTextures[i]);
		}
		mTileOffset=(float)tilesNumberOn*mTileLength;
		mCachedCenterPosition=tileTemplate.transform.position.x+mTileOffset/2;
		mProgress=tilesNumberOn;
		if(tilesNumberOn>=tileTextures.Length) {
			mCachedCenterPosition=99999999f;
		}
	}
	private void SetTileTexture(GameObject tile, Texture tex)
	{
		tile.GetComponent<MeshRenderer>().material.mainTexture=tex;
	}
	private void FixedUpdate() {
		if(movingCam.transform.position.x>=mCachedCenterPosition && mProgress<tileTextures.Length)
		{
			GameObject temp = tiles[0];
			tiles.RemoveAt(0);
			tiles.Add (temp);
			Vector3 nextTilePos = temp.transform.localPosition;
			nextTilePos.x+=mTileOffset;
			temp.transform.localPosition=nextTilePos;
			SetTileTexture(temp, tileTextures[mProgress]);
			mProgress+=1;
			mCachedCenterPosition+=mTileLength;
		}
	}
}
