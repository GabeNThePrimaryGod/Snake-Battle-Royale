using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerUIManager : NetworkBehaviour
{
    public GameObject HostStartGameUI;
    public Text ClientGameStatusText;
    public GameObject NameSelection;

    [Header("Refrences : ")]
    private Player player;
    public KillFeedManager KillFeed;
    public PlayerListManager PlayerList;

    private void Start()
    {
        player = GetComponent<Player>();
        KillFeed = GetComponentInChildren<KillFeedManager>();
        PlayerList = GetComponentInChildren<PlayerListManager>();

        if (isServer)
        {
            HostStartGameUI.SetActive(true);
            ClientGameStatusText.gameObject.SetActive(false);
        }
        else
        {
            HostStartGameUI.SetActive(false);
            ClientGameStatusText.gameObject.SetActive(true);
        }
    }

    public void SetName(string name)
    {
        if (GameManager.PlayersList.ContainsKey(name))  
        {
            Debug.LogWarning("Name " + name + "already taken");
        }
        else
            CmdSetName(name);
    }

    [Command]
    private void CmdSetName(string name)
    {
        RpcSetName(name);
    }

    [ClientRpc]
    private void RpcSetName(string name)
    {
        player.PlayerName = name;
        NameSelection.SetActive(false);

        player.isNamed = true;
        GameManager.PlayersList.Add(player.PlayerName, player);
    }

    [SerializeField]
    private GameObject endGameMessage;

    public void EndGameMessage(Player winer)
    {
        endGameMessage.GetComponent<Text>().text = winer.PlayerName + " a fait top 1 !";
    }
}