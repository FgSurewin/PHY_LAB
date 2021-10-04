using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ProjectileMove : MonoBehaviour
{
    private PhotonView PV;
    public static ProjectileMove pjt;

    float speed = 0f;
    float angle = 0f;
    float Gravity = -9.8f;
    private float timer;
    private float sinValue;
    private float cosValue;
    private Vector3 startPos;
    private float endY = 0;

    public TextMeshPro speedText;   
    public TextMeshPro angleText;

    public Transform cannonBall;
    public Transform Equipment;

    public GameObject AddSpeedButton;
    public GameObject SubSpeedButton;
    public GameObject AddAngleButton;
    public GameObject SubAngleButton;
    public GameObject StartButton;
    public GameObject StopButton;

    bool start = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (ProjectileMove.pjt == null)
        {
            ProjectileMove.pjt = this;
        }

        PV = GetComponent<PhotonView>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speedText.text = "Speed : " + speed.ToString() + "m/s";
        angleText.text = "Angle : " + angle.ToString() + "°";

        if (start)
        {
            timer += Time.deltaTime;

            sinValue = Mathf.Sin(Mathf.Deg2Rad * angle);
            cosValue = Mathf.Cos(Mathf.Deg2Rad * angle);

            float xOffset = speed * timer * cosValue ;
            float yOffset = speed * timer * sinValue + 0.5f * Gravity * timer * timer;
            //float zOffset = speed * timer * cosValue ;

            Vector3 endPos = startPos + new Vector3(xOffset, yOffset, 15);
            if (endPos.y < endY)
            {
                endPos.y = endY;

            }
            transform.position = endPos;
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

    public void OnSpeedAddButtonClick()
    {
        PV.RPC("RPC_OnSpeedAddButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSpeedSubButtonClick()
    {
        PV.RPC("RPC_OnSpeedSubButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAngleAddButtonClick()
    {
        PV.RPC("RPC_OnAngleAddButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAngleSubButtonClick()
    {
        PV.RPC("RPC_OnAngleSubButtonClick", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_OnStartButtonClick()
    {
        start = true;
        StartButton.SetActive(false);
        StopButton.SetActive(true);
        AddSpeedButton.SetActive(false);
        SubSpeedButton.SetActive(false);
        AddAngleButton.SetActive(false);
        SubAngleButton.SetActive(false);
    }

    [PunRPC]
    void RPC_OnStopButtonClick()
    {
        start = false;
        StartButton.SetActive(true);
        StopButton.SetActive(false);

        ProjectileData.pjtData.speed[ProjectileData.pjtData.currentRow] = speed;
        ProjectileData.pjtData.angle[ProjectileData.pjtData.currentRow] = angle;
        ProjectileData.pjtData.currentRow++;
        ProjectileData.pjtData.currentRow %= 3;
    }

    [PunRPC]
    void RPC_OnResetButtonClick()
    {
        start = false;
        timer = 0;
        speed = 0;
        angle = 0;
        transform.position = Equipment.position + new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        AddSpeedButton.SetActive(true);
        SubSpeedButton.SetActive(true);
        AddAngleButton.SetActive(true);
        SubAngleButton.SetActive(true);
        StartButton.SetActive(true);
        StopButton.SetActive(true);
    }

    [PunRPC]
    void RPC_OnSpeedAddButtonClick()
    {
        if (speed <= 17.5f)
        {
            speed += 0.5f;
        }
    }


    [PunRPC]
    void RPC_OnSpeedSubButtonClick()
    {
        if (speed >= 1f)
        {
            speed -= 0.5f;
        }

    }

    [PunRPC]
    void RPC_OnAngleAddButtonClick()
    {
        if (angle <= 60f)
        {
            angle += 5f;
        }
    }

    [PunRPC]
    void RPC_OnAngleSubButtonClick()
    {
        if (angle >= 20f)
        {
            angle -= 10f;
        }
    }
}