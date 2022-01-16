using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Ragdoll ragdoll;
    private Animations animations;

    private List<Hand> hands;

    [HideInInspector] public States playerState = States.Idle;
    [HideInInspector] public UnityEvent connectEvent = new UnityEvent();

    private void Awake()
    {
        ragdoll = gameObject.AddComponent<Ragdoll>();
        animations = gameObject.AddComponent<Animations>();
        hands = GetComponentsInChildren<Hand>().ToList();

        ragdoll.Construct(); 
        ragdoll.Disable();
        
        animations.Construct();
        animations.Idle();
    }
    private void Start()
    {
        GameController.instance.startEvent.AddListener(() =>
        {
            CameraController.instance.SetOffset(0, 2, 0);
            CameraController.instance.SetValues(-7.5f, 0, 0);
            InputController.instance.hitEvent.AddListener(WhenPlayersHitScreen);
        });
        connectEvent.AddListener(WhenConnected);
    }
    private void WhenConnected() 
    {
        playerState = States.Holding;
    }
    private void WhenPlayersHitScreen(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Rock") && playerState != States.Launching)
        {
            hands.ForEach(x => x.Release());

            Vector3 pos = hit.collider.transform.position;

            animations.Disable();
            ragdoll.Enable();

            if (GameController.instance.settings.Mechanic == GameSettings.Mechanics.Launching)
            {
                ragdoll.LaunchRagdoll(pos);
            }
            else
            {
                ragdoll.LerpRagdoll(pos);
            }

            playerState = States.Launching;

        }
    }
    [System.Serializable]
    public enum States
    {
        Idle,
        Launching,
        Holding
    }
}
