using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

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
        GameController.instance.StartLevel();
        ClosePanel(startPanel);
    }
    public void ClosePanel(CanvasGroup panel)
    {
        panel.DOFade(0, 0.5f);
    }
}
