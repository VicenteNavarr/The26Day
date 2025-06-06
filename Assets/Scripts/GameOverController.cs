using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;  // Texto para mostrar la puntuación final del jugador
    public TextMeshProUGUI highScoreText;  // Texto para mostrar la puntuación más alta

    void Start()
    {
        //finalScoreText.text = "Score: " + GameManager.playerScore.ToString();
        int currentScore = GameManager.playerScore;  // Obtiene la puntuación actual del jugador desde GameManager
        finalScoreText.text = "Your Score: " + currentScore.ToString();  // Muestra la puntuación final del jugador

        int highScore = PlayerPrefs.GetInt("HighScore", 0);  // Obtiene la puntuación más alta guardada en las preferencias del jugador
        highScoreText.text = "High Score: " + highScore.ToString();  // Muestra la puntuación más alta
    }
}


