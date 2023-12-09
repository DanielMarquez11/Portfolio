using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Image fade_Out;
    public string mapToLoad;

    private FadeManager _fadeManager;

    [SerializeField] private GameObject OptionCanvas;

    [SerializeField] private GameObject[] falseobjects;

    [SerializeField] private GameObject Credits;


    private void Start()
    {
        _fadeManager = GetComponent<FadeManager>();
    }

    public void OnStartGame(string name)
    {
        StartCoroutine(FadeIn(fade_Out, 1, name));
        _fadeManager.fading = true;

        SceneManager.LoadScene(name);

        Debug.Log("Starting Game");
    }

    public void OnQuitGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }

    public void OnOptions()
    {
        OptionCanvas.SetActive(true);
        for (int i = 0; i < falseobjects.Length; i++)
        {
            falseobjects[i].SetActive(false);
        }

        Debug.Log("Loading Options");
    }

    public void Back()
    {
        OptionCanvas.SetActive(false);
        for (int i = 0; i < falseobjects.Length; i++)
        {
            falseobjects[i].SetActive(true);
        }
    }

    public void BackCredits()
    {
        Credits.SetActive(false);
        for (int i = 0; i < falseobjects.Length; i++)
        {
            falseobjects[i].SetActive(true);
        }
    }

    public void OnCredits()
    {
        Credits.SetActive(true);
        for (int i = 0; i < falseobjects.Length; i++)
        {
            falseobjects[i].SetActive(false);
        }
    }

    IEnumerator FadeIn(Image image, float duration, string name)
    {
        float elapsedTime = 0;

        Color startColor = new Color(image.color.r, image.color.g, image.color.b, 0);
        Color endColor = new Color(image.color.r, image.color.g, image.color.b, 1);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = Color.Lerp(startColor, endColor, t);
            image.color = newColor;
            Debug.Log(newColor);

            yield return null; // Pause the coroutine to allow smooth execution
        }

        SceneManager.LoadScene(name);
    }

    IEnumerator FadeOut(Image image, float duration)
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

            // Debug.Log(image.color.a);
            yield return null; // Pauzeer de coroutine zodat het soepel kan worden uitgevoerd
        }

        _fadeManager.fading = false; // Stel opnieuw in op "false" wanneer de fade is voltooid
    }
}