using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    protected int money=0;
    protected bool isAlive;
    protected bool inPrison=false;
    protected bool robbed;
    protected bool isBriber=false;
    public Transform prisonLocation;
    protected float speed=50f;
    public FloatingText characteText; // Reference to the FloatingText script
    void Start()
    {
        isAlive=true;
        robbed=false;
    }

    public void UpdateMoney(int newMoney)
    {
        money = newMoney;
        characteText.UpdateMoneyText(money);
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
            DeadCharacterManager.Instance.AddCriminalCharacter(this);
             // Get all script components attached to the victim GameObject
            Character[] scripts = victim.GetComponents<Character>();
            // Iterate through the scripts and remove any that are not the Character script
            foreach (Character script in scripts)
            {
                script.enabled = false;       
            }
        }
    }


    public void Robbery(Character victim){
        
        if(victim.isAlive)
            {
            victim.UpdateMoney(victim.money-50);
            UpdateMoney(money+50);
            robbed=true;
            }
    }

    public void Catch(Character criminal){
        if(criminal.isBriber == false)
            {
            criminal.inPrison=true;
            criminal.transform.position = prisonLocation.position;
            criminal.inSentence();
            }
    }

    public void inSentence()
    {
        
        Assasin assasinComponent = GetComponent<Assasin>();
        if (assasinComponent != null)
        {
            Debug.Log(this);
            // Call the function from the Assasin script
            StartCoroutine(assasinComponent.ReleaseFromPrisonAfterDelay());        }
       
    }




}
