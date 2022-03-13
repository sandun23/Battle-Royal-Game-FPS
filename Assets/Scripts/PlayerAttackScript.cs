using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{

    bool attaking = false;
    public ParticleSystem BulletParticleSystem;

    private ParticleSystem.EmissionModule em;

    float AttackTimer = 0f;

    private float FiringRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        em = BulletParticleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        attaking = Input.GetMouseButton(0);

        AttackTimer += Time.deltaTime;

        if (attaking && AttackTimer >= 1f/FiringRate)
        {
            AttackTimer = 0f;
            Attack();
        }

        em.rateOverTime = attaking ? FiringRate : 0f;
    }


    void Attack()
    {
        Ray ray = new Ray(BulletParticleSystem.transform.position, BulletParticleSystem.transform.forward);

        float raycastLength = 100f;

        if (Physics.Raycast(ray,out RaycastHit hit, raycastLength))
        {
            var playerhitScript = hit.collider.GetComponent<PlayerHealth>();

            if(playerhitScript != null)
            {
                float reduceHealthBy = 10f;

                playerhitScript.ReduceHealth(reduceHealthBy);
            }
        }
    }
}
