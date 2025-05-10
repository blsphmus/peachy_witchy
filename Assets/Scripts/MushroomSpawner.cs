using UnityEngine;
using System.Collections;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject[] mushroomPrefabs; // Массив префабов грибов
    public Transform[] spawnPoints;     // Массив точек появления
    public float spawnInterval = 10f;   // Интервал между появлениями

    private void Start()
    {
        // Начинаем спавн грибов
        StartCoroutine(SpawnMushroomsRoutine());
    }

    private IEnumerator SpawnMushroomsRoutine()
    {
        while (true) // Бесконечный цикл
        {
            yield return new WaitForSeconds(spawnInterval);

            // Выбираем случайный гриб и случайную точку
            GameObject randomMushroom = mushroomPrefabs[Random.Range(0, mushroomPrefabs.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Создаём гриб на сцене
            Instantiate(randomMushroom, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}