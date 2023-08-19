using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    protected int money=0;
    protected bool isAlive;
    protected bool inPrison=false;
    protected bool rubbed=false;
    protected float speed=50f;
    public FloatingText floatingText; // Reference to the FloatingText script
    void Start()
    {
        isAlive=true;
        
    }

    public void UpdateMoney(int newMoney)
    {
        money = newMoney
        ;
        floatingText.UpdateMoneyText(money);
    }
    
    public void Kill(Character  victim){
         if (victim != null)
        {
            Debug.Log("killed");
            victim.isAlive = false;
            Vector3 deadRotation = new Vector3(0f, 0f, 90f);
            victim.transform.rotation = Quaternion.Euler(deadRotation);
            victim.UpdateMoney(0);
            DeadCharacterManager.Instance.AddDeadCharacter(victim);
             // Get all script components attached to the victim GameObject
            Character[] scripts = victim.GetComponents<Character>();

            // Iterate through the scripts and remove any that are not the Character script
            foreach (Character script in scripts)
            {
                Debug.Log("scripttt");
                script.enabled = false;
                
            }
        }
    }


    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        // UpdateMoney(money);
    }



}
