using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 25;
    
    
    
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

    
}
