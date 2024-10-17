using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float teleportDistance = 3.0f; // �����̵� �Ÿ� ����
    public float speedMultiplier = 2.0f; // �ӵ� �谡��
    public float speedBoostDuration = 5.0f; // �ӵ� �谡 ���� �ð�

    private Rigidbody rb;
    private float originalSpeed;
    private float speedBoostTimer = 0.0f;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
        originalSpeed = speed;
    }

    void FixedUpdate()
    {
        // ������ �ڵ�
        Vector3 movement = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // ȸ�� �ڵ�
        float mouseX = Input.GetAxis("Mouse X");
        Quaternion deltaRotation = Quaternion.Euler(0, mouseX * rotationSpeed * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // �ӵ� �谡 Ÿ�̸� ó��
        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.fixedDeltaTime;
            if (speedBoostTimer <= 0)
            {
                speed = originalSpeed; // ���� �ӵ��� ����
            }
        }
    }

    void Update()
    {
        // �����̵� �ڵ�
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
