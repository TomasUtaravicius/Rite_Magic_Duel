using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Photon.Realtime;
public class SpawnPoint : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int spawnpointId;

    public void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.grey;
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        Hashtable props = (Hashtable)playerAndUpdatedProps[1];

        if (!props.ContainsKey("position"))
        {
            return;
        }

        int position = (int)props["position"];

        if (position != spawnpointId)
        {
            return;
        }
        Debug.Log("ColorChange");
        GetComponent<MeshRenderer>().material.color = RiteGame.GetColor(spawnpointId);
    }

    public void OnPhotonPlayerDisconnected(Photon.Realtime.Player player)
    {
        if (!player.CustomProperties.ContainsKey("position"))
        {
            return;
        }

        int position = (int)player.CustomProperties["position"];

        if (position != spawnpointId)
        {
            return;
        }

        GetComponent<MeshRenderer>().material.color = Color.grey;
    }

    public void RestoreDefaults()
    {
        GetComponent<MeshRenderer>().material.color = Color.grey;
    }
}