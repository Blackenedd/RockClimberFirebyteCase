using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Type type = Type.normal;
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
    public void GotDisconnect()
    {
        GetComponent<Renderer>().enabled = false;

        Transform pt = Instantiate(Resources.Load<GameObject>("particles/explosion")).transform;
        pt.position = transform.position;
    }
    public enum Type
    {
        normal,
        red,
        green
    }
}
