using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Connection;
using UnityEngine;

public class TeamMenuController : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject TeamUI;
    public GameObject GameUI;
    public void SelectTeam(int teamIndex)
    {

        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        if(!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId,out NetworkClient networkClient))
        {
            return;

        }

        if(!networkClient.PlayerObject.TryGetComponent<TeamPlayer>(out TeamPlayer teamPlayer))
        {

            return;
        }


        teamPlayer.SetTeamServerRpc((byte)teamIndex);

        MenuUI.SetActive(false);
        TeamUI.SetActive(false);
        GameUI.SetActive(true);
    }
}
