using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDust : MonoBehaviour
{
    [SerializeField] private GameObject[] DustMushroomPrefabs; 
    [SerializeField] private float explosionForce = 5f; // Сила "разлёта" кусочков
    [SerializeField] private float explosionRadius = 2f; // Радиус "взрыва"

    //private Dictionary<string, int> mushroomToDustIndex = new Dictionary<string, int>()
    //{
    //    {"Plant1", 0},  // Plant1 -> Plant13 (индекс 0 в массиве)
    //    {"Plant2", 1},  // Plant2 -> Plant23 (индекс 1)
    //    {"Plant3", 2},
    //    {"Plant4", 3},
    //    {"Plant5", 4},
    //    {"Plant6", 5},
    //};

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли гриб с доской (по тегу)
        if (collision.gameObject.CompareTag("Mortar"))
        {
            DustMushroom(collision.contacts[0].point); // Точка контакта
        }
    }

    private void DustMushroom(Vector3 contactPoint)
    {
        // Спавним нарезанные части
        foreach (GameObject DustPiece in DustMushroomPrefabs)
        {
            GameObject piece = Instantiate(DustPiece, transform.position, Random.rotation);
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            
            // Добавляем силу для разлёта кусочков
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
            }
        }

        // Уничтожаем цельный гриб
        Destroy(gameObject);
    }
}
