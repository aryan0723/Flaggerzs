using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomTF;
    public TMP_InputField joinRoomTF;

    public void OnClick_join()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
    }
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 4 }, null);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("joined Lobby");
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed To Join Room - " + message);
    }
}
