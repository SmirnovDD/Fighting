using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float maxVelocity;
    public float force;
    public Rigidbody rigidbodyMover;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    private Rigidbody testRb;
    private Vector3 movementVector;
    private Transform cameraT;
    private void Start()
    {
        testRb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * force;
        if (movementVector != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        transform.Translate(movementVector * Time.fixedDeltaTime * force, Space.Self);
        //rigidbodyMover.AddForce(movementVector);
        //transform.forward = testRb.velocity.normalized;
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        //if(movementVector != Vector2.zero)
        //    transform.forward = movementVector;
    }
}
