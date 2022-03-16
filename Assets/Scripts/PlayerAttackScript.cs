using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using UnityEngine;

public class PlayerAttackScript : NetworkBehaviour
{


    NetworkVariableBool attaking = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly

    }, false);


    public ParticleSystem BulletParticleSystem;

    private ParticleSystem.EmissionModule em;

    float AttackTimer = 0f;

    private float FiringRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        em.enabled = false;
        em = BulletParticleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            attaking.Value = Input.GetKeyDown("space");

            AttackTimer += Time.deltaTime;

            if (attaking.Value && AttackTimer >= 1f / FiringRate)
            {
                AttackTimer = 0f;
                AttackServerRPC();
            }
        }


        em.rateOverTime = attaking.Value ? FiringRate : 0f;
    }

    [ServerRpc]
    void AttackServerRPC()
    {
        Ray ray = new Ray(BulletParticleSystem.transform.position, BulletParticleSystem.transform.forward);

        float raycastLength = 100f;

        if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
        {
            var playerhitScript = hit.collider.GetComponent<PlayerHealth>();

            if (playerhitScript != null)
            {
                float reduceHealthBy = 10f;

                playerhitScript.ReduceHealth(reduceHealthBy);
            }
        }
    }
}
