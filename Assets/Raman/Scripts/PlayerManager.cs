using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Linq;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    GameObject controller;
    private int flagCount;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        flagCount = 0;
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

    }

    public static PlayerManager Find(Player player)
    {
        return GameObject.FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.pv.Owner == player);
    }

}
