using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            GetDependencies();
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    #endregion

    public int level;
    public GameSettings settings;

    [HideInInspector] public UnityEvent startEvent = new UnityEvent();
    [HideInInspector] public GameFinishEvent finishEvent = new GameFinishEvent();

    private bool finished;

    #region LevelOperations
    public void StartLevel()
    {
        startEvent.Invoke();
    }
    public void FinishLevel(bool completed = true) 
    {
        if (finished) return; finished = true;
        finishEvent.Invoke(completed);
    }
    public void GetDependencies()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || level == -1) level = PlayerPrefs.GetInt("level");
    }
    public void LevelUp()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
    }
    #endregion

    #region SceneOperations
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion

    public class GameFinishEvent : UnityEvent<bool> { }
}
