using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform targetTr;

    void Update()
    {
        transform.localPosition = targetTr.localPosition;
        transform.localRotation = targetTr.localRotation;
    }
}
