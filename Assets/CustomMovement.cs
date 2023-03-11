using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMovement : MonoBehaviour
{

    Rigidbody2D rb; // елемент от тип rigidbody

    [Header("Input keys")] // заглавие в болд, което се вижда в интерфейса
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys; // клавиши за управление (стрелки или WASD)
    public float speed = 10f; // скорост на движение
    float positionX; // къде трябва да се намира играчът след приключване на изчисленията

    [Header("Jump")] // заглавие
    public KeyCode key = KeyCode.Space; // създаване на променлива от тип KeyCode и присвояване на space
    public float jumpStrength = 15f; // сила на скок
    bool Grounded; // променлива, с която да отчетем дали играчът е върху даден елемент (по-надолу в кода)
                   // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // старт методът се изпълнява преди стартиране на играта
                                          // присвоява се тялото на елементът, към който е добавен текущия(този) скрипт
    }
    private float moveHorizontal; // променлива, в която да присвоим дали играчът е избрал да се движи с
                                  // стрелките или A/D -- това са настройките по подразбиране в Unity, може на horizontal и horizontal2 
                                  // да промените клавишите от Edit --> Project Settings --> Input

    // Update is called once per frame
    void Update()
    {
        // от Unity -> Edit -> Project Settings -> Input Manager рестартирайте клавишите от трите точки -> Reset, така ще отпадне грешка Missing Input Button "Submit"
        moveHorizontal = Input.GetAxis("Horizontal");


        positionX = moveHorizontal * speed; // нека новата позиция да е натиснатия от потребителя клавиш A/D или вектор (-1,0)/(1,0),
                                            // умножен по скороста.

        // следната проверка е, ако е задържан клавишът кey, който по-рано е настроен (space или друго) 
        // и играчът е заземен Grounded == true (в покой, не скача, с цел да не се създаде двоен, троен, четворен скок)
        if (Input.GetKeyDown(key) && Grounded == true)
        {
            // да се добави продължителна сила по посока нагоре умножена по силата на скок * контролен фактор 50.
            rb.AddForce(Vector2.up * jumpStrength * 50.0f);
        }
    }
    // може да се изпълни 0 пъти, 1 път или много пъти според това колко physics frames на секунда са в 
    // time settings и колко е бърз frame rate-a, и код поставен в FixedUpdate се изпълнява преди
    // геометрични/физични изчисления или прилагането на сила за движение върху елементите ще е в синхрон
    // с рисуването на екрана. Играчът се движи като се приложи сила по хоринзонтала (това са физически изчисления)
    void FixedUpdate()
    {
        // rigidoby2d.velocity е линейна сила на движение, представена като Вектор с посока (x, y) и 
        // изчислявана в единици на секунда, 
        rb.velocity = new Vector2(positionX, rb.velocity.y);
    }


    // методите OnCollisionEnter2D, OnCollisionExit2D и OnCollisionStay2D се изпълняват при допир на текущия елемент с друг
    // елемент. В примерът collisionData e името на променливата, в която се пази информация за другият елемент, с който 
    // се допира текушия (този, към който е добавен скрипта). Нужно е да добавим към платформите нов таг с име Platform И по
    // този начин ще следим за допир на елементът, имащ този скрипт и елементи, чиито таг е Platform
    void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Platform"))
        {  // ако елементът се допре до друг елемент с таг Platform, то текущия елемент, ще стане подчинен (дете) 
           // на елементът с таг Platform ( или по-конкретно collisionData). Така, когато играчът скочи върху движеща се 
           // платформа, той няма да увеличава своето движение под влияние на външната сила, а ще се движи заедно с платформата
           // ако е в покой. Елементът (играчът) може да се движи свободно в платформата без да има нужда от този скрипт, той е 
           // единсвено да застопори играчът, да предотврати неговото плъзгане по платформата.
            this.transform.parent = collisionData.transform;
        }
        Grounded = true;
    }
    // когато елементът спре да се допира до платформата, да престане да бъде й подчинен (дете) 
    void OnCollisionExit2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
        Grounded = false;
    }
}