using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 20;
    private string roomName = "";
    private string roomPass = "";

    private NetworkManager networkManager;

    private void Start() {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
            networkManager.StartMatchMaker();
    }

    public void SetRoomName (string name) {
        roomName = name;
    }

    public void CreateRoom() {

        if(roomName != "" && roomName != null) {

            Debug.Log("La partie " + roomName + " a été crée");
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, roomPass, "", "", 0, 0, networkManager.OnMatchCreate);
        } else
        {
            Debug.LogWarning("Room name invalid");
        }
    }

}
