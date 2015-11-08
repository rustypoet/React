using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		Meniu.Singleton().StartScene(transform.parent.gameObject);
	}
}
