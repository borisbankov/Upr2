using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPatrol : MonoBehaviour
{

    public float speed = 5.0f;
    public Transform targetA = null, targetB = null; // Transform ще носи информация за позицията на двете точки, между които ще се движи платформата
    bool changeDirection = false; // контролна променлива за смяна на посоката на движение

    void FixedUpdate()
    {

        if (!changeDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetA.position, speed * Time.deltaTime);
            // мястото на платформата ще се определя от мястото на игровия обект targetA
        }

        if (changeDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }

        if (transform.position == targetA.position)
        {
            changeDirection = true;
            // когато позицията на платформата съвпада с позицията на targetA ще се смени посоката чрез контролната променлива
        }

        if (transform.position == targetB.position)
        {
            changeDirection = false;
        }
    }
}
