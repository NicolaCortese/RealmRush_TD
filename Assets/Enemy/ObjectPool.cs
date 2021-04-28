using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0,50)] int poolSize = 5;
     [SerializeField][Range(0.1f,5f)] float spawnTime = 2f;
    
    GameObject[]pool;

    void Awake() 
    {
      PopulatePool();   
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

   IEnumerator SpawnEnemies()
    {
        
            while(Application.isPlaying)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(spawnTime);
            }

    }
         
        void EnableObjectInPool()
        {
            for(int i = 0; i<pool.Length; i++)
            {
                if(pool[i].activeInHierarchy == false)
                {
                    pool[i].SetActive(true);
                    return;
                }
                
            }
        }  
}
