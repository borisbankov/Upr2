using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMovement : MonoBehaviour
{

    Rigidbody2D rb; // ������� �� ��� rigidbody

    [Header("Input keys")] // �������� � ����, ����� �� ����� � ����������
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys; // ������� �� ���������� (������� ��� WASD)
    public float speed = 10f; // ������� �� ��������
    float positionX; // ���� ������ �� �� ������ ������� ���� ����������� �� ������������

    [Header("Jump")] // ��������
    public KeyCode key = KeyCode.Space; // ��������� �� ���������� �� ��� KeyCode � ����������� �� space
    public float jumpStrength = 15f; // ���� �� ����
    bool Grounded; // ����������, � ����� �� ������� ���� ������� � ����� ����� ������� (��-������ � ����)
                   // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ����� ������� �� ��������� ����� ���������� �� ������
                                          // ��������� �� ������ �� ���������, ��� ����� � ������� �������(����) ������
    }
    private float moveHorizontal; // ����������, � ����� �� �������� ���� ������� � ������ �� �� ����� �
                                  // ��������� ��� A/D -- ���� �� ����������� �� ������������ � Unity, ���� �� horizontal � horizontal2 
                                  // �� ��������� ��������� �� Edit --> Project Settings --> Input

    // Update is called once per frame
    void Update()
    {
        // �� Unity -> Edit -> Project Settings -> Input Manager ������������� ��������� �� ����� ����� -> Reset, ���� �� ������� ������ Missing Input Button "Submit"
        moveHorizontal = Input.GetAxis("Horizontal");


        positionX = moveHorizontal * speed; // ���� ������ ������� �� � ���������� �� ����������� ������ A/D ��� ������ (-1,0)/(1,0),
                                            // ������� �� ��������.

        // �������� �������� �, ��� � �������� �������� �ey, ����� ��-���� � �������� (space ��� �����) 
        // � ������� � ������� Grounded == true (� �����, �� �����, � ��� �� �� �� ������� �����, �����, �������� ����)
        if (Input.GetKeyDown(key) && Grounded == true)
        {
            // �� �� ������ ������������� ���� �� ������ ������ �������� �� ������ �� ���� * ��������� ������ 50.
            rb.AddForce(Vector2.up * jumpStrength * 50.0f);
        }
    }
    // ���� �� �� ������� 0 ����, 1 ��� ��� ����� ���� ������ ���� ����� physics frames �� ������� �� � 
    // time settings � ����� � ���� frame rate-a, � ��� �������� � FixedUpdate �� ��������� �����
    // �����������/������� ���������� ��� ����������� �� ���� �� �������� ����� ���������� �� � � �������
    // � ���������� �� ������. ������� �� ����� ���� �� ������� ���� �� ������������ (���� �� ��������� ����������)
    void FixedUpdate()
    {
        // rigidoby2d.velocity � ������� ���� �� ��������, ����������� ���� ������ � ������ (x, y) � 
        // ����������� � ������� �� �������, 
        rb.velocity = new Vector2(positionX, rb.velocity.y);
    }


    // �������� OnCollisionEnter2D, OnCollisionExit2D � OnCollisionStay2D �� ���������� ��� ����� �� ������� ������� � ����
    // �������. � �������� collisionData e ����� �� ������������, � ����� �� ���� ���������� �� ������� �������, � ����� 
    // �� ������ ������� (����, ��� ����� � ������� �������). ����� � �� ������� ��� ����������� ��� ��� � ��� Platform � ��
    // ���� ����� �� ������ �� ����� �� ���������, ���� ���� ������ � ��������, ����� ��� � Platform
    void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Platform"))
        {  // ��� ��������� �� ����� �� ���� ������� � ��� Platform, �� ������� �������, �� ����� �������� (����) 
           // �� ��������� � ��� Platform ( ��� ��-��������� collisionData). ����, ������ ������� ����� ����� ������� �� 
           // ���������, ��� ���� �� ��������� ������ �������� ��� ������� �� �������� ����, � �� �� ����� ������ � �����������
           // ��� � � �����. ��������� (�������) ���� �� �� ����� �������� � ����������� ��� �� ��� ����� �� ���� ������, ��� � 
           // ��������� �� ��������� �������, �� ����������� �������� �������� �� �����������.
            this.transform.parent = collisionData.transform;
        }
        Grounded = true;
    }
    // ������ ��������� ���� �� �� ������ �� �����������, �� �������� �� ���� � �������� (����) 
    void OnCollisionExit2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
        Grounded = false;
    }
}