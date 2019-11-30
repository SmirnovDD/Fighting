using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointFollowAnimRot : MonoBehaviour
{
    public bool invert;

    public float torqueForce;
    public float angularDamping;
    public float maxForce;
    public float springForce;
    public float springDamping;

    public Vector3 targetVel;

    public Transform target;

    private GameObject limb;
    private JointDrive drive;
    private SoftJointLimitSpring spring;
    private ConfigurableJoint joint;
    private Quaternion startingRotation;
    // Start is called before the first frame update
    void Start()
    {
        invert = false;

        torqueForce = 500f;
        angularDamping = 0f;
        maxForce = 500f;

        springForce = 0f;
        springDamping = 0f;

        targetVel = Vector3.zero;

        drive.positionSpring = torqueForce;
        drive.positionDamper = angularDamping;
        drive.maximumForce = maxForce;

        spring.spring = springForce;
        spring.damper = springDamping;

        joint = GetComponent<ConfigurableJoint>();

        joint.slerpDrive = drive;

        joint.linearLimitSpring = spring;
        joint.rotationDriveMode = RotationDriveMode.Slerp;
        joint.projectionMode = JointProjectionMode.None;
        joint.targetAngularVelocity = targetVel;
        joint.configuredInWorldSpace = false;
        joint.swapBodies = true;

        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;

        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        startingRotation = Quaternion.Inverse(target.localRotation);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (invert)
            joint.targetRotation = Quaternion.Inverse(target.localRotation * startingRotation);
        else
            joint.targetRotation = target.localRotation * startingRotation;
    }
}
