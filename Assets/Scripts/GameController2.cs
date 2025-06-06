using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour
{
    public int score;  // Puntuación del jugador
    public Text scoreText;  // Texto UI que muestra la puntuación
    public GameObject[] lifes;  // Array de objetos que representan las vidas del jugador

    void Start()
    {
        score = 0;  // Inicializa la puntuación a 0
        scoreText.text = score.ToString();  // Actualiza el texto de la puntuación
    }

    public void AddScore()
    {
        score++;  // Incrementa la puntuación
        scoreText.text = score.ToString();  // Actualiza el texto de la puntuación
        SaveHighScore(score);  // Guarda la puntuación más alta
        GameManager.playerScore = score;  // Actualiza la puntuación del jugador en el GameManager
    }

    private void SaveHighScore(int score)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);  // Obtiene la puntuación más alta actual
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);  // Guarda la nueva puntuación más alta
            PlayerPrefs.Save();  // Guarda las preferencias del jugador
        }
    }

    public void LoseLife(int index)
    {
        if (index >= 0 && index < lifes.Length)
        {
            lifes[index].SetActive(false);  // Desactiva el objeto de vida correspondiente
        }
    }
}
