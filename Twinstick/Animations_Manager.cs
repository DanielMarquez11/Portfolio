using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations_Manager : MonoBehaviour
{
    [SerializeField] private GameObject StartCutsceneEnemyAmbush;
    [SerializeField] private GameObject Activate;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject[] Audioplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCutsceneEnemyAmbush.SetActive(true);
            Camera.SetActive(false);
            for (int i = 0; i < Audioplay.Length; i++)
            {
                Audioplay[i].SetActive(true);
            }

            StartCoroutine(FinishCutScene());
        }
    }

    public IEnumerator FinishCutScene()
    {
        yield return new WaitForSeconds(8);
        Camera.SetActive(true);
        StartCutsceneEnemyAmbush.SetActive(false);
        Activate.SetActive(true);
    }
}