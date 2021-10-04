using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;


public class PendulumMove : MonoBehaviour
{
    private PhotonView PV;
    public static PendulumMove pen;

    float length = 4f;    //length of rope
    float timer = 0f;     // this timer is use for deciding movement phase 
    float speed = 0f;     // moveing speed
    int phase = 0;        // movement phase
    float phasePeriod = 0f;      //change phase everytime timer more than this period. movement function change in each phase,
    public float totalTime = 0;       //tottal movement time
    System.Random r = new System.Random();

    public TextMeshPro timeText;        //time display on screen
    public TextMeshPro lengthText;      //length display on screen

    public Transform pendulum;           
    public Transform Equipment;         //whole equiment, used for reset position
    public Transform penString;
    public Transform penBall;

    public GameObject AddLengthButton;
    public GameObject SubLengthButton;
    public GameObject StartButton;
    public GameObject StopButton;

    bool start = false;

    private void Start()
    {
        

        if (PendulumMove.pen == null)
        {
            PendulumMove.pen = this;
        }

        PV = GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lengthText.text = "Length: " + length.ToString() + "m";
        phasePeriod = (float)Math.Sqrt(length) / 2;
        speed = (float)Math.Sqrt(length) / length;

        // the algorithm that controll pendulum to move
        if (start)
        {
            timer += Time.fixedDeltaTime;
            if (timer > phasePeriod)
            {
                phase++;
                phase %= 4;
                timer = 0f;
            }

            switch (phase)
            {
                case 0:
                    pendulum.Rotate(0f, 0f, speed * (phasePeriod - timer) / phasePeriod);
                    break;

                case 1:
                    pendulum.Rotate(0f, 0f, -speed * timer / phasePeriod);
                    break;

                case 2:
                    pendulum.Rotate(0f, 0f, -speed * (phasePeriod - timer) / phasePeriod);
                    break;

                case 3:
                    pendulum.Rotate(0f, 0f, speed * timer / phasePeriod);
                    break;
            }

            //    totalTime += Time.fixedDeltaTime;
            PV.RPC("RPC_TotalTimer", RpcTarget.AllBuffered);
            timeText.text = "Total Time : " + String.Format("{0:0.00}", totalTime);
            
        }
    }

    // Send RPC function to all users though photon network. 
    public void OnStartButtonClick()
    {
        Debug.Log("Enter on Start button click function");

        PV.RPC("RPC_OnStartButtonClick", RpcTarget.AllBuffered);
    }


    public void OnStopButtonClick()
    {
        PV.RPC("RPC_OnStopButtonClick", RpcTarget.AllBuffered);
    }

    
    public void OnResetButtonClick()
    {
        PV.RPC("RPC_OnResetButtonClick", RpcTarget.AllBuffered);
    }

    public void OnLengthAddButtonClick()
    {
        PV.RPC("RPC_OnLengthAddButtonClick", RpcTarget.AllBuffered);
    }

    public void OnLengthSubButtonClick()
    {
        PV.RPC("RPC_OnLengthSubButtonClick", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_OnStartButtonClick()
    {
        start = true;
        StartButton.SetActive(false);
        StopButton.SetActive(true);
        AddLengthButton.SetActive(false);
        SubLengthButton.SetActive(false);
    }

    [PunRPC]
    void RPC_OnStopButtonClick()
    {
        // add random error to timer
        float randomError = (float)r.NextDouble();
        totalTime += randomError;
        totalTime = Mathf.Round(totalTime * 100f) / 100f;
        timeText.text = "Total Time : " + String.Format("{0:0.00}", totalTime);

        start = false;
        StartButton.SetActive(true);
        StopButton.SetActive(false);

        PendulumData.penData.totalTime[PendulumData.penData.currentRow] = totalTime;
        PendulumData.penData.length[PendulumData.penData.currentRow] = length;
        PendulumData.penData.currentRow++;
        PendulumData.penData.currentRow %= 6;
    }

    [PunRPC]
    void RPC_OnResetButtonClick()
    {
        start = false;
        timer = 0;
        totalTime = 0;
        phase = 0;
        timeText.text = "Total Time : " + String.Format("{0:0.00}", totalTime);
        transform.position = Equipment.position + new Vector3(-0.36f, -0.44f, -2);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        AddLengthButton.SetActive(true);
        SubLengthButton.SetActive(true);
        StartButton.SetActive(true);
        StopButton.SetActive(true);
    }

    [PunRPC]
    void RPC_OnLengthAddButtonClick()
    {
        if (length <= 4.5f)
        {
            length += 0.5f;
            penString.localScale = new Vector3(0.05f, length, 0.05f);
            penString.position -= new Vector3(0f, 0.25f, 0);
            penBall.localScale = new Vector3(4f, 0.4f / length, 4f);
        }
    }

    [PunRPC]
    void RPC_OnLengthSubButtonClick()
    {
        if (length >= 2.5f)
        {
            length -= 0.5f;
            penString.localScale = new Vector3(0.05f, length, 0.05f);
            penString.position += new Vector3(0f,0.25f, 0f);
            penBall.localScale = new Vector3(4f, 0.4f / length, 4f);
        }
    }

    [PunRPC]
    void RPC_TotalTimer()
    {
        totalTime += Time.fixedDeltaTime;
    }
}
