using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    Player player;
    public TMP_Text killText;

    public void InitialisePlayer(Player player)
    {
        this.player = player;
        UpdateStats();
    }

    void UpdateStats()
    {
        Debug.Log("Update stats");
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killText.text = kills.ToString();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            if (changedProps.ContainsKey("kills"))
            {
                UpdateStats();
            }
        }
    }
}
