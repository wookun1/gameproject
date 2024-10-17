using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerWithRandomMovement : MonoBehaviour
{
    public GameObject prefab; // ������ �� ������
    public float startTime = 1f; // ���� ���� �ð�
    public float endTime = 10f; // ���� ���� �ð�
    public float spawnRate = 0.5f; // ���� ����
    public float delay = 15f; // �� ���� ��� �ð�
    public float moveSpeed = 2f; // ���� �̵� �ӵ�
    public Vector3 spawnAreaMin; // ���� ��ġ�� �ּҰ�
    public Vector3 spawnAreaMax; // ���� ��ġ�� �ִ밪

    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnRate); // startTime �� spawnRate �������� �� ����
        Invoke("StopSpawning", endTime); // endTime �� ���� ����
    }

    void Spawn()
    {
        // ���� ��ġ ���
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        // ���� ��ġ���� �� ����
        GameObject clone = Instantiate(prefab, randomPosition, Quaternion.identity);

        // ���� �������� �����̱� ���� RandomMovement ������Ʈ �߰� �� �ʱ�ȭ
        RandomMovement movement = clone.AddComponent<RandomMovement>();
        movement.moveSpeed = moveSpeed;

        // ���� �ð��� ������ �� �ڵ� ����
        Destroy(clone, delay);
    }

    void StopSpawning()
    {
        CancelInvoke("Spawn");
    }
}

// ���� ���� �������� �����ϴ� Ŭ����
public class RandomMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 randomDirection;

    void Start()
    {
        SetRandomDirection();
        InvokeRepeating("SetRandomDirection", 1f, 2f); // 2�ʸ��� ���ο� ���� ���� ����
    }

    void Update()
    {
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
    }

    void SetRandomDirection()
    {
        randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        ).normalized; // ������ �ӵ��� �����ϱ� ���� ����ȭ
    }
}
