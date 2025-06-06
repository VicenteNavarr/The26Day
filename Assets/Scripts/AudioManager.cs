using UnityEngine;

public class AudioManager : MonoBehaviour
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
        // Asigna el tema principal a la fuente de música y comienza a reproducirlo en bucle
        musicSource.clip = mainTheme;
        musicSource.loop = true;
        musicSource.Play();

        // Si existe un valor de volumen de música guardado, lo ajusta
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(musicVolume);
        }
        // Si existe un valor de volumen de efectos de sonido guardado, lo ajusta
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume(sfxVolume);
        }
    }

    // Método para reproducir un efecto de sonido específico
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Método para ajustar el volumen de la música
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Guarda el volumen de la música en las preferencias del jugador
    }

    // Método para ajustar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);  // Guarda el volumen de los efectos de sonido en las preferencias del jugador
    }
}

