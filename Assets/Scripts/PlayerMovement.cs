using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : NetworkBehaviour
{
    private PlayerMovementsInput playerInput;
    public GameObject gameUI;


    CharacterController characterControllerPlayer;

    float speed = 3f;

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
            gameUI.SetActive(true);

            playerInput = new PlayerMovementsInput();
            playerInput.Enable();
        }

    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private PlayerMovementsInput Controls
    {
        get
        {
            if(playerInput != null)
            {
                return playerInput;
            }

            return playerInput = new PlayerMovementsInput();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (IsLocalPlayer)
        {
            MovePlayer();

           // Debug.Log("inside update");

            //SafeZone zone Player Die Function
            if (this.transform.position.y < -2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }


        }


        void MovePlayer()
        {
            //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //controller.Move(move * Time.deltaTime * playerSpeed);


            Vector2 movementInput = playerInput.PlayerAction.Move.ReadValue<Vector2>();
           // Debug.Log(movementInput);
            Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);



            //move = Vector3.ClampMagnitude(move, 1f);

            //move = transform.TransformDirection(move);

            
            characterControllerPlayer.Move(move * Time.deltaTime * speed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

        }


    }
}
