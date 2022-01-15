using UnityEngine;

public class Hand : MonoBehaviour
{
    private Player player;

    private Vector3 targetPosition;
    private Vector3 targetRotation;

    private Transform joint;
    private Transform rock;

    private Vector3 originalLocalPosition;

    private void Start()
    {
        targetPosition = targetRotation = Vector3.zero;
        player = GetComponentInParent<Player>();
        joint = transform.parent;
        originalLocalPosition = transform.localPosition;
    }
    private void LateUpdate()
    {
        if (joint != null && targetPosition != Vector3.zero)
        {
            joint.position = targetPosition;
            joint.eulerAngles = targetRotation;
        }
        if(rock != null)
        {
            transform.position = rock.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock")) Connect(other.GetComponent<Rock>());
    }
    public void Connect(Rock _rock)
    {
        _rock.GotConnect();

        rock = _rock.transform;

        targetPosition = joint.position;
        targetRotation = joint.eulerAngles;

        joint.GetComponent<Rigidbody>().isKinematic = true;

        player.connectEvent.Invoke();

    }
    public void Release()
    {
        targetPosition = targetRotation = Vector3.zero;

        transform.localPosition = originalLocalPosition;

        rock = null;

        joint.GetComponent<Rigidbody>().isKinematic = false;
    }
}
