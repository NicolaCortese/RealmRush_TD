using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

   [SerializeField] int health = 10;
   [Tooltip("Adds amount to health each time they die")][SerializeField] int difficultyRamp = 1;
    
    int currentHealth;

    Enemy enemy;

void OnEnable() 
{
    currentHealth = health;    
}

    void Start() 
    {
        enemy = GetComponent<Enemy>();

    }
 
    void OnParticleCollision(GameObject other) 
    {
        currentHealth--;  

        if(currentHealth == 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            health+=difficultyRamp;

        }
    }
   

}
