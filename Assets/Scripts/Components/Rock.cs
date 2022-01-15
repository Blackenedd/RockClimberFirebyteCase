using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Collider mCollider;
    private void Awake()
    {
        mCollider = GetComponent<Collider>();
    }
    public void GotConnect()
    {
        mCollider.enabled = false;
    }
}
