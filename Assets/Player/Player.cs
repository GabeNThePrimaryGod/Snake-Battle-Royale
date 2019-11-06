using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class Player : NetworkBehaviour
{
    [SyncVar]
    public string PlayerName;

    public int score = 1;

    public string netId;

    public bool isNamed = false;
    
    public void Start()
    {
        netId = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerName = "UnamedPlayer " + netId;

        if (isLocalPlayer)
        {
            GameManager.LocalPlayer = this;
            Debug.Log("localPlayer is " + PlayerName);
        }
    }

    private void Update()
    {
        transform.name = PlayerName;
    }

    [Header("Death :")]

    [SerializeField]
    private GameObject loot;

    [SerializeField]
    private List<GameObject> goDestroyOnDeath = new List<GameObject>();

    [SerializeField]
    private List<Component> componentDestroyOnDeath = new List<Component>();

    public bool isDead = false;

    public void Die(string killerName)
    {
        isDead = true;
        GameManager.LocalPlayer.GetComponent<PlayerUIManager>().AddKillFeedEntry(killerName + " => " + PlayerName);

        GameObject playerLoot = Instantiate(loot);
        playerLoot.transform.position = transform.position;
        playerLoot.GetComponent<LootData>().LootSize = GetComponentInChildren<PlayerGrowingMotor>().tails.Count;
        playerLoot.GetComponent<LootData>().playerName = PlayerName;

        foreach (GameObject go in goDestroyOnDeath)
            Destroy(go);

        foreach (Component component in componentDestroyOnDeath)
            Destroy(component);

        //CmdDie(killerName);
    }

    [Command]
    private void CmdDie(string killerName)
    {
        RpcDie(killerName);
    }

    [ClientRpc]
    private void RpcDie(string killerName)
    {
        isDead = true;
        GameManager.LocalPlayer.GetComponent<PlayerUIManager>().AddKillFeedEntry(killerName + " => " + PlayerName);

        GameObject playerLoot = Instantiate(loot);
        playerLoot.transform.position = transform.position;
        playerLoot.GetComponent<LootData>().LootSize = GetComponentInChildren<PlayerGrowingMotor>().tails.Count;
        playerLoot.GetComponent<LootData>().playerName = PlayerName;

        foreach (GameObject go in goDestroyOnDeath)
            Destroy(go);

        foreach (Component component in componentDestroyOnDeath)
            Destroy(component);
    }

    private void OnDestroy()
    {
        if (isNamed && !isDead)
        {
            GameManager.PlayersList.Remove(PlayerName);
        }
    }

    #region GameStarting

    [Header("Enable OnStart :")]

    [SerializeField]
    private List<GameObject> gameObjectsEnableOnGameStart = new List<GameObject>();

    [SerializeField]
    private List<Behaviour> componentsEnableOnGameStart = new List<Behaviour>();

    [Header("Disable OnStart :")]

    [SerializeField]
    private List<GameObject> gameObjectsDisableOnGameStart = new List<GameObject>();

    [SerializeField]
    private List<Behaviour> componentsDisableOnGameStart = new List<Behaviour>();

    [Server]
    public void StartGame()
    {
        CmdStartGame();
    }

    [Command]
    private void CmdStartGame()
    {
        RpcStartGame();
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        GameManager.SetGameStatus(GameStatus.InProgress);
    }

    public void LocalPlayerStartGameSetup()
    {
        foreach (GameObject gameObject in gameObjectsEnableOnGameStart)
            gameObject.SetActive(true);

        foreach (Behaviour component in componentsEnableOnGameStart)
            component.enabled = true;

        foreach (GameObject gameObject in gameObjectsDisableOnGameStart)
            gameObject.SetActive(false);

        foreach (Behaviour component in componentsDisableOnGameStart)
            component.enabled = false;
    }

    #endregion GameStarting
}
