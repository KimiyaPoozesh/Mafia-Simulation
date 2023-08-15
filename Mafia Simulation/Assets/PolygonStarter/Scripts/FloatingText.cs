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
    private int money =10000; //default money


    void Start(){
        mainCam= Camera.main.transform;
        unit=transform.parent;
        WorldSpaceCanvas = GameObject.FindObjectOfType<Canvas>().transform;
        transform.SetParent(WorldSpaceCanvas);
        
    }

     void Update()
    {
       transform.rotation=Quaternion.LookRotation(transform.position - mainCam.transform.position);
       transform.position=unit.position+offset;
        moneyText.text = money.ToString();    }

     public void UpdateMoneyText(int money)
    {
        this.money=money;
    }
}
