using UnityEngine;
using System.Collections;

public class ConstantMove : MonoBehaviour {

	public float speedPerSecond=2f;
	public bool stopped=false;
	private void Start () {
	
	}
	
	private void Update () {
		if(stopped) {
			return;
		}
		Vector3 newPos=transform.localPosition;
		newPos.x+=speedPerSecond*Time.deltaTime;
		transform.localPosition=newPos;
	}
}
