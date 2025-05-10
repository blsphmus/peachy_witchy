using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemy : MonoBehaviour
{
    public GameObject Potion;
    public GameObject PoofEffectPrefab;
    public bool isEmpty;
    public float currentX;
    public float currentY;

    private Renderer _renderer;
    

    // Start is called before the first frame update
    void Start()
    {
        _renderer = Potion.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Potion.SetActive(!isEmpty);

    }

    void OnTriggerEnter(Collider other)
    {
        if (isEmpty && other.CompareTag("Water"))
        {
            // Получаем позицию эффекта как точку пересечения
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            // Создаем эффект в точке контакта
            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            // Уничтожаем объект воды
            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.2f, 0.3f, 0.9f);

            currentX = 0f; currentY = 0f;

        }
        else if (isEmpty && other.CompareTag("Wine"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.75f, 0f, 0.2f);

            currentX = 0f; currentY = 0f;
        }
        else if (isEmpty && other.CompareTag("Oil"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.75f, 0.6f, 0.1f);

            currentX = 0f; currentY = 0f;
        }
        else if (!isEmpty && other.CompareTag("Plant1"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.5f);
            _renderer.material.color += new Color(0.1f, 0.1f, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant2"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, -0.7f);
            currentY += CheckPossibility(currentY, -0.9f);
            _renderer.material.color += new Color(-0.2f, -0.1f, 0.3f);
        }
        else if (!isEmpty && other.CompareTag("EmptyBottle"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            ActivateChildren(other.gameObject, true);

            PotionCheck();
            isEmpty = true;

            currentX = 0f; currentY = 0f;
        }
    }

    void PotionCheck()
    {
        Color targetColor = Color.white;

        if (currentY <= 0.5)
        {
            if      (currentX >= 0 && currentX <= 0.2f) // Левитация
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.2 && currentX <= 0.4) // Дождь в бутылке
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.4 && currentX <= 0.6) // Смелость
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.6 && currentX <= 0.8) // Очарование
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.8 && currentX <= 1) // Рост
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
        }
        else
        {
            if (currentX >= 0 && currentX <= 0.2f) // Уменьшение
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.2 && currentX <= 0.4) // Невидимость
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.4 && currentX <= 0.6) // Сон
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.6 && currentX <= 0.8) // Вдохновение
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.8 && currentX <= 1) // Пушистость
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
        }
    }


    float CheckPossibility(float a, float b)
    {
        if ((a+b) >= 1)
        {
            return (1 - a);
        }
        else if ((a + b) <= 0)
        {
            return (a*(-1));
        }
        else
        {
            return (b);
        }
    }

    void ActivateChildren(GameObject parent, bool state)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}
