using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_GameMusic : MonoBehaviour
{
    public GameObject BGMusicPrefab;  // Префаб с аудиоисточником
    private static GameObject musicInstance;  // Единственный экземпляр музыки
    private static AudioSource audioSrc;  // Компонент AudioSource

    void Awake()
    {
        // Если музыка уже играет, уничтожаем новый объект
        if (musicInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Создаем новый экземпляр музыки
        musicInstance = Instantiate(BGMusicPrefab);
        musicInstance.name = "BGMusic";
        DontDestroyOnLoad(musicInstance);
        audioSrc = musicInstance.GetComponent<AudioSource>();

        // Начинаем воспроизведение
        if (!audioSrc.isPlaying)
        {
            audioSrc.Play();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Останавливаем музыку при загрузке главного меню
        if (scene.name == "VASYA_MENU") // Замените на имя вашей сцены меню
        {
            StopAndDestroyMusic();
        }
    }

    public static void StopAndDestroyMusic()
    {
        if (musicInstance != null)
        {
            if (audioSrc != null)
            {
                audioSrc.Stop();
            }
            Destroy(musicInstance);
            musicInstance = null;
            audioSrc = null;
        }
    }
}