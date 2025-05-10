using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject[] mushroomPrefabs; // Массив префабов грибов
    public Transform[] spawnPoints; // Массив точек спавна
    public float spawnInterval = 2f; // Интервал спавна в секундах
    public string mushroomTag = "Mushroom"; // Тег для грибов
    public float minScale = 1f; // Минимальный масштаб
    public float maxScale = 1f; // Максимальный масштаб
    public float spawnRadius = 0.5f; // Радиус проверки спавна

    private void Start()
    {
        // Запускаем корутину для спавна грибов
        StartCoroutine(SpawnMushrooms());
    }

    private System.Collections.IEnumerator SpawnMushrooms()
    {
        while (true)
        {
            SpawnMushroom();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMushroom()
    {
        // Переменная для отслеживания, была ли найдена свободная точка
        bool mushroomSpawned = false;

        // Попытка спавна гриба
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Выбираем случайную точку спавна
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Проверяем, есть ли гриб на точке спавна
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, spawnRadius);
            bool isOccupied = false;

            foreach (var collider in colliders)
            {
                if (collider.CompareTag(mushroomTag))
                {
                    // Если гриб уже есть, помечаем точку как занятую
                    isOccupied = true;
                    break;
                }
            }

            // Если точка свободна, спавним гриб
            if (!isOccupied)
            {
                // Выбираем случайный префаб гриба
                GameObject mushroomPrefab = mushroomPrefabs[Random.Range(0, mushroomPrefabs.Length)];

                // Создаем гриб с поворотом на -90 градусов по оси X
                Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                GameObject mushroom = Instantiate(mushroomPrefab, spawnPoint.position, rotation);

                // Генерируем случайный масштаб в заданных пределах
                float randomScale = Random.Range(minScale, maxScale);
                Vector3 uniformScale = new Vector3(randomScale, randomScale, randomScale);

                // Применяем масштаб к грибу
                mushroom.transform.localScale = uniformScale;

                // Устанавливаем флаг, что гриб был успешно заспавнен
                mushroomSpawned = true;
                break; // Выходим из цикла, так как гриб успешно заспавнен
            }
        }

        // Если гриб не был заспавнен, можно добавить логику для обработки этого случая
        if (!mushroomSpawned)
        {
            Debug.Log("Не удалось заспавнить гриб, все точки заняты.");
        }
    }
}
