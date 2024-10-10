using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerWithRandomMovement : MonoBehaviour
{
    public GameObject prefab; // 생성할 적 프리팹
    public float startTime = 1f; // 스폰 시작 시간
    public float endTime = 10f; // 스폰 종료 시간
    public float spawnRate = 0.5f; // 스폰 간격
    public float delay = 15f; // 적 제거 대기 시간
    public float moveSpeed = 2f; // 적의 이동 속도
    public Vector3 spawnAreaMin; // 랜덤 위치의 최소값
    public Vector3 spawnAreaMax; // 랜덤 위치의 최대값

    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnRate); // startTime 후 spawnRate 간격으로 적 생성
        Invoke("StopSpawning", endTime); // endTime 후 스폰 멈춤
    }

    void Spawn()
    {
        // 랜덤 위치 계산
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        // 랜덤 위치에서 적 생성
        GameObject clone = Instantiate(prefab, randomPosition, Quaternion.identity);

        // 랜덤 방향으로 움직이기 위한 RandomMovement 컴포넌트 추가 및 초기화
        RandomMovement movement = clone.AddComponent<RandomMovement>();
        movement.moveSpeed = moveSpeed;

        // 일정 시간이 지나면 적 자동 제거
        Destroy(clone, delay);
    }

    void StopSpawning()
    {
        CancelInvoke("Spawn");
    }
}

// 적의 랜덤 움직임을 구현하는 클래스
public class RandomMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 randomDirection;

    void Start()
    {
        SetRandomDirection();
        InvokeRepeating("SetRandomDirection", 1f, 2f); // 2초마다 새로운 랜덤 방향 설정
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
        ).normalized; // 일정한 속도를 유지하기 위해 정규화
    }
}
