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


    
    public IEnumerator ReleaseFromPrisonAfterDelay()
    {
        Debug.Log("in prison with meeeee");
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        Debug.Log("Assassin released from prison.");
        inPrison = false; // Set the inPrison status to false after the delay
        MoveToNextWaypoint(); // Continue normal behavior after release
    }
    
    
    
    private void Update()
    {
        // if (inPrison || currentWaypointIndex < 0 || currentWaypointIndex >= waypoints.Count)
        //     {
        //     return;}
    }

private void MoveToNextWaypoint()
{
    if(waypoints.Count==0||inPrison){
        
        return;
    }
    currentWaypointIndex = Random.Range(0, waypoints.Count);
    
    Character currentWaypointCharacter = waypoints[currentWaypointIndex];
    
    if (DeadCharacterManager.Instance.IsCharacterDead(currentWaypointCharacter))
    {
        Debug.Log("DEAD");
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

        if (!inPrison)
    {
        Kill(waypoints[currentWaypointIndex]);
        // Wait for a short time before moving to the next waypoint
        yield return new WaitForSeconds(5f);
        MoveToNextWaypoint();
    }
}


}

