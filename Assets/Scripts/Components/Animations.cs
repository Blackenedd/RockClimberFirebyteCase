using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animations : MonoBehaviour
{
    private Animator mAnimator;

    private List<OriginalValues> bones = new List<OriginalValues>();

    public void Construct()
    {
        mAnimator = GetComponent<Animator>();

        List<Transform> allBones = transform.GetComponentsInChildren<Transform>().Where(x => x.gameObject != gameObject).ToList();

        allBones.ForEach(x => 
        {
            OriginalValues ov = new OriginalValues();
            ov.bone = x;
            ov.position = x.localPosition;
            ov.rotation = x.localRotation;

            bones.Add(ov);
        });
    }
    public void Idle()
    {
        mAnimator.SetTrigger("Idle");
    }
    public void Dance()
    {
        mAnimator.SetTrigger("Dance");
    }
    public void Flip()
    {
        mAnimator.SetTrigger("Flip");
    }
    public void Disable()
    {
        mAnimator.enabled = false;
    }
    public void FixBones()
    {
        bones.ForEach(x => 
        {
            x.bone.localPosition = x.position;
            x.bone.localRotation = x.rotation;
        });
    }
    public void Enable()
    {
        mAnimator.enabled = true;
    }

    [System.Serializable]
    public class OriginalValues
    {
        public Transform bone;
        public Vector3 position;
        public Quaternion rotation;
    }
}
