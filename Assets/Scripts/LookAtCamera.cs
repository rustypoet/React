using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
    public GameObject target;
    Vector3 Distance;
    public float speed;
    // Use this for initialization
    void Start () {
	
	}
	public void CamApproach()
    {
        float k = 0.0f;
        k += 0.02f;
        k *= speed;
        Distance = this.target.transform.position - this.transform.position;
        if ((this.transform.position.x >= this.target.transform.position.x && Distance.z > 10))
        {
            this.transform.Translate(new Vector3(0, -k / 2, k));
            Distance.z -= k;
        }
    }
    // Update is called once per frame
    void Update () {
        float h = 0.0f;
        h += 0.02f; 
        h *= speed;
       if(transform.position.x < target.transform.position.x)
             transform.Translate(new Vector3(h, 0, 0));
        else
            CamApproach();
    }
}
