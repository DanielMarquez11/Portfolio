using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private MultiplayerManager _multiplayerManager;

    [SerializeField] private int _PlayerOne;
    [SerializeField] private int _PlayerTwo;
    [SerializeField] private int _PlayerThree;
    [SerializeField] private int _PlayerFour;

    private void Awake()
    {
        _multiplayerManager = FindObjectOfType<MultiplayerManager>();
    }

    public Vector2 CalculatePlayerRaycasts(GameObject playerGameobject)
    {
        print("Player Manager: Calculate player raycasts");

        int playerLayer = playerGameobject.layer;

        if (_multiplayerManager.PlayerIndex == 1 && playerLayer == _PlayerOne) // P1 (One player)
        {
            return new Vector2(Screen.width / 2f, Screen.height / 2f);
        }
        else if (_multiplayerManager.PlayerIndex == 2 && playerLayer == _PlayerOne) // P1 (Two players)
        {
            return new Vector2(Screen.width / 4f, Screen.height / 2f);
        }
        else if (_multiplayerManager.PlayerIndex == 2 && playerLayer == _PlayerTwo) // P2 (Two players)
        {
            return new Vector2(Screen.width / 4f * 3, Screen.height / 2f);
        }
        else if (_multiplayerManager.PlayerIndex == 3 && playerLayer == _PlayerOne) // P1 (Three players)
        {
            return new Vector2(Screen.width / 4f, Screen.height / 4f);
        }
        else if (_multiplayerManager.PlayerIndex == 3 && playerLayer == _PlayerTwo) // P2 (Three players)
        {
            return new Vector2(Screen.width / 4f, Screen.height / 4f * 3);
        }
        else if (_multiplayerManager.PlayerIndex == 3 && playerLayer == _PlayerThree) // P3 (Three players)
        {
            return new Vector2(Screen.width / 4f * 3, Screen.height / 4f);
        }
        else if (_multiplayerManager.PlayerIndex == 4 && playerLayer == _PlayerOne) // P1 (Four players)
        {
            return new Vector2(Screen.width / 4f, Screen.height / 4f);
        }
        else if (_multiplayerManager.PlayerIndex == 4 && playerLayer == _PlayerTwo) // P2 (Four players)
        {
            return new Vector2(Screen.width / 4f, Screen.height / 4f * 3);
        }
        else if (_multiplayerManager.PlayerIndex == 4 && playerLayer == _PlayerThree) // P3 (Four players)
        {
            return new Vector2(Screen.width / 4f * 3, Screen.height / 4f);
        }
        else if (_multiplayerManager.PlayerIndex == 4 && playerLayer == _PlayerFour) // P4 (Four players)
        {
            return new Vector2(Screen.width / 4f * 3, Screen.height / 4f * 3);
        }
        else
        {
            print("Couldnt get the player raycast!");
            return Vector2.zero;
        }
    }
}
