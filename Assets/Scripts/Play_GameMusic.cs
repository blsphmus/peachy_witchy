using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_GameMusic : MonoBehaviour
{
    public GameObject BGMusicPrefab;  // ������ � ���������������
    private static GameObject musicInstance;  // ������������ ��������� ������
    private static AudioSource audioSrc;  // ��������� AudioSource

    void Awake()
    {
        // ���� ������ ��� ������, ���������� ����� ������
        if (musicInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        // ������� ����� ��������� ������
        musicInstance = Instantiate(BGMusicPrefab);
        musicInstance.name = "BGMusic";
        DontDestroyOnLoad(musicInstance);
        audioSrc = musicInstance.GetComponent<AudioSource>();

        // �������� ���������������
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
        // ������������� ������ ��� �������� �������� ����
        if (scene.name == "VASYA_MENU") // �������� �� ��� ����� ����� ����
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