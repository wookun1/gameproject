using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forwardmovement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Destroy(gameObject, 4f); // 2초 후에 총알이 자동으로 사라지도록 설정
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}