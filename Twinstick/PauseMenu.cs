using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    private Playercontrols controls;
    [SerializeField] private GameObject PauseMenuCanvas;
    [SerializeField] private GameObject OptionCanvas;
    [SerializeField] private GameObject creditsCanvas;
    private Animation credits;

    private bool weaponWheelSelected = false;

    [SerializeField] private GameObject Tab;

    public string mapToLoad;

    [SerializeField] private Image Fade;

    private FadeManager fadeManager;


    private void Start()
    {
        credits = GetComponentInChildren<Animation>();
        fadeManager = GetComponent<FadeManager>();
    }

    private void OnEnable()
    {
        controls = new Playercontrols();
        controls.UI.Enable();
        controls.UI.Pause.performed += OnPause;
        controls.PlayerMovement.Disable();
    }

    private void OnDisable()
    {
        controls = new Playercontrols();
        controls.UI.Enable();
        controls.UI.Pause.performed -= OnPause;
        controls.PlayerMovement.Enable();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                PauseMenuCanvas.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                PauseMenuCanvas.SetActive(false);
                isPaused = false;
            }
        }

        if (PauseMenuCanvas)
        {
            OptionCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
        }
        else if (OptionCanvas)
        {
            PauseMenuCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
        }
        else if (creditsCanvas)
        {
            OptionCanvas.SetActive(false);
            PauseMenuCanvas.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenuCanvas.SetActive(false);
        isPaused = false;
    }

    public void Credits()
    {
        creditsCanvas.SetActive(true);
        PauseMenuCanvas.SetActive(false);
        if (creditsCanvas)
        {
            credits.Play();
        }
    }

    public void Options()
    {
        OptionCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mapToLoad);
    }

    public void died(string name)
    {
        FadeIn(Fade, 3);
        SceneManager.LoadScene(name);

        IEnumerator FadeIn(Image image, float duration)
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

            fadeManager.fading = true;
        }
    }
}