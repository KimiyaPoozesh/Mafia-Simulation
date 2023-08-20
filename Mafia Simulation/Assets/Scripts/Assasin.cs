using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Character
{
    public List<Character> waypoints; // List of characters to assassinate
    private int currentWaypointIndex = -1; // Initialize to -1

    private void Start()
    {
        MoveToNextWaypoint();
    }

    private void Update()
    {
        if (inPrison || currentWaypointIndex < 0 || currentWaypointIndex >= waypoints.Count)
            {Debug.Log("inPrison "+inPrison);
            return;}
    }

private void MoveToNextWaypoint()
{
    if(waypoints.Count==0){
        return;
    }
    currentWaypointIndex = Random.Range(0, waypoints.Count);
    Character currentWaypointCharacter = waypoints[currentWaypointIndex];
    
    if (DeadCharacterManager.Instance.IsCharacterDead(currentWaypointCharacter))
    {
        waypoints.Remove(currentWaypointCharacter);
        MoveToNextWaypoint();
        return;
    }

    StartCoroutine(MoveTowardsWaypoint(currentWaypointCharacter.transform));
}

private IEnumerator MoveTowardsWaypoint(Transform targetTransform)
{
    while (Vector3.Distance(transform.position, targetTransform.position) > 10f)
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        yield return null;
    }

    Kill(waypoints[currentWaypointIndex]);

    // Wait for a short time before moving to the next waypoint
    yield return new WaitForSeconds(1f);

    MoveToNextWaypoint();
}


}

