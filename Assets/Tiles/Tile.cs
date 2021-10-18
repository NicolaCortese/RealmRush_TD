using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable{get{return isPlaceable;}
    }
    [SerializeField] Tower TowerPrefab;
    Tower tower;
    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

   
   private void Awake() 
   {
       tower = GetComponent<Tower>();
       gridManager = FindObjectOfType<GridManager>();
       pathfinder = FindObjectOfType<Pathfinder>();
   }
   
   
    private void Start() 
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = TowerPrefab.CreateTower(TowerPrefab, transform.position);
            if(isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
            
            
            
        
        }
    }
}
