using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;


public class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    private JoinRoomDelegate joinRoomCallBack;

    [SerializeField]
    private Text roomNameText;

    private MatchInfoSnapshot match;

    public void Setup(MatchInfoSnapshot match, JoinRoomDelegate joinRoomCallBack) {
        this.match = match;
        this.joinRoomCallBack = joinRoomCallBack;
        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom() {
        joinRoomCallBack.Invoke(match);
    }
}
