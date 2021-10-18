using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeller : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = new Color(1f,0.5f,0f);

    TextMeshPro label;
    Vector2Int coord = new Vector2Int();
    GridManager gridManager;
    
    void Awake()
    {
        label = GetComponent<TextMeshPro>();    
        gridManager = FindObjectOfType<GridManager>();
        
        DisplayCoord();
        UpdateObjectName();
        label.enabled = true;
    }
    
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoord();
            UpdateObjectName();
        }
        ToggleLabels();
        SetLabelColor();
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {   
        if(gridManager == null){return;}

        Node node = gridManager.GetNode(coord);
        if(node == null){return;}

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
         else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
       
        else
        {
        label.color = defaultColor;
        }
    }

    void DisplayCoord()
    {
        // UnityEditor Class cannot be built therefore this script will have to be moved into the Editor folder (which will be ignored in build)
        if(gridManager == null){return;}
        coord.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize); 
        coord.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coord.x + "," + coord.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coord.x + "," + coord.y;
    }
}
