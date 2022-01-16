using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Collider mCollider;
    private Rigidbody mRigidbody;
    private void Awake()
    {
        mCollider = GetComponent<Collider>();
        if (GetComponent<Rigidbody>() == null)
        {
            mRigidbody = gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            mRigidbody = GetComponent<Rigidbody>();
        }

        mRigidbody.isKinematic = true;
    }
    public void GotConnect()
    {
        mCollider.enabled = false;
    }
}
