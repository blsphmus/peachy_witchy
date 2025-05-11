using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemy : MonoBehaviour
{
    public GameObject Potion;
    public GameObject[] BottleSpawns;
    public GameObject[] PoofEffectPrefab;
    public GameObject[] RespawnablePrefabs;
    public bool isEmpty;
    public float currentX;
    public float currentY;
    public int baseIs = 0; // 0 - пусто, 1 - вода, 2 - вино, 3 - масло
    public int cntInPotion = 0;

    private Renderer _renderer;


    void Start()
    {
        _renderer = Potion.GetComponent<Renderer>();
    }

    void Update()
    {
        Potion.SetActive(!isEmpty);

    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 collisionPoint = other.ClosestPoint(transform.position);

        if (isEmpty)
        {
            HandleEmptyPotion(other, collisionPoint);
        }
        else
        {
            HandleFilledPotion(other, collisionPoint);
            //_renderer.material.color = PotionCheck();
        }

        // ��������� ���� ����� ����� ����� ���������
        
    }

    void HandleEmptyPotion(Collider other, Vector3 collisionPoint)
    {
        if (other.CompareTag("Water"))
        {
            CreateEffect(collisionPoint);
            DestroyAndRespawn(other.gameObject, 0);
            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.2f, 0.3f, 0.9f);
            baseIs = 1;
            //ResetValues();
        }
        else if (other.CompareTag("Wine"))
        {
            CreateEffect(collisionPoint);
            DestroyAndRespawn(other.gameObject, 1);
            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.75f, 0f, 0.2f);
            baseIs = 2;
            //ResetValues();
        }
        else if (other.CompareTag("Oil"))
        {
            CreateEffect(collisionPoint);
            DestroyAndRespawn(other.gameObject, 2);
            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.75f, 0.6f, 0.1f);
            baseIs = 3;
            //ResetValues();
        }
    }

    void HandleFilledPotion(Collider other, Vector3 collisionPoint)
    {
        if (other.CompareTag("EmptyBottle") && cntInPotion >= 3)
        {
            Color potionColor = PotionCheck();
            DestroyAndRespawn(other.gameObject, 3);
            ActivateChildren(other.gameObject, true, potionColor);
            isEmpty = true;
            ResetValues();
            CreateEffect(collisionPoint);
            baseIs = 0;
            cntInPotion = 0;
            other.gameObject.tag = "FullBottle";
            return;
        }

        // ��������� ���� ��������
        float xModifier = 0f;
        float yModifier = 0f;

        if (other.CompareTag("Plant1"))
        {
            xModifier = 0.1f;
            yModifier = 0.2f;
        }
        else if (other.CompareTag("Plant11"))
        {
            xModifier = 0.1f;
            yModifier = 0.1f;
        }
        else if (!isEmpty && other.CompareTag("Plant12"))
        {
            xModifier = 0.3f;
            yModifier = 0f;
        }
        else if (!isEmpty && other.CompareTag("Plant13"))
        {
            xModifier = -0.1f;
            yModifier = -0.2f;
        }
        else if (!isEmpty && other.CompareTag("Plant2"))
        {
            xModifier = 0f;
            yModifier = -0.4f;
        }
        else if (!isEmpty && other.CompareTag("Plant21"))
        {
            xModifier = -0.1f;
            yModifier = -0.2f;
        }
        else if (!isEmpty && other.CompareTag("Plant22"))
        {
            xModifier = 0.1f;
            yModifier = -0.1f;
        }
        else if (!isEmpty && other.CompareTag("Plant23"))
        {
            xModifier = 0f;
            yModifier = -0.2f;
        }
        else if (!isEmpty && other.CompareTag("Plant24"))
        {
            xModifier = 0f;
            yModifier = 0.1f;
        }
        else if (!isEmpty && other.CompareTag("Plant3"))
        {
            xModifier = -0.1f;
            yModifier = 0.3f;
        }
        else if (!isEmpty && other.CompareTag("Plant31"))
        {
            xModifier = -0.1f;
            yModifier = -0.3f;
        }
        else if (!isEmpty && other.CompareTag("Plant32"))
        {
            xModifier = 0f;
            yModifier = 0.1f;
        }
        else if (!isEmpty && other.CompareTag("Plant33"))
        {
            xModifier = -0.3f;
            yModifier = 0f;
        }
        else if (!isEmpty && other.CompareTag("Plant4"))
        {
            xModifier = 0.5f;
            yModifier = 0.5f;
        }
        else if (!isEmpty && other.CompareTag("Plant41"))
        {
            xModifier = 0.15f;
            yModifier = 0.15f;
        }
        else if (!isEmpty && other.CompareTag("Plant42"))
        {
            xModifier = 0.25f;
            yModifier = -0.05f;
        }
        else if (!isEmpty && other.CompareTag("Plant43"))
        {
            xModifier = 0.05f;
            yModifier = -0.25f;
        }
        else if (!isEmpty && other.CompareTag("Plant5"))
        {
            xModifier = -0.2f;
            yModifier = -0.15f;
        }
        else if (!isEmpty && other.CompareTag("Plant51"))
        {
            xModifier = 0.1f;
            yModifier = -0.15f;
        }
        else if (!isEmpty && other.CompareTag("Plant52"))
        {
            xModifier = -0.05f;
            yModifier = 0.05f;
        }
        else if (!isEmpty && other.CompareTag("Plant53"))
        {
            xModifier = 0.2f;
            yModifier = 0.1f;
        }
        else if (!isEmpty && other.CompareTag("Plant6"))
        {
            xModifier = -0.1f;
            yModifier = -0.1f;
        }
        else if (!isEmpty && other.CompareTag("Plant61"))
        {
            xModifier = -0.15f;
            yModifier = -0.15f;
        }
        else if (!isEmpty && other.CompareTag("Plant62"))
        {
            xModifier = 0.2f;
            yModifier = 0.2f;
        }
        else if (!isEmpty && other.CompareTag("Plant63"))
        {
            xModifier = -0.25f;
            yModifier = -0.25f;
        }

        if (xModifier != 0 || yModifier != 0)
        {
            CreateEffect(collisionPoint);
            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, xModifier);
            currentY += CheckPossibility(currentY, yModifier);
            _renderer.material.color = PotionCheck();
            cntInPotion++;
        }
    }

    void CreateEffect(Vector3 position)
    {
        foreach (var effectPrefab in PoofEffectPrefab)
        {
            GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

    void ResetValues()
    {
        currentX = 0f;
        currentY = 0f;
    }

    Color PotionCheck()
    {
        Color targetColor = Color.white;

        if (baseIs == 1)
        {
            if (currentY <= 0.5f)
            {
                if (currentX >= 0 && currentX <= 0.2f)       // Левитация (Levitation)
                {
                    targetColor = new Color(0.45f, 0.6f, 0.6f);
                }
                else if (currentX > 0.2f && currentX <= 0.4f) // Дождь в бутылке (Bottled Rain)
                {
                    targetColor = new Color(0f, 0.4f, 0.75f);
                }
                else if (currentX > 0.4f && currentX <= 0.6f)   // Смелость (Courage)
                {
                    targetColor = new Color(0.75f, 0f, 0f);
                }
                else if (currentX > 0.6f && currentX <= 0.8f)   // Очарование (Charm)
                {
                    targetColor = new Color(1f, 0f, 0.7f);
                }
                else if (currentX > 0.8f && currentX <= 1f)     // Рост (Growth)
                {
                    targetColor = new Color(0.6f, 0.65f, 0f);
                }
            }
            else
            {
                if (currentX >= 0 && currentX <= 0.2f)         // Уменьшение (Shrinking)
                {
                    targetColor = new Color(0.7f, 0.4f, 0.2f);
                }
                else if (currentX > 0.2f && currentX <= 0.4f)   // Невидимость (Invisibility)
                {
                    targetColor = new Color(0.7f, 0.5f, 0.85f);
                }
                else if (currentX > 0.4f && currentX <= 0.6f)   // Сон (Sleep)
                {
                    targetColor = new Color(0.15f, 0.15f, 0.3f);
                }
                else if (currentX > 0.6f && currentX <= 0.8f)   // Вдохновение (Inspiration)
                {
                    targetColor = new Color(0.1f, 1f, 0.7f);
                }
                else if (currentX > 0.8f && currentX <= 1f)     // Пушистость (Fluffiness)
                {
                    targetColor = new Color(0.75f, 0.45f, 0.2f);
                }
            }
        }
        else if (baseIs == 2)
        {
            if (currentY <= 0.5f)
            {
                if (currentX >= 0 && currentX <= 0.2f)       // Пушистость (Fluffiness)
                {
                    targetColor = new Color(0.75f, 0.45f, 0.2f);
                }
                else if (currentX > 0.2f && currentX <= 0.4f) // Вдохновение (Inspiration)
                {
                    targetColor = new Color(0.1f, 1f, 0.7f);
                }
                else if (currentX > 0.4f && currentX <= 0.6f)   // Сон (Sleep)
                {
                    targetColor = new Color(0.15f, 0.15f, 0.3f);
                }
                else if (currentX > 0.6f && currentX <= 0.8f)   // Невидимость (Invisibility)
                {
                    targetColor = new Color(0.7f, 0.5f, 0.85f);
                }
                else if (currentX > 0.8f && currentX <= 1f)     // Уменьшение (Shrinking)
                {
                    targetColor = new Color(0.7f, 0.4f, 0.2f);
                }
            }
            else
            {
                if (currentX >= 0 && currentX <= 0.2f)         // Рост (Growth)
                {
                    targetColor = new Color(0.6f, 0.65f, 0f);
                }
                else if (currentX > 0.2f && currentX <= 0.4f)   // Очарование (Charm)
                {
                    targetColor = new Color(1f, 0f, 0.7f);
                }
                else if (currentX > 0.4f && currentX <= 0.6f)   // Смелость (Courage)
                {
                    targetColor = new Color(0.75f, 0f, 0f);
                }
                else if (currentX > 0.6f && currentX <= 0.8f)   // Дождь в бутылке (Bottled Rain)
                {
                    targetColor = new Color(0f, 0.4f, 0.75f);
                }
                else if (currentX > 0.8f && currentX <= 1f)     // Левитация (Levitation)
                {
                    targetColor = new Color(0.45f, 0.6f, 0.6f);
                }
            }
        }
        else if (baseIs == 3)
        {
            if (currentY <= 0.5f)
            {
                if (currentX >= 0 && currentX <= 0.2f)       // Уменьшение (Shrinking)
                {
                    targetColor = new Color(0.7f, 0.4f, 0.2f);
                }
                else if (currentX > 0.2f && currentX <= 0.4f) // Невидимость (Invisibility)
                {
                    targetColor = new Color(0.7f, 0.5f, 0.85f);
                }
                else if (currentX > 0.4f && currentX <= 0.6f)   // Сон (Sleep)
                {
                    targetColor = new Color(0.15f, 0.15f, 0.3f);
                }
                else if (currentX > 0.6f && currentX <= 0.8f)   // Вдохновение (Inspiration)
                {
                    targetColor = new Color(0.75f, 0.45f, 0.2f);
                }
                else if (currentX > 0.8f && currentX <= 1f)     // Пушистость (Fluffiness)
                {
                    targetColor = new Color(0.1f, 1f, 0.7f);
                }
            }
            else
            {
                if (currentX >= 0 && currentX <= 0.2f)         // Левитация (Levitation)
                {
                    targetColor = new Color(0.45f, 0.6f, 0.6f);
                }
                else if (currentX > 0.2f && currentX <= 0.4f)   // Дождь в бутылке (Bottled Rain)
                {
                    targetColor = new Color(0f, 0.4f, 0.75f);
                }
                else if (currentX > 0.4f && currentX <= 0.6f)   // Смелость (Courage)
                {
                    targetColor = new Color(0.75f, 0f, 0f);
                }
                else if (currentX > 0.6f && currentX <= 0.8f)   // Очарование (Charm)
                {
                    targetColor = new Color(1f, 0f, 0.7f);
                }
                else if (currentX > 0.8f && currentX <= 1f)     // Рост (Growth)
                {
                    targetColor = new Color(0.6f, 0.65f, 0f);
                }
            }
        }

        return targetColor;
    }

    void DestroyAndRespawn(GameObject objToDestroy, int prefabIndex)
    {
        StartCoroutine(RespawnWithDelay(prefabIndex, 2f)); // ������� ����� 2 �������
    }

    IEnumerator RespawnWithDelay(int prefabIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���������, ��� ������ � �������� �������
        if (prefabIndex >= 0 && prefabIndex < RespawnablePrefabs.Length &&
            prefabIndex < BottleSpawns.Length && BottleSpawns[prefabIndex] != null)
        {
            // ������� ����� ������ � ����� ������
            GameObject newObj = Instantiate(
                RespawnablePrefabs[prefabIndex],
                BottleSpawns[prefabIndex].transform.position,
                Quaternion.identity
            );

            // ������� ������ � ����� ������
            CreateEffect(BottleSpawns[prefabIndex].transform.position);
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

    void ActivateChildren(GameObject parent, bool state, Color color)
    {
        // Получаем Property ID для свойств шейдера
        int colorFrontID = Shader.PropertyToID("_ColorFront");
        int colorBackID = Shader.PropertyToID("_ColorBack");

        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(state);

            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null && childRenderer.material != null)
            {
                // Устанавливаем цвета через свойства материала
                childRenderer.material.SetColor(colorFrontID, color);
                childRenderer.material.SetColor(colorBackID, color - new Color(0f, 0.3f, 0.3f));
            }
        }
    }
}
