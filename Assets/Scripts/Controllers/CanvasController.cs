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

    [Header("Buttons")]
    [SerializeField] private Button startButton;

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
}
