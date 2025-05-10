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
        if (isEmpty && other.CompareTag("Water"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

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
            currentX += CheckPossibility(currentX, 0.1f);
            currentY += CheckPossibility(currentY, 0.2f);
            _renderer.material.color += new Color(0.3f, -0.1f, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant11"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.1f);
            currentY += CheckPossibility(currentY, 0.2f);
            _renderer.material.color += new Color(0.3f, -0.1f, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant12"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.1f);
            currentY += CheckPossibility(currentY, 0.2f);
            _renderer.material.color += new Color(0.3f, -0.1f, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant13"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.1f);
            currentY += CheckPossibility(currentY, 0.2f);
            _renderer.material.color += new Color(0.3f, -0.1f, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant2"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0f);
            currentY += CheckPossibility(currentY, -0.4f);
            _renderer.material.color += new Color(0f, 0.1f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant21"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0f);
            currentY += CheckPossibility(currentY, -0.4f);
            _renderer.material.color += new Color(0f, 0.1f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant22"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0f);
            currentY += CheckPossibility(currentY, -0.4f);
            _renderer.material.color += new Color(0f, 0.1f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant23"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0f);
            currentY += CheckPossibility(currentY, -0.4f);
            _renderer.material.color += new Color(0f, 0.1f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant3"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, -0.1f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(-0.1f, -0.1f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant31"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, -0.1f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(-0.1f, -0.1f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant32"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, -0.1f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(-0.1f, -0.1f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant33"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, -0.1f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(-0.1f, -0.1f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant4"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.5f);
            currentY += CheckPossibility(currentY, 0.5f);
            _renderer.material.color += new Color(0.2f, 0.2f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant41"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.5f);
            currentY += CheckPossibility(currentY, 0.5f);
            _renderer.material.color += new Color(0.2f, 0.2f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant42"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.5f);
            currentY += CheckPossibility(currentY, 0.5f);
            _renderer.material.color += new Color(0.2f, 0.2f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant43"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.5f);
            currentY += CheckPossibility(currentY, 0.5f);
            _renderer.material.color += new Color(0.2f, 0.2f, -0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant5"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant51"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant52"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant53"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant6"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant61"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant62"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("Plant63"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            currentX += CheckPossibility(currentX, 0.4f);
            currentY += CheckPossibility(currentY, 0.3f);
            _renderer.material.color += new Color(0.25f, 0.2f, 0.2f);
        }
        else if (!isEmpty && other.CompareTag("EmptyBottle"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Color potionColor = PotionCheck(); // –ü–æ–ª—É—á–∞–µ–º —Ü–≤–µ—Ç
            ActivateChildren(other.gameObject, true, potionColor); // –ü–µ—Ä–µ–¥–∞–µ–º —Ü–≤–µ—Ç

            isEmpty = true;
            currentX = 0f;
            currentY = 0f;
        }
    }

    Color PotionCheck()
    {
        Color targetColor = Color.white;

        if (currentY <= 0.5f)
        {
            if      (currentX >= 0 && currentX <= 0.2f) // ÀÂ‚ËÚ‡ˆËˇ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.2 && currentX <= 0.4) // ƒÓÊ‰¸ ‚ ·ÛÚ˚ÎÍÂ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.4 && currentX <= 0.6) // —ÏÂÎÓÒÚ¸
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.6 && currentX <= 0.8) // Œ˜‡Ó‚‡ÌËÂ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.8 && currentX <= 1) // –ÓÒÚ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
        }
        else
        {
            if (currentX >= 0 && currentX <= 0.2f) // ”ÏÂÌ¸¯ÂÌËÂ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.2 && currentX <= 0.4) // ÕÂ‚Ë‰ËÏÓÒÚ¸
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.4 && currentX <= 0.6) // —ÓÌ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.6 && currentX <= 0.8) // ¬‰ÓıÌÓ‚ÂÌËÂ
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (currentX > 0.8 && currentX <= 1) // œÛ¯ËÒÚÓÒÚ¸
            {
                targetColor = new Color(0.75f, 0.6f, 0.1f);
            }
        }

        return targetColor;
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
        // –ü–æ–ª—É—á–∞–µ–º Property ID –¥–ª—è —Å–≤–æ–π—Å—Ç–≤ —à–µ–π–¥–µ—Ä–∞
        int colorFrontID = Shader.PropertyToID("_ColorFront");
        int colorBackID = Shader.PropertyToID("_ColorBack");

        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(state);

            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null && childRenderer.material != null)
            {
                // –£—Å—Ç–∞–Ω–∞–≤–ª–∏–≤–∞–µ–º —Ü–≤–µ—Ç–∞ —á–µ—Ä–µ–∑ —Å–≤–æ–π—Å—Ç–≤–∞ –º–∞—Ç–µ—Ä–∏–∞–ª–∞
                childRenderer.material.SetColor(colorFrontID, color);
                childRenderer.material.SetColor(colorBackID, color - new Color(0f, 0.3f, 0.3f));
            }
        }
    }
}
