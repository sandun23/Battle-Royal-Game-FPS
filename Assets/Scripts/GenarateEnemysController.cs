using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenarateEnemysController : MonoBehaviour
{

    public GameObject TheEnemy;

    public int enemyCreated;
     

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemies());
    }

    
    IEnumerator CreateEnemies()
    {
        int numberOfEnemies = 10;
        float yPosition = 1.1f;

        while (enemyCreated < numberOfEnemies)
        {
            var xPosition = Random.Range(-20, 27);
            var zPosition = Random.Range(-10, 37);

           

            Instantiate(TheEnemy,new Vector3(xPosition, yPosition, zPosition),Quaternion.identity);

            yield return new WaitForSeconds(0.1f);
            enemyCreated +=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
