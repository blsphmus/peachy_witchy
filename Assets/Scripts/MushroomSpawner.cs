using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject[] mushroomPrefabs; // ������ �������� ������
    public Transform[] spawnPoints; // ������ ����� ������
    public float spawnInterval = 2f; // �������� ������ � ��������
    public string mushroomTag = "Mushroom"; // ��� ��� ������
    public float minScale = 1f; // ����������� �������
    public float maxScale = 1f; // ������������ �������
    public float spawnRadius = 0.5f; // ������ �������� ������

    private void Start()
    {
        // ��������� �������� ��� ������ ������
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
        // ���������� ��� ������������, ���� �� ������� ��������� �����
        bool mushroomSpawned = false;

        // ������� ������ �����
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // �������� ��������� ����� ������
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // ���������, ���� �� ���� �� ����� ������
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, spawnRadius);
            bool isOccupied = false;

            foreach (var collider in colliders)
            {
                if (collider.CompareTag(mushroomTag))
                {
                    // ���� ���� ��� ����, �������� ����� ��� �������
                    isOccupied = true;
                    break;
                }
            }

            // ���� ����� ��������, ������� ����
            if (!isOccupied)
            {
                // �������� ��������� ������ �����
                GameObject mushroomPrefab = mushroomPrefabs[Random.Range(0, mushroomPrefabs.Length)];

                // ������� ���� � ��������� �� -90 �������� �� ��� X
                Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                GameObject mushroom = Instantiate(mushroomPrefab, spawnPoint.position, rotation);

                // ���������� ��������� ������� � �������� ��������
                float randomScale = Random.Range(minScale, maxScale);
                Vector3 uniformScale = new Vector3(randomScale, randomScale, randomScale);

                // ��������� ������� � �����
                mushroom.transform.localScale = uniformScale;

                // ������������� ����, ��� ���� ��� ������� ���������
                mushroomSpawned = true;
                break; // ������� �� �����, ��� ��� ���� ������� ���������
            }
        }

        // ���� ���� �� ��� ���������, ����� �������� ������ ��� ��������� ����� ������
        if (!mushroomSpawned)
        {
            Debug.Log("�� ������� ���������� ����, ��� ����� ������.");
        }
    }
}
