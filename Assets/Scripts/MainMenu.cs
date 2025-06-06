using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Necesario para trabajar con botones

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;  // Administrador de audio
    public float delayDuration = 1f; // Duración del retraso en segundos

    public Button easyButton;  // Referencia al botón de dificultad fácil
    public Button hardButton;  // Referencia al botón de dificultad difícil

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Si no se encuentra el AudioManager, muestra un error
        if (audioManager == null)
        {
            Debug.LogError("AudioManager no encontrado. Asegúrate de que hay un objeto con el tag 'Audio' y el componente 'AudioManager' en la escena.");
        }
    }

    private void Start()
    {
        // Establecer la dificultad por defecto a "Easy" si no está configurada
        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.SetString("Difficulty", "Easy");
        }

        // Leer la dificultad guardada y actualizar los colores de los botones
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        UpdateButtonColors(difficulty);
    }

    // Método para seleccionar dificultad fácil
    public void SetEasyDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "Easy");
        UpdateButtonColors("Easy");
    }

    // Método para seleccionar dificultad difícil
    public void SetHardDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "Hard");
        UpdateButtonColors("Hard");
    }

    // Método para actualizar los colores de los botones según la dificultad
    private void UpdateButtonColors(string difficulty)
    {
        bool isEasySelected = (difficulty == "Easy");
        ChangeButtonColor(easyButton, isEasySelected);
        ChangeButtonColor(hardButton, !isEasySelected);
    }

    // Método para iniciar el juego
    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.gameStart);  // Reproduce el sonido de inicio del juego

        // Leer la dificultad guardada en PlayerPrefs
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        int sceneIndex = (difficulty == "Easy") ? 1 : 6;  // Escena 1 si es fácil, escena 6 si es difícil

        StartCoroutine(DelayedSceneLoad(sceneIndex));  // Inicia la carga retrasada de la escena correspondiente
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        audioManager.PlaySFX(audioManager.gameStart);  // Reproduce el sonido de inicio del juego
        Time.timeScale = 1; // Reanudar el juego
        SceneManager.UnloadSceneAsync("GamePause"); // Descargar la escena de pausa
    }

    // Método para ir a las opciones del juego
    public void Options()
    {
        audioManager.PlaySFX(audioManager.gameStart);  // Reproduce el sonido de inicio del juego
        StartCoroutine(DelayedSceneLoad(3));  // Inicia la carga retrasada de la escena con índice 3
    }

    // Método para volver al menú principal
    public void Back()
    {
        audioManager.PlaySFX(audioManager.gameStart);  // Reproduce el sonido de inicio del juego
        StartCoroutine(DelayedSceneLoad(0));  // Inicia la carga retrasada de la escena con índice 0
    }

    // Método para cargar la escena con un retraso
    private IEnumerator DelayedSceneLoad(int sceneIndex)
    {
        yield return new WaitForSeconds(delayDuration);  // Espera la duración especificada
        SceneManager.LoadSceneAsync(sceneIndex);  // Carga la escena de manera asincrónica
    }

    // Método para cerrar el juego
    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.gameStart);  // Reproduce el sonido de inicio del juego
        Application.Quit();  // Cierra la aplicación
    }

    // Método para cambiar el color del botón seleccionado
    private void ChangeButtonColor(Button button, bool isSelected)
    {
        if (button != null)
        {
            Color selectedColor = new Color32(188, 0, 0, 255);  // Color hexadecimal BC0000
            Color defaultColor = Color.white;  // Color por defecto (blanco)
            
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = isSelected ? selectedColor : defaultColor;
            colorBlock.selectedColor = isSelected ? selectedColor : defaultColor;
            colorBlock.highlightedColor = isSelected ? selectedColor : defaultColor;
            button.colors = colorBlock;
        }
    }
}







