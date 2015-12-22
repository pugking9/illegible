using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    private float yaw = 0.0f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        yaw += 0.2f;

        transform.eulerAngles = new Vector3(0, yaw, 0);
	}
}
