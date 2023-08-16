using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    protected int money=0;
    protected bool isAlive=true;
    protected bool inPrison=false;
    protected bool rubbed=false;
    protected float speed=50f;
    public FloatingText floatingText; // Reference to the FloatingText script
    

    public void UpdateMoney(int newMoney)
    {
        money = newMoney
        ;
        floatingText.UpdateMoneyText(money);
    }
    
    public void Kill(Character  victim){
         if (victim != null)
        {
            victim.isAlive = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateMoney(money);
    }



}
