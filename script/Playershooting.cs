using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershooting : MonoBehaviour
{
    public GameObject prefab;
    public Transform shootPoint; // 추가된 shootPoint 변수

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject clone = Instantiate(prefab); // prefab 생성
            clone.transform.position = shootPoint.transform.position; // shootPoint 위치 적용
            clone.transform.rotation = shootPoint.transform.rotation; // shootPoint 회전 적용
        }
    }
}