using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPage, optionPage, CreditPage, loadingPage, Title, Shop;
    [SerializeField] private GameObject Graphics, Audio, Controls, GamePlay, Language, Display, Accessibility;
    [SerializeField] private GameObject Layout, Layout2;

    private float TweenTime = 1.2f;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void OptionButton()
    {
        LeanTween.moveLocal(optionPage, new Vector3(0, 0, 0), TweenTime).setDelay(0.21f)
            .setEase(LeanTweenType.easeInOutCubic);
        LeanTween.moveLocalX(mainPage, -800, TweenTime).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.moveLocalX(Title, -2000, TweenTime).setEase(LeanTweenType.linear);
    }

    public void BackOption()
    {
        LeanTween.moveLocal(optionPage, new Vector3(0, 1200, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.moveLocal(mainPage, new Vector3(-103, 131, 0), TweenTime).setDelay(1.24f)
            .setEase(LeanTweenType.easeInOutBounce);
        LeanTween.moveLocal(Title, new Vector3(-398, 340, 0), TweenTime).setDelay(1.24f).setEase(LeanTweenType.linear);
    }

    public void RightButton()
    {
        LeanTween.moveLocalX(Layout, 1300, 1.1f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.moveLocalX(Layout2, -206.85f, 1.1f).setEase(LeanTweenType.easeInOutSine);
    }

    public void LeftButton()
    {
        LeanTween.moveLocalX(Layout2, -2000, 1.1f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.moveLocalX(Layout, -580, 1.1f).setEase(LeanTweenType.easeInOutSine);
    }

    public void AudioSettings()
    {
        LeanTween.moveLocalY(Audio, 0, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 1200, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }

    public void BackSettingButtonFromAudio()
    {
        LeanTween.moveLocalY(Audio, -1300, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 0, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }

    public void LanguageSettings()
    {
        LeanTween.moveLocalY(Language, 0, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 1200, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }

    public void BackSettingButtonFromLanguage()
    {
        LeanTween.moveLocalY(Language, -1300, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 0, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }

    public void ShopMenu()
    {
        LeanTween.moveLocalX(Shop, 0, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocalX(mainPage, -800, TweenTime).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.moveLocalX(Title, -2000, TweenTime).setEase(LeanTweenType.linear);
    }

    public void BackMenuButtonFromShop()
    {
        LeanTween.moveLocalX(Shop, 2175, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(mainPage, new Vector3(-103, 131, 0), TweenTime).setDelay(1.24f)
            .setEase(LeanTweenType.easeInOutBounce);
        LeanTween.moveLocal(Title, new Vector3(-398, 340, 0), TweenTime).setDelay(1.24f).setEase(LeanTweenType.linear);
    }

    public void AccessibilitySettings()
    {
        LeanTween.moveLocalY(Accessibility, 0, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 1200, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }

    public void BackSettingButtonFromAccessibility()
    {
        LeanTween.moveLocalY(Accessibility, -1300, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 0, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }
    
    public void KeyBindsSettings()
    {
        LeanTween.moveLocalY(Controls, 0, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 1200, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }

    public void BackSettingButtonFromKeyBinds()
    {
        LeanTween.moveLocalY(Controls, -1300, 1.1f).setEase(LeanTweenType.once);
        LeanTween.moveLocal(optionPage, new Vector3(0, 0, 0), TweenTime).setEase(LeanTweenType.easeInOutCubic);
    }
}