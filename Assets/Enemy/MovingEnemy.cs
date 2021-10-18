using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MovingEnemy : MonoBehaviour
{
    
    [SerializeField] [Range(0.1f,5f)] float enemySpeed = 1f;
    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    void OnEnable() 
    {  
        ReturnToStart();
        RecalculatePath(true);
              
    }

private void Awake() 
{
    gridManager = FindObjectOfType<GridManager>();
    pathfinder = FindObjectOfType<Pathfinder>();

}
    void Start() 
    {
    enemy = GetComponent<Enemy>();
    }
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPathFrom(coordinates);
        StartCoroutine(FollowPath());  

    }
    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 finalPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(finalPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPosition, finalPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }
        FinishPath();
    }

    void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }
}
