using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListManager : MonoBehaviour
{
    [SerializeField]
    private float playerListRefreshCooldown = 2;

    [SerializeField]
    private GameObject playerListItemPrefab;

    [SerializeField]
    private Transform playerListLocation;

    private List<GameObject> playerListItems = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(AutoRefreshPlayerList());
    }

    private void BuildPlayerList()
    {
        foreach (GameObject item in playerListItems)
        {
            Destroy(item);
        }

        foreach (Player player in GameManager.PlayersList.Values)
        {
            GameObject item = Instantiate(playerListItemPrefab, playerListLocation);

            item.transform.name = player.name;
            if (!player.isDead)
                item.GetComponentInChildren<Text>().text = player.name;
            else
                item.GetComponentInChildren<Text>().text = player.name + " | DEAD";

            playerListItems.Add(item);
        }
    }

    public IEnumerator AutoRefreshPlayerList()
    {
        yield return new WaitForSeconds(playerListRefreshCooldown);
        BuildPlayerList();
        StartCoroutine(AutoRefreshPlayerList());
    }
}
