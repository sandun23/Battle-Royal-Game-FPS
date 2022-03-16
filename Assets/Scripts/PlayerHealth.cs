using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : NetworkBehaviour
{
  

    public NetworkVariableFloat health = new NetworkVariableFloat(100f);

    MeshRenderer[] renderers;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void ReduceHealth(float damage)
    {
        health.Value -= damage;

        if(health.Value < 1)
        {
            if(this.gameObject.tag == "Enemy")
            {

                Destroy(this.gameObject);


            }else if (this.gameObject.tag == "Player")
            {
                PlayerDieClientRPC();

                DisableAttack();

                KeepCountOfDeathClientRPC();

            }
        }
    }

    public int NumberOfPlayers = 2;
    public NetworkVariableInt numberOfDeadPlayers = new NetworkVariableInt(0);
    [ClientRpc]
    void KeepCountOfDeathClientRPC()
    {

        numberOfDeadPlayers.Value++;

        

        if (numberOfDeadPlayers.Value >= NumberOfPlayers - 1)
        {


            MLAPI.NetworkManager.Singleton.StopClient();
            MLAPI.NetworkManager.Singleton.Shutdown();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     


        }

    }
    [ClientRpc]
    void PlayerDieClientRPC()
    {
        foreach(var renderer in renderers)
        {

            renderer.enabled = false;
        }
    }


    void DisableAttack()
    {

//        PlayerAttackScript attackScript = GetComponent<PlayerAttackScript>();

   //     attackScript.enabled = false;

    }

    private void Update()
    {
        if(this.transform.position.y < -1)
        {

            KeepCountOfDeathClientRPC();
        }
    }
}
