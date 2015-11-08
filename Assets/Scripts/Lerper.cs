using UnityEngine;
using System.Collections.Generic;

public delegate void CallbackDelegate();
public delegate void LerpDelegate(float value);

public class LerpInfo {
	public LerpDelegate lerpF;
	public float lerpMin;
	public float lerpMax;
	public float lerpTime;
	public float lerp;
	public CallbackDelegate lerpCB;
	public LerpInfo(LerpDelegate lerpF,
	                float min, float max, float time, CallbackDelegate cb=null) {
		this.lerpF=lerpF;
		this.lerpMin=min;
		this.lerpMax=max;
		this.lerpTime=time;
		this.lerp=0f;
		this.lerpCB=cb;
	}
}

public class Lerper: MonoBehaviour {
	private static Lerper mInstance=null;
	private static List<int> mRemovals=new List<int>();
	private static List<LerpInfo> mLerpers=new List<LerpInfo>();
	public Lerper()
	{
		if(mInstance!=null) {
			Destroy(this);
		}
		mInstance=this;
	}

	public static void RegisterLerper (LerpDelegate lerpF, float min, float max, float time, CallbackDelegate cb=null) {
		mLerpers.Add(new LerpInfo(lerpF, min, max, time, cb));
	}

	public void Update () {
		if(mLerpers.Count>0) {
			float lerp=0f;
			for(int i=0; i<mLerpers.Count; ++i) {
				LerpInfo ler=mLerpers[i];
				ler.lerp+=Time.deltaTime*ler.lerpTime;
				lerp=Mathf.Lerp(ler.lerpMin, ler.lerpMax, 
				                ler.lerp);
				ler.lerpF(lerp);
				if(ler.lerp>=ler.lerpTime) {
					mRemovals.Insert(0, i);
					if(ler.lerpCB!=null) {
						ler.lerpCB();
					}
				}
			}
			if(mRemovals.Count>0) {
				foreach(int iLer in mRemovals ) {
					mLerpers.RemoveAt(iLer);
				}
				mRemovals.Clear();
			}
		}
	}
}
