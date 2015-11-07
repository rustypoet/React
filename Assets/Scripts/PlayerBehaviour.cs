using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	private float mMorality=1f;
	public float moralityMax=10f;
	public float speedPerSecond=2f;
	public bool stopped = true;
	public MeshRenderer beam;

	private void Update () {
		if(stopped) {
			return;
		}
		Vector3 newPos=transform.localPosition;
		newPos.x+=speedPerSecond*Time.deltaTime;
		transform.localPosition=newPos;
	}
	public float Morality {
		get { return mMorality;}
		set { 
			mMorality=value;
			Color color = beam.material.color;
			color.a=mMorality/moralityMax;
			beam.material.color=color;
		}
	}
}