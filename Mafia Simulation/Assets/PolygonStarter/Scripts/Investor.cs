using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investor : Character
{
    private float waypointStayDuration = 1f;
    private int _increasePercent = 2;
    private bool isInvesting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive ){
            if (!isInvesting)
            {
                UpdateMoney(money);
                StartCoroutine(StayAtWaypoint());
            }
        }
    }

    private IEnumerator StayAtWaypoint()
    {
        isInvesting = true;
        yield return new WaitForSeconds(waypointStayDuration);
        money+=100* _increasePercent;
        isInvesting = false;
    }
}
