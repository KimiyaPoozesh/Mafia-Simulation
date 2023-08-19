using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
     public FloatingText buildingText;
    private int housemoney=1000;
    private int mineMoney=500;

    public void UpdateMoney(int reduced, bool isMine)
    {
        if(housemoney<0)
            buildingText.UpdateMoneyText(000000000);
        else{
            if(!isMine)
                {housemoney-=reduced;
                buildingText.UpdateMoneyText(housemoney);
    }
            else{
                mineMoney-=reduced;
                buildingText.UpdateMoneyText(mineMoney);
    }
            }
            }
        }
       


   

