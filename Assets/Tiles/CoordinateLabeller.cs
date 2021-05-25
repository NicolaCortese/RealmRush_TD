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
    TextMeshPro label;
    Vector2Int coord = new Vector2Int();
    Waypoint waypoint;
    
    void Awake()
    {
        label = GetComponent<TextMeshPro>();    
        waypoint = GetComponentInParent<Waypoint>();
        
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
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void DisplayCoord()
    {
        // UnityEditor Class cannot be built therefore this script will have to be moved into the Editor folder (which will be ignored in build)
        
        coord.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); 
        coord.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coord.x + "," + coord.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coord.x + "," + coord.y;
    }
}
