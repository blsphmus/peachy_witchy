<<<<<<< Updated upstream
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
=======
﻿using System;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject[] mushroomPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public string[] mushroomTags = { "Plant1", "Plant2", "Plant3", "Plant4", "Plant5" };
    public float minScale = 1f;
    public float maxScale = 1f;
    public float spawnRadius = 0.5f;

    private void Start()
    {
        StartCoroutine(SpawnMushrooms());
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
}
=======

    private void SpawnMushroom()
    {  
        bool mushroomSpawned = false;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, spawnRadius);
            bool isOccupied = false;

            foreach (var collider in colliders)
            {
                // Проверяем все теги из массива
                foreach (string tag in mushroomTags)
                {
                    if (collider.CompareTag(tag))
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (isOccupied) break;
            }

            if (!isOccupied)
            {
                GameObject mushroomPrefab = mushroomPrefabs[UnityEngine.Random.Range(0, mushroomPrefabs.Length)];
                Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                GameObject mushroom = Instantiate(mushroomPrefab, spawnPoint.position, rotation);

                float randomScale = UnityEngine.Random.Range(minScale, maxScale);
                mushroom.transform.localScale = Vector3.one * randomScale;

                mushroomSpawned = true;
                break;
            }
        }

    }

    //if (!mushroomSpawned)
    //{
    //    Debug.Log("   ,   .");
    //}
}
>>>>>>> Stashed changes
