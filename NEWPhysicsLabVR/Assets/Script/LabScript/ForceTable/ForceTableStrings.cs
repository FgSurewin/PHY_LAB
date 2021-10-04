using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class ForceTableStrings : MonoBehaviour
{

    public static ForceTableStrings fts;

    private PhotonView PV;

    public TextMeshPro labSuccess;

    public Transform redPivot;
    public Transform blackPivot;
    public Transform purplePivot;

    public TextMeshPro redAngleTMP;
    public TextMeshPro blackAngleTMP;
    public TextMeshPro purpleAngleTMP;

    public Transform center;

    // used for resetting
    public Transform reset;

    public float redMass;
    public float blackMass;
    public float purpleMass;

    public float redAngle;
    public float blackAngle;
    public float purpleAngle;
    public float redDegrees;
    public float blackDegrees;
    public float purpleDegrees;

    public TextMeshPro redMassTMP;
    public TextMeshPro blackMassTMP;
    public TextMeshPro purpleMassTMP;

    public GameObject addRedAngleButton;
    public GameObject addBlackAngleButton;
    public GameObject addPurpleAngleButton;
    public GameObject subRedAngleButton;
    public GameObject subBlackAngleButton;
    public GameObject subPurpleAngleButton;

    public GameObject addRedMassButton;
    public GameObject addBlackMassButton;
    public GameObject addPurpleMassButton;
    public GameObject subRedMassButton;
    public GameObject subBlackMassButton;
    public GameObject subPurpleMassButton;

    public GameObject startButton;
    public GameObject resetButton;

    public bool start = false;

    public float g = 9.8f;

    float xForce;
    float zForce;

    // Start is called before the first frame update
    void Start()
    {
        if (ForceTableStrings.fts == null)
            ForceTableStrings.fts = this;

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (start)
        {
            if (xForce == 0 && zForce == 0)
            {
                start = false;
                labSuccess.text = "LAB COMPLETE, SAVE YOUR DATA";
            }
            else
            {
                center.position = new Vector3(center.position.x + xForce, center.position.y, center.position.z + zForce);
            }
        }
    }

    public void OnStartButtonClick()
    {
        PV.RPC("RPC_OnStartButtonClick", RpcTarget.AllBuffered);
    }

    public void OnResetButtonClick()
    {
        PV.RPC("RPC_OnResetButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAddRedAngleButtonClick()
    {
        PV.RPC("RPC_OnAddRedAngleButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAddBlackAngleButtonClick()
    {
        PV.RPC("RPC_OnAddBlackAngleButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAddPurpleAngleButtonClick()
    {
        PV.RPC("RPC_OnAddPurpleAngleButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSubRedAngleButtonClick()
    {
        PV.RPC("RPC_OnSubRedAngleButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSubBlackAngleButtonClick()
    {
        PV.RPC("RPC_OnSubBlackAngleButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSubPurpleAngleButtonClick()
    {
        PV.RPC("RPC_OnSubPurpleAngleButtonClick", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_OnStartButtonClick()
    {
        start = true;

        redDegrees = float.Parse(redAngleTMP.text);
        blackDegrees = float.Parse(blackAngleTMP.text);
        purpleDegrees = float.Parse(purpleAngleTMP.text);

        redAngle = (float)Math.PI * redDegrees / 180.0f;
        blackAngle = (float)Math.PI * blackDegrees / 180.0f;
        purpleAngle = (float)Math.PI * purpleDegrees / 180.0f;

        redMass = float.Parse(redMassTMP.text);
        blackMass = float.Parse(blackMassTMP.text);
        purpleMass = float.Parse(purpleMassTMP.text);

        xForce = redMass * (float)Math.Cos((double)redAngle) + blackMass * (float)Math.Cos((double)blackAngle) + purpleMass * (float)Math.Cos((double)purpleAngle);
        if (Math.Abs(xForce) < .6f)
            xForce = 0f;
        else
            xForce *= .00001f;
        zForce = redMass * (float)Math.Sin((double)redAngle) + blackMass * (float)Math.Sin((double)blackAngle) + purpleMass * (float)Math.Sin((double)purpleAngle);
        if (Math.Abs(zForce) < .6f)
            zForce = 0f;
        else
            zForce *= .00001f;

        //startButton.SetActive(false);
        //resetButton.SetActive(true);
        //addRedAngleButton.SetActive(false);

    }

    [PunRPC]
    void RPC_OnResetButtonClick()
    {
        start = false;
        center.position = reset.position;
        Vector3 eulerAngles = redPivot.eulerAngles;
        eulerAngles.y = 320f;
        redPivot.eulerAngles = eulerAngles;
        eulerAngles = blackPivot.eulerAngles;
        eulerAngles.y = 0f;
        blackPivot.eulerAngles = eulerAngles;
        eulerAngles = purplePivot.eulerAngles;
        eulerAngles.y = 0f;
        purplePivot.eulerAngles = eulerAngles;

        redMassTMP.text = "300";
        blackMassTMP.text = "0";
        purpleMassTMP.text = "0";

    }

    [PunRPC]
    void RPC_OnAddRedAngleButtonClick()
    {
        Vector3 eulerAngles = redPivot.eulerAngles;
        eulerAngles.y = redPivot.eulerAngles.y - 1f;
        redPivot.eulerAngles = eulerAngles;
    }

    [PunRPC]
    void RPC_OnAddBlackAngleButtonClick()
    {
        Vector3 eulerAngles = blackPivot.eulerAngles;
        eulerAngles.y = blackPivot.eulerAngles.y - 1f;
        blackPivot.eulerAngles = eulerAngles;
    }

    [PunRPC]
    void RPC_OnAddPurpleAngleButtonClick()
    {
        Vector3 eulerAngles = purplePivot.eulerAngles;
        eulerAngles.y = purplePivot.eulerAngles.y - 1f;
        purplePivot.eulerAngles = eulerAngles;
    }

    [PunRPC]
    void RPC_OnSubRedAngleButtonClick()
    {
        Vector3 eulerAngles = redPivot.eulerAngles;
        eulerAngles.y = redPivot.eulerAngles.y + 1f;
        redPivot.eulerAngles = eulerAngles;
    }

    [PunRPC]
    void RPC_OnSubBlackAngleButtonClick()
    {
        Vector3 eulerAngles = blackPivot.eulerAngles;
        eulerAngles.y = blackPivot.eulerAngles.y + 1f;
        blackPivot.eulerAngles = eulerAngles;
    }

    [PunRPC]
    void RPC_OnSubPurpleAngleButtonClick()
    {
        Vector3 eulerAngles = purplePivot.eulerAngles;
        eulerAngles.y = purplePivot.eulerAngles.y - 1f;
        purplePivot.eulerAngles = eulerAngles;
    }
}
