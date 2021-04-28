using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable{get{return isPlaceable;}
    }
    [SerializeField] Tower TowerPrefab;
    Tower tower;

    private void Start() 
    {
        tower = GetComponent<Tower>();
    }

    void OnMouseDown()
    {
        if(isPlaceable)
        {
            bool isPlaced = TowerPrefab.CreateTower(TowerPrefab, transform.position);
            isPlaceable = !isPlaced;
        
        }
    }
}
