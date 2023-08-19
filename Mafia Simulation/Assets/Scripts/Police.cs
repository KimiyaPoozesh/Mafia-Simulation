using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Character
{
    public List<Transform> waypoints;
    private float waypointStayDuration = 3f;
    private int currentWaypointIndex;
    private List<Character> criminals;
    private Transform currentWaypoint;
    private bool isMoving = false;
    private void Start()
    {
        if (waypoints.Count > 0)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Count);
            currentWaypoint = waypoints[currentWaypointIndex];
            isMoving = true;
        }
        else
        {
            Debug.LogWarning("No waypoints assigned.");
        }
    }



    private void Update()
    {
        if (!isMoving)
            return;
        if(DeadCharacterManager.Instance.GetDeadCharacters().Count()>0){
            foreach (Character criminal in DeadCharacterManager.Instance.GetCriminalCharacters()){
                criminals.Add(criminal);
                
            }
        }
        Vector3 direction = (currentWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
         if (direction != Vector3.zero)
        {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }
        if (Vector3.Distance(transform.position, currentWaypoint.position) <= 1f)
        {
            StartCoroutine(StayAtWaypoint());
        }
    }

    private IEnumerator StayAtWaypoint()
    {
        isMoving = false;
        yield return new WaitForSeconds(waypointStayDuration);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        currentWaypoint = waypoints[currentWaypointIndex];
        isMoving = true;
    }
}
