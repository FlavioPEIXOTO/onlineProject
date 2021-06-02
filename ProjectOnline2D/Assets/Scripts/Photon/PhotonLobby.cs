using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    //RoomInfo[] rooms;
    public GameObject gameButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    void Start()
    {
        // Connects to master photon server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon server");
        PhotonNetwork.AutomaticallySyncScene = true;
        gameButton.SetActive(true);
    }

    public void OnGameButtonClicked()
    {
        gameButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Join Lobby");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join but its failed");
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = 5};
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
        Debug.Log("Room Create");
    }

    public void OnCancelButtonCliked()
    {
        cancelButton.SetActive(false);
        gameButton.SetActive(true); 
        PhotonNetwork.LeaveRoom();
        Debug.Log("Leave the lobby");
    }

}
