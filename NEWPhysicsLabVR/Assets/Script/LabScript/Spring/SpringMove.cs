using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SpringMove : MonoBehaviour
{
    //public GameObject weightObject; //the weight object
    private PhotonView PV;
    public static SpringMove spr;

    float Springlength = 40.0f; //length of spring
    public float ObjWeight = 50;   // the weight of the object1


    public TextMeshPro Object1WeightText;        //Object1 weight display on screen
    public TextMeshPro lengthText;      //length display on screen


    public Transform spring;
    public Transform Equipment;           //whole equiment, used for reset position
    public Transform object1;


    public GameObject AddObject1WeightButton;
    public GameObject SubObject1WeightButton;
    public GameObject StartButton;
    public GameObject StopButton;

    bool start = false;


    // Start is called before the first frame update
    private void Start()
    {
        if (SpringMove.spr == null)
        {
            SpringMove.spr = this;
        }

        PV = GetComponent<PhotonView>();
        AddObject1WeightButton.SetActive(false);
        SubObject1WeightButton.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Object1WeightText.text = "Object1 Wight : " + ObjWeight.ToString() + "g";
        lengthText.text = "Length : " + Springlength.ToString() + "cm";
    }

    public void OnStartButtonClick()
    {
        Debug.Log("Enter on Start button click function");

        PV.RPC("RPC_OnStartButtonClick", RpcTarget.AllBuffered);
    }


    public void OnStopButtonClick()
    {
        PV.RPC("RPC_OnStopButtonClick", RpcTarget.AllBuffered);
    }


   /* public void OnResetButtonClick()
    {
        PV.RPC("RPC_OnResetButtonClick", RpcTarget.AllBuffered);
    }*/

    public void OnWeight1AddButtonClick()
    {
        PV.RPC("RPC_OnWeight1AddButtonClick", RpcTarget.AllBuffered);
    }

    public void OnWeight1SubButtonClick()
    {
        PV.RPC("RPC_OnWeight1SubButtonClick", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_OnStartButtonClick()
    {
        start = true;
        AddObject1WeightButton.SetActive(true);
        SubObject1WeightButton.SetActive(true);
    }

    [PunRPC]
    void RPC_OnStopButtonClick()
    {
        start = false;
        SpringData.spData.weight[SpringData.spData.currentSpr1] = ObjWeight;
        SpringData.spData.length[SpringData.spData.currentSpr1] = Springlength;
        SpringData.spData.currentSpr1++;
        SpringData.spData.currentSpr1 %= 3;
        AddObject1WeightButton.SetActive(false);
        SubObject1WeightButton.SetActive(false);
    }

   /* [PunRPC]
    void RPC_OnResetButtonClick()
    {
        start = false;
        ObjWeight = 50;
        Springlength = 40.0f;
        Object1WeightText.text = "Object1 Wight : " + ObjWeight.ToString() + "g";
        lengthText.text = "Length : " + Springlength.ToString() + "cm";
        transform.position = spring.position + new Vector3(-0.01f, 28.3f, -0.51f);
        //spring.position = new Vector3(-0.008f,28.3f, -0.508f);
        object1.position = new Vector3(-0.218f, 5.43f, 13.013f);
       // spring.localScale = new Vector3(0.333f, 0.25f, 0.333f);
        AddObject1WeightButton.SetActive(true);
        SubObject1WeightButton.SetActive(true);
    }*/
    
    [PunRPC]
    void RPC_OnWeight1AddButtonClick()
    {
        if (Springlength >= 40)
        {
            ObjWeight += 50;
            Springlength += 2f;
            spring.localScale += new Vector3(0f, 0.05f, 0f);
            spring.position -= new Vector3(0f, 4f, 0f);
            object1.position -= new Vector3(0f, 0.15f, 0f);

        }
    }


   [PunRPC]
    void RPC_OnWeight1SubButtonClick()
    {
        if (Springlength > 40)
        {
            ObjWeight -= 50;
            Springlength -= 2f;
            spring.localScale -= new Vector3(0f, 0.05f, 0f);
            spring.position += new Vector3(0f, 0.2f, 0f);
            object1.position += new Vector3(0f, 0.15f, 0f);
        }

    }

    /*public void weightPointerClick()
    {
        //object1.transform.position = new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z);
    }*/

}



