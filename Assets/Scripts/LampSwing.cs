using UnityEngine;
using System.Collections;

public class LampSwing : MonoBehaviour {
    Rigidbody rb;
    int I = 0;
    public float Vel;
    public float Peak;
    public bool peaked = false;
	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 0, 100 - rb.velocity.z);
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        I++;
        Vel = (rb.velocity.z * 10);
        if (Vel > Peak)
            Peak = Vel;
        else if(peaked == false)
        {
            peaked = true;
            if (Peak >= 1 && Peak < 3)
            {
                rb.AddForce(0, 0, 100 - Peak);
            }
            Peak = 0;
        }

        if(Vel < 0)
        {
            peaked = false;
        }
	}
}
