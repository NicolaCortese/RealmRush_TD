using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] float towerRange = 25f;
    [SerializeField] ParticleSystem arrowParticle;
    Transform target;


      void Start()
    {
        
        
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
        
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }

        }
        target = closestTarget;
    }

   void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position,target.position);
        weapon.LookAt(target);
        if(targetDistance < towerRange)
        {
        Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = arrowParticle.emission;
        emissionModule.enabled = isActive;
    }
}
