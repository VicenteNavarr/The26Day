using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Vector2 startPos;  // Posición inicial del jugador
    Rigidbody2D playerRb;  // Componente Rigidbody2D del jugador
    //SpriteRenderer spriteRenderer;  // Componente SpriteRenderer del jugador (comentado)
    [SerializeField] ParticleSystem fallParticle;  // Sistema de partículas para cuando el jugador cae
    SpriteRenderer spriteRenderer; // Añadir el SpriteRenderer

    public GameController2 gameController2;  // Referencia a otro controlador del juego
    AudioManager audioManager;  // Administrador de audio
    public int life = 3;  // Número de vidas del jugador

    private void Awake()
    {
        // Inicializa los componentes del jugador
        //spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start se llama una vez antes de la primera ejecución de Update después de crear el MonoBehaviour
    void Start()
    {
        startPos = transform.position;  // Guarda la posición inicial del jugador
        fallParticle.Stop();  // Detiene las partículas de caída

        gameController2 = FindObjectOfType<GameController2>();  // Busca y asigna el controlador de juego 2
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador colisiona con un obstáculo
        if (collision.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.hurt);  // Reproduce el sonido de daño
            Die();  // Llama a la función Die
            DeleteLife();  // Llama a la función DeleteLife
        }

        // Si el jugador colisiona con un enemigo
             if (collision.CompareTag("Enemy"))
        {
            audioManager.PlaySFX(audioManager.hurt);  // Reproduce el sonido de daño
            Die();  // Llama a la función Die
            DeleteLife();  // Llama a la función DeleteLife
        }

        // Si el jugador colisiona con una bolsa de monedas
        if (collision.CompareTag("CoinBag"))
        {
            audioManager.PlaySFX(audioManager.bonus);  // Reproduce el sonido de bonificación
            gameController2.AddScore();  // Añade puntuación
            Destroy(collision.gameObject);  // Destruye el objeto de la bolsa de monedas
        }

        // Si el jugador colisiona con el colisionador de victoria
        if (collision.CompareTag("WinCollider"))
        {
            audioManager.PlaySFX(audioManager.bonus);  // Reproduce el sonido de bonificación
            SceneManager.LoadScene(5);  // Carga la escena 5
        }
    }

    public void DeleteLife()
    {
        if (gameController2 != null)
        {
            life -= 1;  // Resta una vida

            if (life >= 0 && life < gameController2.lifes.Length)
            {
                if (gameController2.lifes[life] != null)
                {
                    gameController2.LoseLife(life);  // Llama a la función LoseLife
                }
                else
                {
                    Debug.LogError("El objeto en la lista 'lifes' es nulo en el índice: " + life);
                }
            }
            else
            {
                Debug.LogError("El valor de 'life' está fuera del rango de la lista 'lifes'");
            }

            if (life <= 0)
            {
                Debug.Log("¡Fin del juego!");
                SceneManager.LoadScene(2);  // Carga la escena de fin del juego
            }
        }
        else
        {
            Debug.LogError("gameController2 no ha sido asignado en el inspector");
        }
    }

    void Die()
    {
        fallParticle.Play();  // Activa las partículas de caída
        StartCoroutine(FadeOut(0.2f)); // Llama a la animación de desvanecimiento
        StartCoroutine(Respawn(0.5f));  // Llama a la función de reaparición
    }

    IEnumerator FadeOut(float duration)
    {
        float startAlpha = spriteRenderer.color.a;
        float endAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;  // Desactiva la simulación física del jugador
        playerRb.linearVelocity = new Vector2(0, 0);  // Detiene cualquier movimiento del jugador
        transform.localScale = new Vector3(0, 0, 0);  // Hace invisible al jugador
        yield return new WaitForSeconds(duration);  // Espera la duración especificada
        transform.position = startPos;  // Restablece la posición inicial del jugador
        transform.localScale = new Vector3(1, 1, 1);  // Restablece el tamaño del jugador
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); // Restablecer la transparencia
        playerRb.simulated = true;  // Activa la simulación física del jugador
        fallParticle.Stop();  // Detiene las partículas de caída
    }

    void Update()
    {
        // Si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();  // Llama a la función PauseGame
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Pausar el juego
        SceneManager.LoadScene("GamePause", LoadSceneMode.Additive); // Cargar la escena de pausa de manera aditiva
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Reanudar el juego
        SceneManager.UnloadSceneAsync("GamePause"); // Descargar la escena de pausa
    }
}
