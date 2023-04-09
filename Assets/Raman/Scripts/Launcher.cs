using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField errorTxt;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting....");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("joined Lobby HII");
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        //base.OnJoinedRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorTxt.text = "Room Creation Failed " + message;
        MenuManager.Instance.OpenMenu("error");
        //base.OnCreateRoomFailed(returnCode, message);
    }
}
