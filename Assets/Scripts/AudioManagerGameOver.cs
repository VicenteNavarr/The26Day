using UnityEngine;

public class AudioManagerGameOver : MonoBehaviour
{
    // Encabezado para la fuente de audio
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;  // Fuente de audio para la música
    [SerializeField] AudioSource SFXSource;    // Fuente de audio para los efectos de sonido (SFX)

    // Encabezado para los clips de audio
    [Header("---------- Audio Clip ----------")]  
    public AudioClip mainTheme;  // Clip de audio para el tema principal
    public AudioClip hurt;       // Clip de audio para el sonido de daño
    public AudioClip bonus;      // Clip de audio para el sonido de bonificación
    public AudioClip gameOver;   // Clip de audio para el sonido de fin del juego
    public AudioClip gameStart;  // Clip de audio para el sonido de inicio del juego

    // Start se ejecuta una vez antes de la primera ejecución de Update después de crear el MonoBehaviour
    void Start()
    {
        // Asigna el tema principal a la fuente de música y comienza a reproducirlo
        musicSource.clip = mainTheme;
        musicSource.Play();
    }

    // Método para reproducir un efecto de sonido específico
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
