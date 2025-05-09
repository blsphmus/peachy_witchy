using UnityEngine;
using System.Collections;
using System.Linq;

public class MushroomSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] mushroomPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;
    public float collisionCheckRadius = 0.5f;
    public LayerMask collisionCheckMask;
    public LayerMask groundMask; // ��������� ����� ��� �����

    [Header("Scale Settings")]
    public Vector3 targetScale = Vector3.one;

    private void Start()
    {
        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points assigned!");

        if (mushroomPrefabs.Length == 0)
            Debug.LogError("No mushroom prefabs assigned!");

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            TrySpawnMushroom();
        }
    }

    private void TrySpawnMushroom()
    {
        var freePoint = FindFreeSpawnPoint();
        if (freePoint == null) return;

        var prefab = mushroomPrefabs[Random.Range(0, mushroomPrefabs.Length)];
        var instance = Instantiate(prefab, freePoint.position, Quaternion.identity); // ���������� identity rotation

        instance.transform.localScale = targetScale;
        PositionAndAlignMushroom(instance.transform); // ������������� �����
    }

    private void PositionAndAlignMushroom(Transform mushroom)
    {
        RaycastHit hit;
        if (Physics.Raycast(mushroom.position + Vector3.up * 2,
                          Vector3.down,
                          out hit,
                          3f,
                          groundMask))
        {
            // ������������� �������
            mushroom.position = hit.point;

            // ����������� ���� �� ������� �����������
            mushroom.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            // ��������� ��������� ������� ������ ��� Y ��� ��������������
            mushroom.Rotate(Vector3.up, Random.Range(0f, 360f), Space.Self);
        }
    }

    private Transform FindFreeSpawnPoint()
    {
        foreach (var point in spawnPoints.OrderBy(x => Random.value))
        {
            if (!Physics.CheckSphere(point.position, collisionCheckRadius, collisionCheckMask))
            {
                return point;
            }
        }
        return null;
    }
}