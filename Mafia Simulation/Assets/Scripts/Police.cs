using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Character
{
    public List<Transform> waypoints;
    private float waypointStayDuration = 1f;
    private int currentWaypointIndex;
    private List<Character> criminals = new List<Character>();
    private Transform currentWaypoint;
    private bool isMoving = false;
    private bool follow=false;
    
    private void Start()
    {
        speed=100f;
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
    if(DeadCharacterManager.Instance.GetCriminalCharacters().Count>0){
        
            foreach (Character criminal in DeadCharacterManager.Instance.GetCriminalCharacters()){
                criminals.Add(criminal); 
            }
            follow=true;
        }
    if (follow)
    {
        if (currentWaypointIndex >= 0 && currentWaypointIndex < criminals.Count)
        {
            MoveToCriminal();
        }
        else
        {
            follow = false;
        }
    }
    else
    {
        // Your regular patrol behavior
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
}


    private void MoveToCriminal()
{
     if (criminals.Count == 0 || follow == false)
    {
        return;
    }
    currentWaypointIndex = Random.Range(0, criminals.Count);
    Character currentWaypointCharacter = criminals[currentWaypointIndex];
    StartCoroutine(MoveTowardsWaypoint(currentWaypointCharacter.transform));
}

private IEnumerator MoveTowardsWaypoint(Transform targetTransform)
{
    Vector3 initialPosition = transform.position;
    Vector3 targetPosition = targetTransform.position;
    Quaternion targetRotation = Quaternion.LookRotation(targetPosition - initialPosition);
    Character criminalToCatch = criminals[currentWaypointIndex];

    float startTime = Time.time;
    float journeyDuration = Vector3.Distance(initialPosition, targetPosition) / speed;

    while (Time.time < startTime + journeyDuration)
    {
        float fractionOfJourney = (Time.time - startTime) / journeyDuration;
        transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
        yield return null;
    }

    // Ensure the final position and rotation are set correctly
    transform.position = targetPosition;
    transform.rotation = targetRotation;
    follow = false;
    Catch(criminalToCatch);
    criminals.Remove(criminalToCatch);
    DeadCharacterManager.Instance.RemoveCriminalCharacter(criminalToCatch);
    yield return new WaitForSeconds(1f);
    currentWaypointIndex = -1;
    
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
