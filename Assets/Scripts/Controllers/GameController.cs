using UnityEngine.SceneManagement;
using UnityEngine;

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

    #region LevelOperations
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
}
