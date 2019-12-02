using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpright : MonoBehaviour
{
    new protected Rigidbody rigidbody;
    public bool keepUpright = true;
    public float uprightForce = 10f;
    public float uprightOffset = 1.45f;
    public float additionalUpwardForce = 10f;
    public float dampenAngularForce = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 40;
    }

    // Update is called once per frame
    void Update()
    {
        if(keepUpright)
        {
            rigidbody.AddForceAtPosition(new Vector3(0, (uprightForce + additionalUpwardForce), 0),
                transform.position + transform.TransformPoint(new Vector3(0, uprightOffset, 0)), ForceMode.Force);

            rigidbody.AddForceAtPosition(new Vector3(0, -uprightForce, 0),
                transform.position + transform.TransformPoint(new Vector3(0, -uprightOffset, 0)), ForceMode.Force);

        }

        if(dampenAngularForce > 0)
        {
            rigidbody.angularVelocity *= (1 - Time.deltaTime * dampenAngularForce);
        }
    }
}
