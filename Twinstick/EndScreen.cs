using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI highScoreText;

    private PersistentScoreManager PersistentScoreManager;
    private FadeManager _fadeManager;

    public string restartScene;
    public string MainMenuScene;

    [SerializeField] private Image fadeOut;
    int score;
    int highScore;

    private void Start()
    {
        PersistentScoreManager = GetComponent<PersistentScoreManager>();
        _fadeManager = GetComponent<FadeManager>();
        //
        // // Retrieve the score and high score from PlayerPrefs and display them
        // score = PlayerPrefs.GetInt("Score", 0);
        // highScore = PlayerPrefs.GetInt("HighScore", 0);
        //
        // pointsText.text = "Score: " + score;
        // highScoreText.text = "HighScore: " + highScore;
    }

    public void OnTryAgain()
    {
        ResetScore();
        SceneManager.LoadScene("LilGoatedScene");
    }

    public void Restart()
    {
        StartCoroutine(FadeOut(fadeOut, 1, restartScene));
        _fadeManager.fading = true;
    }

    public void OnMainMenu()
    {
        StartCoroutine(FadeOut(fadeOut, 1, MainMenuScene));
        _fadeManager.fading = true;
    }

    private void ResetScore()
    {
        PlayerPrefs.DeleteKey("Score");
    }

    IEnumerator FadeOut(Image image, float duration, string name)
    {
        float elapsedTime = 0;

        float startAlpha = 0;
        float endAlpha = 1;

        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 255);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            image.color = newColor;
            Debug.Log(newColor);
            yield return null; // Pauzeer de coroutine zodat het soepel kan worden uitgevoerd
        }

        SceneManager.LoadScene(name);
        _fadeManager.fading = false; // Stel opnieuw in op "false" wanneer de fade is voltooid
    }
}