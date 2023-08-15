using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    protected int money=0;
    protected bool isAlive=true;
    protected bool inPrison=false;
    protected bool attacked=false;
    protected float speed=10f;
    private List<Transform> waypoints;
    public FloatingText floatingText; // Reference to the FloatingText script

    public void UpdateMoney(int newMoney)
    {
        money = newMoney
        ;
        floatingText.UpdateMoneyText(money);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney(2500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
