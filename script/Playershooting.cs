using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershooting : MonoBehaviour
{
    public GameObject prefab;
    public Transform shootPoint; // �߰��� shootPoint ����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject clone = Instantiate(prefab); // prefab ����
            clone.transform.position = shootPoint.transform.position; // shootPoint ��ġ ����
            clone.transform.rotation = shootPoint.transform.rotation; // shootPoint ȸ�� ����
        }
    }
}