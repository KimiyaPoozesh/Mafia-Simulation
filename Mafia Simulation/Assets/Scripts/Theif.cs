using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theif : Character
{
  public List<Character> waypoints; // List of characters to assassinate
    private int currentWaypointIndex = -1; // Initialize to -1

    private void Start()
    {
        money=0;
        MoveToNextWaypoint();
    }

    private void Update()
    {
        UpdateMoney(money);
        if (inPrison || currentWaypointIndex < 0 || currentWaypointIndex >= waypoints.Count)
            return;
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
        while (Vector3.Distance(transform.position, targetTransform.position) > 5f)
        {
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            yield return null;
        }

            Robbery(waypoints[currentWaypointIndex]);
            
        // else{
        //     Buildings house = targetTransform.GetComponent<Buildings>();
        //     house.UpdateMoney(100,false);
        //     UpdateMoney(100);
        // }
        // Wait for a short time before moving to the next target
        yield return new WaitForSeconds(1f);

        MoveToNextWaypoint();
    }

}
