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

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();

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

        StartCoroutine(AutoRefreshPlayerList());
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
    private Text KillFeed;

    public void AddKillFeedEntry(string entry)
    {
        KillFeed.text = entry + "\n\n" + KillFeed.text;
    }

    [Header("Player List :")]

    [SerializeField]
    private float playerListRefreshCooldown = 2;

    [SerializeField]
    private GameObject playerListItemPrefab;

    [SerializeField]
    private Transform playerListLocation;

    private List<GameObject> playerListItems = new List<GameObject>();

    private void BuildPlayerList()
    {
        foreach(GameObject item in playerListItems)
        {
            Destroy(item);
        }

        foreach(Player player in GameManager.PlayersList.Values)
        {
            GameObject item = Instantiate(playerListItemPrefab, playerListLocation);

            item.transform.name = player.name;
            if(!player.isDead)
                item.GetComponentInChildren<Text>().text = player.name;
            else
                item.GetComponentInChildren<Text>().text = player.name + " | DEAD";

            playerListItems.Add(item);
        }
    }

    private IEnumerator AutoRefreshPlayerList()
    {
        yield return new WaitForSeconds(playerListRefreshCooldown);
        BuildPlayerList();
        StartCoroutine(AutoRefreshPlayerList());
    }

    [SerializeField]
    private GameObject endGameMessage;

    public void EndGameMessage(Player winer)
    {
        endGameMessage.GetComponent<Text>().text = winer.PlayerName + " a fait top 1 !";
    }
}