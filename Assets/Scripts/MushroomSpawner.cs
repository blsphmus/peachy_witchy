using UnityEngine;
using System.Collections;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject[] mushroomPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public string[] mushroomTags = { "Plant1", "Plant2", "Plant3", "Plant4", "Plant5", "Plant6" }; // Исправлено на массив строк
    public float minScale = 1f;
    public float maxScale = 1f;
    public float spawnRadius = 0.5f;

    private void Start()
    {
        StartCoroutine(SpawnMushrooms());
    }

    private IEnumerator SpawnMushrooms()
    {
        while (true)
        {
            SpawnMushroom();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMushroom()
    {
        // Перемешиваем точки спавна для более случайного распределения
        ShuffleSpawnPoints();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (IsSpawnPointAvailable(spawnPoint.position))
            {
                CreateMushroom(spawnPoint);
                return; // Выходим после успешного создания гриба
            }
        }

        // Если не удалось создать гриб
        Debug.LogWarning("Не удалось создать гриб - все точки заняты");
    }

    private bool IsSpawnPointAvailable(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, spawnRadius);
        foreach (var collider in colliders)
        {
            foreach (string tag in mushroomTags) // Проверяем все теги из списка
            {
                if (collider.CompareTag(tag))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void CreateMushroom(Transform spawnPoint)
    {
        GameObject mushroomPrefab = mushroomPrefabs[Random.Range(0, mushroomPrefabs.Length)];
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        GameObject mushroom = Instantiate(mushroomPrefab, spawnPoint.position, rotation);

        float randomScale = Random.Range(minScale, maxScale);
        mushroom.transform.localScale = Vector3.one * randomScale;

        // Назначаем PlayerLook и LineRendererLocation
        DragRigidbody dragRb = mushroom.GetComponent<DragRigidbody>();
        if (dragRb != null)
        {
            // Находим игрока в сцене
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                // Получаем компонент PlayerLook
                dragRb.playerLook = player.GetComponent<PlayerLook>();

                // Находим LineRendererLocation в иерархии игрока
                Transform lineRenderLocation = player.transform.Find("Main Camera/wand/LineRendererLocation");
                if (lineRenderLocation != null)
                {
                    dragRb.lineRenderLocation = lineRenderLocation;

                    // Находим LineRenderer
                    dragRb.lr = lineRenderLocation.GetComponent<LineRenderer>();
                    if (dragRb.lr == null)
                    {
                        dragRb.lr = lineRenderLocation.GetComponentInChildren<LineRenderer>();
                    }
                }
            }
        }
    }

    private void ShuffleSpawnPoints()
    {
        // Алгоритм Фишера-Йетса для перемешивания массива
        for (int i = spawnPoints.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[j];
            spawnPoints[j] = temp;
        }
    }
}
