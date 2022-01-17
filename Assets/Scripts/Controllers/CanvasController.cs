using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CanvasController : MonoBehaviour
{
    #region Singleton
    public static CanvasController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [Header("Panels")]
    [SerializeField] private CanvasGroup startPanel;
    [SerializeField] private CanvasGroup finishPanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button okayButton;

    [SerializeField] private GameObject success;
    [SerializeField] private GameObject fail;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }
    public void OnStartButtonClicked()
    {
        ClosePanel(startPanel,() => 
        {
            startPanel.gameObject.SetActive(false);
            GameController.instance.StartLevel();
        });
    }
    public void ClosePanel(CanvasGroup panel,UnityAction onComplete = null)
    {
        panel.DOFade(0, 0.5f).OnComplete(() => onComplete?.Invoke());
    }

    public void OnGameFinish(bool success)
    {

    }
}
