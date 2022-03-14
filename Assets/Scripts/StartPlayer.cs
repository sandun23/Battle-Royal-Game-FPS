using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class StartPlayer : NetworkBehaviour
{

   
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,

        ReadPermission = NetworkVariablePermission.Everyone

    });


    public void Move()
    {

        if (NetworkManager.Singleton.IsServer)
        {
            Vector3 randomPosition = GetRandomPositionOnPlane();

            transform.position = randomPosition;


            Position.Value = randomPosition;

        }
        else
        {

            SubmitPositionRequestServerRpc();

        }

    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default )
    {

        Position.Value = GetRandomPositionOnPlane(); 

    }

    static Vector3 GetRandomPositionOnPlane()
    {
        float MinimumPosition = -4f;
        float MaximumPosition = 4f;


        var xPosition = Random.Range(MinimumPosition, MaximumPosition);

        var yPosition = 1f;

        var zPosition = Random.Range(MinimumPosition, MaximumPosition);


        return new Vector3(xPosition, yPosition, zPosition);


    }


    void Update()
    {
        transform.position = Position.Value;
    }
}
