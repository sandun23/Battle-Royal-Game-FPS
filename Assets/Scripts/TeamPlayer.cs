using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class TeamPlayer : NetworkBehaviour
{

    [SerializeField]
    private Renderer teamColorRenderer;

    [SerializeField]
    private Color[] teamColors;

    private NetworkVariableByte teamIndex = new NetworkVariableByte();

    [ServerRpc]
    public void SetTeamServerRpc(byte newTeamIndex)
    {

        if(newTeamIndex > 1)
        {
            return;
        }
        teamIndex.Value = newTeamIndex;

        if (newTeamIndex == 0)
        {
            Debug.Log("0 team");

        }else if (newTeamIndex == 1)
        {
            Debug.Log("1 team");
        }
        else
        {
            Debug.Log(" no team");
        }

    }

    private void OnEnable()
    {
        teamIndex.OnValueChanged += OnTeamChanged;
    }

    private void OnDisable()
    {
        teamIndex.OnValueChanged -= OnTeamChanged;
    }


    private void OnTeamChanged(byte oldTeamIndex, byte NewTeamIndex)
    {

        if (!IsClient) { return; }

        teamColorRenderer.material.SetColor("_BaseColor", teamColors[NewTeamIndex]);


    }

}
