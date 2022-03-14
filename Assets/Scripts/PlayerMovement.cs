using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : NetworkBehaviour
{

    CharacterController characterControllerPlayer;

    float speed = 3f;

    float pitch = 0f;

    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {

        if (!IsLocalPlayer)
        {

            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;

        }
        else
        {
            characterControllerPlayer = this.GetComponent<CharacterController>();


        }




    }

    // Update is called once per frame
    void Update()
    {

        if(IsLocalPlayer){
            MovePlayer();

            Look();

          //  Debug.Log("ss");
        }
       


        //SafeZone zone Player Die Function
        if (this.transform.position.y < -2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }


    }


    void MovePlayer()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        move = Vector3.ClampMagnitude(move, 1f);

        move = transform.TransformDirection(move);


        characterControllerPlayer.SimpleMove(move * speed);

    }

    float Sensitivity = 3f;
    float MinPitch = -45f;
    float MaxPitch = 45f;

    void Look()
    {


        float mouseX = Input.GetAxis("Mouse X") * Sensitivity;


        //float mouseX = Input.GetAxis("Mouse Y") * Sensitivity;

        transform.Rotate(0, mouseX, 0);

        pitch -= Input.GetAxis("Mouse Y") * Sensitivity;

        pitch = Mathf.Clamp(pitch, MinPitch, MaxPitch);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);

    }
}
