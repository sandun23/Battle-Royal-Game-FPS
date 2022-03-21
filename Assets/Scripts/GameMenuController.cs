using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{

    public GameObject GameUI;
    public GameObject MenuUI;
    public GameObject TeamUI;
    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {

            GameUI.SetActive(false);
            MenuUI.SetActive(true);
            TeamUI.SetActive(false);

        }
       
    }

    public void Host()
    {
        MenuUI.SetActive(false);
        TeamUI.SetActive(true);
        NetworkManager.Singleton.StartHost();

    }

    public void Client()
    {

        MenuUI.SetActive(false);
        TeamUI.SetActive(true);
        NetworkManager.Singleton.StartClient();
    }
}
