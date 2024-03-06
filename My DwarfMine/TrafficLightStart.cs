using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightStart : MonoBehaviour
{
    [SerializeField] private GameObject[] Traffic_Lights;
    [SerializeField] private Car Car;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager._timerStarted = false;
        Car.CanMove = false;
        for (int i = 0; i < Traffic_Lights.Length; i++)
        {
            Traffic_Lights[i].SetActive(false);
        }
    }


    private void Update()
    {
        StartCoroutine(StartTimerGame());
    }

    private IEnumerator StartTimerGame()
    {
        yield return new WaitForSeconds(3);
        Traffic_Lights[2].SetActive(true);
        yield return new WaitForSeconds(1);
        Traffic_Lights[1].SetActive(true);
        yield return new WaitForSeconds(1);
        Traffic_Lights[0].SetActive(true);
        Car.CanMove = true;
        _gameManager._timerStarted = true;
    }
}
