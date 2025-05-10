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
        if (!isEmpty)
            Potion.SetActive(true);
        else
            Potion.SetActive(false);

    }

    void OnTriggerEnter(Collider other)
    {
        if (isEmpty && other.CompareTag("Water"))
        {
            // �������� ������� ������� ��� ����� �����������
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            // ������� ������ � ����� ��������
            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            // ���������� ������ ����
            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.2f, 0.3f, 0.9f);

        }
        else if (isEmpty && other.CompareTag("Wine"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.75f, 0f, 0.2f);
        }
        else if (isEmpty && other.CompareTag("Oil"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            isEmpty = false;
            _renderer.material.color = new Color(0.75f, 0.6f, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("Plant1"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            //currentX += ChekPossibility(currentX, 0.4f);
            //currentY += ChekPossibility(currentY, 0.5f);
        }
        else if (!isEmpty && other.CompareTag("Plant2"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(other.gameObject);
            //currentX += ChekPossibility(currentX, 0.1f);
            //currentY += ChekPossibility(currentY, 0.1f);
        }
        else if (!isEmpty && other.CompareTag("EmptyBottle"))
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            GameObject effect = Instantiate(PoofEffectPrefab, collisionPoint, Quaternion.identity);
            Destroy(effect, 1f);

            //PotionCheck();
        }
    }

    /*void PotionCheck()
    {
        if (currentY <= 0.5)
        {
            if ( 0 <= currentX <= 0.2) // ���������
            {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.2 < currentX <= 0.4) // ����� � �������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.4 < currentX <= 0.6) // ��������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.6 < currentX <= 0.8) // ����������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.8 < currentX <= 1) // ����
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
        }
        else
        {
            if (0 <= currentX <= 0.2) // ����������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.2 < currentX <= 0.4) // �����������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.4 < currentX <= 0.6) // ���
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.6 < currentX <= 0.8) // �����������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
            else if (0.8 < currentX <= 1) // ����������
        {
                color = new Color(0.75f, 0.6f, 0.1f);
            }
        }
    }*/

    /*void ChekPossibility(a, b)
    {
        private float res; 

        if ((a + b) > 1)
        {
            res = 1f;
        }
        else
        {
            res = a + b;
        }
        return (res);
    }*/
}
