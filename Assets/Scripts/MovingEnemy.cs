using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Transform positionA, positionB;  // Transformaciones para las posiciones A y B de la plataforma
    public float speed;  // Velocidad de movimiento de la plataforma
    Vector3 targetPos;  // Posición objetivo de la plataforma

    private void Start()
    {
        targetPos = positionB.position;  // Inicialmente, establece la posición objetivo en la posición B
    }

    // Update se llama una vez por fotograma
    private void Update()
    {
        // Mueve la plataforma hacia la posición objetivo a la velocidad especificada
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Si la distancia entre la posición actual de la plataforma y la posición A es menor que 0.05 y la posición objetivo es A
        if (Vector2.Distance(transform.position, positionA.position) < 0.05f && targetPos == positionA.position)
        {
            targetPos = positionB.position;
            Flip();
        }

        // Si la distancia entre la posición actual de la plataforma y la posición B es menor que 0.05 y la posición objetivo es B
        if (Vector2.Distance(transform.position, positionB.position) < 0.05f && targetPos == positionB.position)
        {
            targetPos = positionA.position;
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la colisión es con un objeto con la etiqueta "Player", establece el objeto colisionante como hijo de la plataforma
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si la colisión es con un objeto con la etiqueta "Player", elimina la relación de hijo con la plataforma
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    // Método para voltear el sprite del enemigo
    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;  // Invertir la escala en el eje X
        transform.localScale = currentScale;
    }
}


