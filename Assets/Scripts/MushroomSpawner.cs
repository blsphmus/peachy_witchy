using UnityEngine;
using System.Collections;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject[] mushroomPrefabs; // ������ �������� ������
    public Transform[] spawnPoints;     // ������ ����� ���������
    public float spawnInterval = 10f;   // �������� ����� �����������

    private void Start()
    {
        // �������� ����� ������
        StartCoroutine(SpawnMushroomsRoutine());
    }

    private IEnumerator SpawnMushroomsRoutine()
    {
        while (true) // ����������� ����
        {
            yield return new WaitForSeconds(spawnInterval);

            // �������� ��������� ���� � ��������� �����
            GameObject randomMushroom = mushroomPrefabs[Random.Range(0, mushroomPrefabs.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // ������ ���� �� �����
            Instantiate(randomMushroom, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}