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
    [SerializeField] private CanvasGroup tutorialPanel;

    [Header("Buttons")]
    [SerializeField] private Button startButton;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button okayButton;

    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failPanel;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        GameController.instance.finishEvent.AddListener(OnGameFinish);
    }
    public void OnStartButtonClicked()
    {
        ClosePanel(startPanel,() => 
        {
            startPanel.gameObject.SetActive(false);
            GameController.instance.StartLevel();

            tutorialPanel.gameObject.SetActive(true);
            tutorialPanel.DOFade(1f,0.3f).OnComplete(() => 
            {
                tutorialPanel.GetComponent<Button>().onClick.AddListener(() => 
                {
                    tutorialPanel.GetComponent<Button>().onClick.RemoveAllListeners();
                    tutorialPanel.DOFade(0, 0.3f).OnComplete(() => { tutorialPanel.gameObject.SetActive(false); });
                });
            });
        });
    }
    public void ClosePanel(CanvasGroup panel,UnityAction onComplete = null)
    {
        panel.DOFade(0, 0.5f).OnComplete(() => onComplete?.Invoke());
    }

    public void OnGameFinish(bool success)
    {
        GameController.instance.Delay(1f, () => 
        {
            if (success)
            {
                successPanel.SetActive(true);
                finishPanel.gameObject.SetActive(true);
                finishPanel.DOFade(1, 0.5f);

                continueButton.onClick.AddListener(GameController.instance.RestartScene);
            }
            else
            {
                failPanel.SetActive(true);
                finishPanel.gameObject.SetActive(true);
                finishPanel.DOFade(1, 0.5f);

                okayButton.onClick.AddListener(GameController.instance.RestartScene);
            }
        });
    }
}
