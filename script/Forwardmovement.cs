using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forwardmovement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Destroy(gameObject, 4f); // 2�� �Ŀ� �Ѿ��� �ڵ����� ��������� ����
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}