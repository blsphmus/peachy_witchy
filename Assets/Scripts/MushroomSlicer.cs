using UnityEngine;

public class MushroomSlicer : MonoBehaviour
{
    [SerializeField] private GameObject[] slicedMushroomPrefabs; // Массив префабов нарезанных частей
    [SerializeField] private float explosionForce = 5f; // Сила "разлёта" кусочков
    [SerializeField] private float explosionRadius = 2f; // Радиус "взрыва"

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли гриб с доской (по тегу)
        if (collision.gameObject.CompareTag("CuttingBoard"))
        {
            SliceMushroom(collision.contacts[0].point); // Точка контакта
        }
    }

    private void SliceMushroom(Vector3 contactPoint)
    {
        // Спавним нарезанные части
        foreach (GameObject slicedPiece in slicedMushroomPrefabs)
        {
            GameObject piece = Instantiate(slicedPiece, transform.position, Random.rotation);
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