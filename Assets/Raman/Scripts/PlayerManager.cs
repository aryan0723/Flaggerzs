using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    GameObject controller;
    private int kills;
    public TMP_Text killText;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        killText = GameObject.FindGameObjectWithTag("flag").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (pv.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), SpawnManager.instance.GetSpawnPoint().position, SpawnManager.instance.GetSpawnPoint().rotation, 0, new object[] { pv.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        Invoke("CreateController", 2f);
    }

    public void GetKill()
    {
        pv.RPC("RPC_GetKill", RpcTarget.All);
    }

    [PunRPC]

    void RPC_GetKill()
    {
        if (pv.IsMine)
        {
            kills++;
            killText.text = kills.ToString();

        }
        //Hashtable hash = new Hashtable();
        //hash.Add("kills", kills);
        //PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

    }

    public static PlayerManager Find(Player player)
    {
        return GameObject.FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.pv.Owner == player);
    }

}
