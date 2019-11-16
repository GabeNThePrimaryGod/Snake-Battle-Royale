using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum GameStatus { WaitingForHost, InProgress, Ended }

public class GameManager : NetworkBehaviour
{
    public static Dictionary<string, Player> PlayersList = new Dictionary<string, Player>();
    public static Player LocalPlayer;
    public static GameStatus gameStatus = GameStatus.WaitingForHost;

    public static void SetGameStatus(GameStatus status)
    {
        gameStatus = status;

        if (gameStatus == GameStatus.InProgress)
        {
            Debug.Log("Starting Game");
            LocalPlayer.LocalPlayerStartGameSetup();
        }
        if (gameStatus == GameStatus.Ended)
        {

        }
    }

    public int PlayersAlive = 0;
    [SerializeField]
    private bool onePlayerMode = false;

    private void Update()
    {
        if(gameStatus == GameStatus.InProgress)
        {
            int playersAlive = 0;

            foreach (Player player in PlayersList.Values)
                if (!player.isDead)
                    playersAlive++;
            
            PlayersAlive = playersAlive;

            if(playersAlive == 1 && !onePlayerMode)
            {
                foreach (Player player in PlayersList.Values)
                {
                    if(!player.isDead)
                        LocalPlayer.GetComponent<PlayerUIManager>().EndGameMessage(player);
                }

                SetGameStatus(GameStatus.Ended);
            }
        }
    }
}