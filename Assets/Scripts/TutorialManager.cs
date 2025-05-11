using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Sprite[] tutorialSprites;
    [SerializeField] private Image tutorialImageDisplay;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Player References")]
    [SerializeField] private PlayerLook playerLookScript;

    [Header("Settings")]
    [SerializeField] private KeyCode toggleKey = KeyCode.I;
    [SerializeField] private CursorLockMode gameCursorMode = CursorLockMode.Locked;
    [SerializeField] private bool hideCursorInGame = false;

    private int currentImageIndex = 0;
    private bool gameIsPaused = false;

    private void Start()
    {
        tutorialPanel.SetActive(false);
        previousButton.onClick.AddListener(ShowPreviousImage);
        nextButton.onClick.AddListener(ShowNextImage);
        UpdateButtons();

        // Устанавливаем начальное состояние игры
        SetGameState(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTutorialPanel();
        }
    }

    private void ToggleTutorialPanel()
    {
        bool willActivate = !tutorialPanel.activeSelf;
        tutorialPanel.SetActive(willActivate);

        if (willActivate)
        {
            ShowCurrentImage();
        }

        SetGameState(!willActivate);
    }

    private void SetGameState(bool gameActive)
    {
        gameIsPaused = !gameActive;
        Time.timeScale = gameActive ? 1f : 0f;

        if (gameActive)
        {
            // Режим игры
            Cursor.lockState = gameCursorMode;
            Cursor.visible = !hideCursorInGame;

            // Включаем управление камерой
            if (playerLookScript != null)
                playerLookScript.enabled = true;
        }
        else
        {
            // Режим меню/паузы
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Выключаем управление камерой
            if (playerLookScript != null)
                playerLookScript.enabled = false;
        }
    }

    private void ShowCurrentImage()
    {
        tutorialImageDisplay.sprite = tutorialSprites[currentImageIndex];
        UpdateButtons();
    }

    private void ShowNextImage()
    {
        if (currentImageIndex < tutorialSprites.Length - 1)
        {
            currentImageIndex++;
            ShowCurrentImage();
        }
    }

    private void ShowPreviousImage()
    {
        if (currentImageIndex > 0)
        {
            currentImageIndex--;
            ShowCurrentImage();
        }
    }

    private void UpdateButtons()
    {
        previousButton.interactable = currentImageIndex > 0;
        nextButton.interactable = currentImageIndex < tutorialSprites.Length - 1;
    }

    // Для внешнего доступа к состоянию паузы
    public bool IsGamePaused()
    {
        return gameIsPaused;
    }
}