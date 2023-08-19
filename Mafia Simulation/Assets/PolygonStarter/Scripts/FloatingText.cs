using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class  FloatingText : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    Transform WorldSpaceCanvas;
    public Vector3 offset;
    public TextMeshProUGUI moneyText;
    public bool isMine;
    private int money =1000; //default money


    void Start(){
        mainCam= Camera.main.transform;
        unit=transform.parent;
        WorldSpaceCanvas = GameObject.FindObjectOfType<Canvas>().transform;
        transform.SetParent(WorldSpaceCanvas);
        if(isMine){
            moneyText.text = "500";
        }
        else
            {moneyText.text = money.ToString();}
        
    }

     void Update()
    {
       transform.rotation=Quaternion.LookRotation(transform.position - mainCam.transform.position);
       transform.position=unit.position+offset;
       
            }

     public void UpdateMoneyText(int money)
    {
        moneyText.text =  money.ToString();

    }
}
