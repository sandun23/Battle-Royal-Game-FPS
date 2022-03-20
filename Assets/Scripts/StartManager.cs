using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class StartManager : MonoBehaviour
{
  //  public GameObject gameUI;


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {

            ShowStartButoons();
           // gameUI.SetActive(false);//

        }
    
//        else
//        {

//            ShowConnectedLables();


//}

GUILayout.EndArea();

    }


    static void ShowStartButoons()
    {

        if (GUILayout.Button("Join")) NetworkManager.Singleton.StartClient();

        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();


        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();

    }


    //static void ShowConnectedLables()
    //{

    //    if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change "))
    //    {

    //        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkedClient))
    //        {

    //            var player = networkedClient.PlayerObject.GetComponent<StartPlayer>();

    //            if (player)
    //            {
    //                player.Move();

    //            }

    //        }

    //    }

    //}

}
