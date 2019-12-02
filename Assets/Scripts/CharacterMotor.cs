using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public Animator modelAnim;
    public GameObject stringy;
    public Transform headTr;

    public float maxVelocity;
    public float force;
    public Rigidbody rigidbodyMover;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    private Rigidbody testRb;
    private Vector3 movementVector;
    private Transform cameraT;

    private bool falling;
    private void Start()
    {
        testRb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            modelAnim.SetTrigger("Jab");
        else if (Input.GetMouseButtonDown(1))
            modelAnim.SetTrigger("Hook");
        else if (Input.GetKeyDown(KeyCode.Space))
            modelAnim.SetTrigger("Block");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (falling)
            return;
        movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (movementVector != Vector3.zero && movementVector.z >= 0)
        {
            float targetRotation = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            modelAnim.SetBool("Run", true);
        }
        else if (movementVector.z < 0)
        {
            modelAnim.SetBool("Run", true);
            transform.LookAt(cameraT.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            modelAnim.SetBool("Run", false);
            rigidbodyMover.velocity = Vector3.zero;
        }

        if (movementVector != Vector3.zero)
        {
            if (rigidbodyMover.velocity.magnitude < maxVelocity)
                //rigidbodyMover.AddForce(transform.forward * force);
                rigidbodyMover.velocity = transform.forward * force;
        }
        //else
        //    Debug.Log("Reached max velocity");
        //transform.forward = testRb.velocity.normalized;
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        //if(movementVector != Vector2.zero)
        //    transform.forward = movementVector;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            stringy.SetActive(false);
            modelAnim.SetBool("Fall", true);
            modelAnim.SetBool("Run", false);
            rigidbodyMover.AddTorque(Vector3.forward * 10000);
            falling = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            stringy.transform.position = headTr.position;
            stringy.SetActive(true);
            modelAnim.SetBool("Fall", false);
            falling = false;
        }
    }
}
