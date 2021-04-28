using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MovingEnemy : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0.1f,5f)] float enemySpeed = 1f;
    Enemy enemy;


    void OnEnable() 
    {  
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());        
    }

    void Start() 
    {
    enemy = GetComponent<Enemy>();
    }
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FindPath()
    {
        path.Clear();
        
        
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint!= null)
            {
            path.Add(waypoint);
            }
        }

    }
    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 finalPosition = waypoint.transform.position;
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
