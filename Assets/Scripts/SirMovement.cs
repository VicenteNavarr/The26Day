using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirMovement : MonoBehaviour
{
    public float Speed;  // Velocidad de movimiento
    public float JumpForce;  // Fuerza del salto
    public GameController2 gameController;  // Referencia al controlador del juego

    private Rigidbody2D Rigidbody2D;  // Componente Rigidbody2D del jugador
    private Animator Animator;  // Componente Animator del jugador
    private float Horizontal;  // Movimiento horizontal
    private bool Grounded;  // Bandera para verificar si el jugador está en el suelo
    //private int Health = 5;  // Salud del jugador (comentado)

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();  // Obtiene el componente Rigidbody2D
        Animator = GetComponent<Animator>();  // Obtiene el componente Animator
        gameController = FindObjectOfType<GameController2>();  // Encuentra el controlador del juego
    }

    private void Update()
    {
        // Movimiento
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Cambia la escala del jugador según la dirección del movimiento
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        // Detectar Suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
        Debug.Log("Grounded: " + Grounded); // Agregar esta línea para depuración

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Movimiento horizontal
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }

    private void Jump()
    {
        // Aplica una fuerza hacia arriba para el salto
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador colisiona con una bolsa de monedas
        if (collision.CompareTag("CoinBag"))
        {
            gameController.AddScore();  // Añade puntuación
            Destroy(collision.gameObject);  // Destruye el objeto de la bolsa de monedas
        }
    }*/
}
