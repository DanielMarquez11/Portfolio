using System;
using System.Collections;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("References")] [SerializeField]
    private GameObject _Player;

    [SerializeField] private Animator _Animator;
    [SerializeField] private Animation _RoundSwitch;
    [SerializeField] private TextMeshProUGUI _RoundSwitchText;
    [SerializeField] private TextMeshProUGUI _EnemyRemainingText;
    [SerializeField] GameObject _Startpointplayer;
    [SerializeField] GameObject _EndPointPlayer;

    private Coroutine onlevelcoroutineHandler;

    [SerializeField] private float _OnLevelEnterlerpTime;

    public GameObject[] EnemyTypes;
    public Transform[] SpawnPoints;
    public int EnemiesPerWave = 5;
    public float TimeBetweenWaves = 5f;

    public int EnemiesRemaining;
    public int CurrentWave = 0;
    private bool WaveInProgress = false; // Add this variable to check if a wave is in progress

    private void Awake(){
        if (Instance != null && Instance != this)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy(){
        if (onlevelcoroutineHandler != null)
        {
            StopCoroutine(onlevelcoroutineHandler);
        }
    }

    public void Start(){
        if (onlevelcoroutineHandler != null)
        {
            StopCoroutine(onlevelcoroutineHandler);
        }

        // OnlevelcoroutineHandler = StartCoroutine(Onlevelcoroutine());
        StartCoroutine(NextWave());
    }

    public void EnemyDestroyed(){
        enemiesRemaining--;
        _EnemyRemainingText.text = "" + enemiesRemaining;
        if (enemiesRemaining <= 0 && !waveInProgress)
        {
            waveInProgress = true; // Set waveInProgress to true to prevent multiple NextWave calls
            LevelUp.Instance.LevelUpPlayer();
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave(){
        waveInProgress = true; // Set waveInProgress to true to indicate a wave is in progress
        _RoundSwitch.Play();
        int waveCount = currentWave + 1;
        _RoundSwitchText.text = "Next Wave: " + waveCount;
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        enemiesRemaining = enemiesPerWave; // Increase the number of enemies with each wave
        _EnemyRemainingText.text = "" + enemiesRemaining;
        int enemiestospawn = enemiesPerWave;
        waveInProgress = true;

        // Add Wave to Score
        ScoreManager _scoreManager = ScoreManager.instance;
        if (_scoreManager != null)
        {
            _scoreManager.AddWave();
        }

        for (int i = 0; i < enemiestospawn; i++)
        {
            SpawnEnemy(spawnPoints[Random.Range(0, spawnPoints.Length)]);
            yield return new WaitForSeconds(Random.Range(1, 5)); // Wait 1 second between each enemy spawn
        }

        waveInProgress = false; // Set waveInProgress to false to indicate the wave has finished spawning

        switch (currentWave)
        {
            case 1:
                enemiesPerWave = 5;
                break;
            case 2:
                enemiesPerWave = 10;
                break;

            case 6:
                enemiesPerWave = 20;
                break;
        }
    }

    void SpawnEnemy(Transform spawnPoint){
        GameObject enemyPrefab = GetRandomEnemyPrefab();
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    GameObject GetRandomEnemyPrefab(){
        int rand = Random.Range(0, enemyTypes.Length);

        // Fallback in case of error
        return enemyTypes[rand];
    }

    public int GetCurrentWave(){
        return currentWave;
    }
}