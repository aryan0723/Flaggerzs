using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public RoomInfo info;
    public void Setup(RoomInfo roomInfo)
    {
        info = roomInfo;
        text.text = roomInfo.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);

    }

}

