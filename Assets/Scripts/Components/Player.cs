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
    [HideInInspector] public ConnectRockEvent connectEvent = new ConnectRockEvent();

    private void Awake()
    {
        ragdoll = gameObject.AddComponent<Ragdoll>();
        animations = gameObject.AddComponent<Animations>();
        hands = GetComponentsInChildren<Hand>().ToList();
        ragdoll.Construct();
        ragdoll.Disable();

        animations.Construct();

        animations.Enable();
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
        GameController.instance.finishEvent.AddListener((bool win) => 
        {
            WhenWin();
        });
        connectEvent.AddListener(WhenConnected);
    }
    private void WhenConnected(Rock rock)
    {
        if (playerState == States.Win)
        {
            hands.ForEach(x => x.Disable());
            return;
        }

        ragdoll.ConnectRagdoll(rock.GetComponent<Rigidbody>());

        playerState = States.Holding;
    }
    private void WhenWin()
    {
        if (playerState == States.Win) return;
        playerState = States.Win;

        ResetPivot();

        hands.ForEach(x => x.Disable());
        ragdoll.Disable(() =>
        {
            animations.FixBones();
            animations.Enable();
            animations.Flip();

            animations.FixBones();

            Transform flipTarget = Level.instance.finish.playerFinishPosition;

            transform.DOMoveX(flipTarget.position.x, 1f);
            transform.DOMoveZ(flipTarget.position.z, 1f);

            transform.DOMoveY(flipTarget.position.y + 1f, 0.8f).OnComplete(() => transform.DOMoveY(flipTarget.position.y, 0.5f).OnComplete(() =>
            {
                animations.FixBones();
                animations.Dance();
            }));
        });
    }
    private void ResetPivot()
    {
        Transform hip = GetComponentInChildren<Rigidbody>().transform;
        transform.position = hip.position;
        hip.localPosition = Vector3.zero;
    }
    private void WhenPlayersHitScreen(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Rock") && (playerState != States.Launching && playerState != States.Win))
        {
            ragdoll.DisconnectRagdoll();

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
        Holding,
        Win
    }
    public class ConnectRockEvent : UnityEvent<Rock> { }
}
