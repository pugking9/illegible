using UnityEngine;
using System.Collections;

public class RigifbodyWalker : MonoBehaviour {

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public Camera playerCam;
    public int sensitivity = 10;

    private bool grounded = false;
    private float pitch = 0.0f;
    private float yaw = 0.0f;



    void Awake()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    void FixedUpdate()
    {
        yaw += (Input.GetAxis("Mouse X") * (sensitivity / 10));
        pitch -= (Input.GetAxis("Mouse Y") * (sensitivity / 10));

        pitch = Mathf.Clamp(pitch, -90, 90);

        transform.eulerAngles = new Vector3(0, yaw, 0);
        playerCam.transform.eulerAngles = new Vector3(pitch, yaw, 0);

        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

        if (grounded)
        {
            if (canJump && Input.GetButton("Jump"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
