using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public string nextSceneName;
    private float fadeTime = 2f;

    public bool fading = false;
    public bool canChangeSeen;

    public Canvas FadeCanvas;
    public Image Fade_Out;
    public Image Fade_In;

    private void Start()
    {
        if (!canChangeSeen)
        {
            StartCoroutine(FadeIn(Fade_In, 3));
            fading = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !fading)
        {
            StartCoroutine(FadeOut(Fade_Out, fadeTime));
            if (canChangeSeen)
            {
                SceneManager.LoadScene(nextSceneName);
                fading = true;
            }
        }
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
            yield return null;
        }

        fading = false;
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        float elapsedTime = 0;

        Color startColor = new Color(image.color.r, image.color.g, image.color.b, 1);
        Color endColor = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = Color.Lerp(startColor, endColor, t);
            image.color = newColor;
            yield return null;
        }

        fading = false;
    }
}