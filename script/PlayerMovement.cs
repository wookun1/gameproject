using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float teleportDistance = 3.0f; // 순간이동 거리 설정
    public float speedMultiplier = 2.0f; // 속도 배가율
    public float speedBoostDuration = 5.0f; // 속도 배가 지속 시간

    private Rigidbody rb;
    private float originalSpeed;
    private float speedBoostTimer = 0.0f;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        originalSpeed = speed;
    }

    void FixedUpdate()
    {
        // 움직임 코드
        Vector3 movement = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // 회전 코드
        float mouseX = Input.GetAxis("Mouse X");
        Quaternion deltaRotation = Quaternion.Euler(0, mouseX * rotationSpeed * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // 속도 배가 타이머 처리
        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.fixedDeltaTime;
            if (speedBoostTimer <= 0)
            {
                speed = originalSpeed; // 원래 속도로 복구
            }
        }
    }

    void Update()
    {
        // 순간이동 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 teleportPosition = rb.position + transform.forward * teleportDistance;
            rb.MovePosition(teleportPosition);
        }
    }

    void Start()
    {
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            speed *= speedMultiplier;
            speedBoostTimer = speedBoostDuration;
        }
    }
}
