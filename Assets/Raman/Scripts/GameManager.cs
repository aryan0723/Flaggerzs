using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPref;
    private void Start()
    {
        SpawmPlayer();
    }
    void SpawmPlayer()
    {
        PhotonNetwork.Instantiate(PlayerPref.name, PlayerPref.transform.position, PlayerPref.transform.rotation);
    }
}
