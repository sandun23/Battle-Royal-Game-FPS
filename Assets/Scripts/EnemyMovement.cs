using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    CharacterController controller;

    float heading;

    public float DirectionChangeInterval = 1f;

    public float maxHeadingChange = 30;

    Vector3 tragetRotation;

    public float speed = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();

        heading = Random.Range(0,360);

        transform.eulerAngles = new Vector3(0, heading, 0);


        StartCoroutine(NewHeading());


    }


    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRouting();


            yield return new WaitForSeconds(DirectionChangeInterval);

        }


    }

   

    void NewHeadingRouting()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0 , 360);


        var ceiling = Mathf.Clamp(heading + maxHeadingChange, 0, 360);


        heading = Random.Range(floor, ceiling);

        tragetRotation = new Vector3(0, heading, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, tragetRotation, Time.deltaTime * DirectionChangeInterval);


        var forward = transform.TransformDirection(Vector3.forward);

        controller.SimpleMove(forward * speed);


    }
}
