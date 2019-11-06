using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;


public class joinGame : MonoBehaviour {

    NetworkManager networkManager;
    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    private void Start() {

        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
            networkManager.StartMatchMaker();
        RefreshRoomList();
    }

    public void RefreshRoomList() {

        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        status.text = "Refeshing ...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {

        status.text = "";
        if (matchList == null) {
            status.text = "Couldn't found match list";
            return;
        }
        
        foreach (MatchInfoSnapshot match in matchList) {            //coeur du stystem

            GameObject _roomListTitemGO = Instantiate(roomListItemPrefab);
            _roomListTitemGO.transform.SetParent(roomListParent);
            _roomListTitemGO.transform.localScale = new Vector3(1f, 1f, 1f);

            RoomListItem _roomListItem = _roomListTitemGO.GetComponent<RoomListItem>();
            if (_roomListItem != null)
                _roomListItem.Setup(match, JoinRoom);

            roomList.Add(_roomListTitemGO);
        }

        if (roomList.Count == 0)
            status.text = "No rooms available";
    }

    private void ClearRoomList() {
        for (int i = 0; i < roomList.Count; i++)
            Destroy(roomList[i]);
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot match) {

        Debug.Log("Joining " + match.name);
        networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining ...";
        
        //crée une coroutine pour le time out
    }
}
