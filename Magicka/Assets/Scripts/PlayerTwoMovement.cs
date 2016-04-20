using UnityEngine;
using System.Collections;

public class PlayerTwoMovement : MonoBehaviour
{
    public float speed = 10f;

    Vector3 movement;              //Vector to store direction of player movement
    Animator anim;                 //Reference to animator
    Rigidbody playerRigidbody;     //Reference to player rigidbody
    float camRayLength = 100f;     //Length of ray from camera into the scene

	/// <summary>
	CharacterController cc;
	/// </summary>

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
		cc = GetComponent<CharacterController> ();
    }

    // FixedUpdate fires each physics update
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("LeftStickHorizontal2");
        float v = Input.GetAxisRaw("LeftStickVertical2");
        float rH = Input.GetAxisRaw("RightStickHorizontal2");
        float rV = Input.GetAxisRaw("RightStickVertical2");

        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        cc.Move(movement);
        float angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;

        float rotAngle = Mathf.Atan2(rH, rV) * Mathf.Rad2Deg;
        if (rH == 0 && rV == 0)
        {
            cc.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if (rH != 0 && rV != 0)
            cc.transform.rotation = Quaternion.Euler(0, -rotAngle + 90, 0);
    }

};