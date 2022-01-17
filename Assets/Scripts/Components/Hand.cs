using UnityEngine;
using DitzelGames.FastIK;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform pole;

    private Player player;

    private Rigidbody rock;

    private bool connected = false;

    private FastIKFabric fastIK;

    private Vector3 defaultLocalEulerAngles;

    private void Start()
    {
        defaultLocalEulerAngles = transform.localEulerAngles;

        player = GetComponentInParent<Player>();
        fastIK = gameObject.AddComponent<FastIKFabric>();

        fastIK.ChainLength = 3;
        fastIK.Pole = pole;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock")) Connect(other.GetComponent<Rock>());
        if (other.CompareTag("Ground")) { GameController.instance.FinishLevel(false); }
    }
    public void Connect(Rock _rock)
    {
        if (player.playerState == Player.States.Holding) return;

        fastIK.enabled = true;

        _rock.GotConnect();
        rock = _rock.GetComponent<Rigidbody>();

        if (_rock.type == Rock.Type.red)
        {
            GameController.instance.Delay(1f, () => 
            {
                player.hitObstacleEvent.Invoke();
            });
        }

        fastIK.SetTarget(_rock.transform);

        player.connectEvent.Invoke(_rock);
        connected = true;
    }
    public void Disable()
    {
        GetComponent<Collider>().enabled = false;

        Destroy(fastIK);

        transform.localEulerAngles = defaultLocalEulerAngles;

        connected = false;
        rock = null;
    }
    public void Release()
    {
        if(rock != null) rock.GetComponent<Rock>().GotDisconnect();
        
        fastIK.Target = null;
        connected = false;
        rock = null;
    }
}
