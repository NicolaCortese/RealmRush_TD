using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 25;
    [SerializeField] int towerDelay = 2;
    
    void Start()
    {
     StartCoroutine(Build());   
    }
    
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

       if(bank == null)
       {
           return false;
       }
            
         if(bank.CurrentBalance>=towerCost)
         {   
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(towerCost);
            return true;
         }
        
        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(towerDelay);
        }

        
        
        
        
        
    }
}
