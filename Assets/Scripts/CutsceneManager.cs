using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Image slideImage;
    [SerializeField] private Sprite[] slides;
    [SerializeField] private float slideDuration = 3f;

    private int currentSlideIndex = 0;

    void Start()
    {
        if (slides.Length > 0)
        {
            StartCoroutine(ShowSlides());
        }
        else
        {
            Debug.LogError("No slides assigned!");
            LoadGameScene();
        }
    }

    IEnumerator ShowSlides()
    {
        slideImage.sprite = slides[currentSlideIndex];
        slideImage.color = Color.white;

        while (currentSlideIndex < slides.Length)
        {
            // Показ текущего слайда
            yield return new WaitForSeconds(slideDuration);

            // Смена слайда (пока экран затемнен)
            currentSlideIndex++;
            if (currentSlideIndex >= slides.Length) break;
            slideImage.sprite = slides[currentSlideIndex];

        }

        LoadGameScene();
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("alisa_delaet 1");
    }

    // Опционально: пропуск катсцены по нажатию клавиши
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            LoadGameScene();
        }
    }
}