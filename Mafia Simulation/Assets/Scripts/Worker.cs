using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Character
{
    public List<Transform> waypoints;
    // public Buildings house;
    private float waypointStayDuration = 5f;
    private int currentWaypointIndex;
    private Transform currentWaypoint; 
    private bool isMoving = false;
    private float timer = 0f;
    private float countdownDuration = 5f;
    private int houseMoney=1000;
    
    
    private void Start()
    {
        money=500;
        isAlive =true;
        robbed=false;
        // house.UpdateMoney(houseMoney);
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
        UpdateMoney(money);
        if (robbed)
        {
            timer += Time.deltaTime;

            if (timer >= countdownDuration)
            {
                // 5 seconds have passed, do something here
                Debug.Log("5 seconds have passed!");
                
                // Reset the timer
                timer = 0f;
            }
        }
        if (!isMoving)
            return;
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
        money+=100;
        Buildings house = currentWaypoint.GetComponent<Buildings>();
        house.UpdateMoney(100,false);
        isMoving = true;
    }
}
