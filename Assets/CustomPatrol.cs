using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPatrol : MonoBehaviour
{

    public float speed = 5.0f;
    public Transform targetA = null, targetB = null; // Transform �� ���� ���������� �� ��������� �� ����� �����, ����� ����� �� �� ����� �����������
    bool changeDirection = false; // ��������� ���������� �� ����� �� �������� �� ��������

    void FixedUpdate()
    {

        if (!changeDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetA.position, speed * Time.deltaTime);
            // ������� �� ����������� �� �� �������� �� ������� �� ������� ����� targetA
        }

        if (changeDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }

        if (transform.position == targetA.position)
        {
            changeDirection = true;
            // ������ ��������� �� ����������� ������� � ��������� �� targetA �� �� ����� �������� ���� ����������� ����������
        }

        if (transform.position == targetB.position)
        {
            changeDirection = false;
        }
    }
}
