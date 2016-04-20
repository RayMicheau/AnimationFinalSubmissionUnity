using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    Vector3 movement;              //Vector to store direction of player movement
    Animator anim;                 //Reference to animator
    float m_LastAngleReceived = 100;

	/// <summary>
	CharacterController cc;
	/// </summary>

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        //playerRigidbody = GetComponent<Rigidbody>();
		cc = GetComponent<CharacterController> ();
    }

    void Update()
    {
       
    }
    // FixedUpdate fires each physics update
    void FixedUpdate()
    {
        
        float h = Input.GetAxisRaw("LeftStickHorizontal");
        float v = Input.GetAxisRaw("LeftStickVertical");

        float rH = Input.GetAxisRaw("RightStickHorizontal");
        float rV = Input.GetAxisRaw("RightStickVertical");

        movement.Set(h, 0f, v);
		float n = Mathf.Sqrt (h * h + v * v);
		anim.SetFloat ("Speed", n);

        movement = movement.normalized * speed * Time.deltaTime;
        cc.Move(movement);
        float angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;

        float rotAngle = Mathf.Atan2(rH, rV) * Mathf.Rad2Deg;
        if (rH == 0 && rV == 0) { 
            cc.transform.rotation = Quaternion.Euler(0, angle, 0);
        } else if(rH != 0 && rV != 0)
            cc.transform.rotation = Quaternion.Euler(0, -rotAngle + 90, 0);

        // Quaternion.Euler(0, -angle + 90, 0);
       // m_LastAngleReceived = rotAngle;
    }
};