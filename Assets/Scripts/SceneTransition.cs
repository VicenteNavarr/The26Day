using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;  // Imagen utilizada para el efecto de desvanecimiento
    public float fadeDuration = 1f;  // Duración del desvanecimiento en segundos

    private void Start()
    {
        // Comienza con la imagen completamente desvanecida
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeIn());  // Inicia la corrutina para desvanecer la imagen
    }

    // Método para iniciar el desvanecimiento hacia una nueva escena
    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeOut(sceneIndex));  // Inicia la corrutina para desvanecer la imagen y cargar la nueva escena
    }

    // Corrutina para desvanecer la imagen al iniciar la escena
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);  // Reduce la opacidad de la imagen
            fadeImage.color = color;
            yield return null;
        }
    }

    // Corrutina para desvanecer la imagen al cambiar de escena
    private IEnumerator FadeOut(int sceneIndex)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);  // Aumenta la opacidad de la imagen
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);  // Carga la nueva escena
    }
}
