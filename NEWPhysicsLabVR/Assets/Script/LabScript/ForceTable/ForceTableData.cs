using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class ForceTableData : MonoBehaviour
{

    public static ForceTableData ftd;
    private PhotonView PV;

    public TextMeshPro redAngle;
    public TextMeshPro blackAngle;
    public TextMeshPro purpleAngle;

    public TextMeshPro redMass;
    public TextMeshPro blackMass;
    public TextMeshPro purpleMass;

    public GameObject redPivot;
    public GameObject blackPivot;
    public GameObject purplePivot;

    // Start is called before the first frame update
    void Start()
    {
        if (ForceTableData.ftd == null)
            ForceTableData.ftd = this;

        PV = GetComponent<PhotonView>();

        float rAngle, bAngle, pAngle;
        if (redPivot.transform.eulerAngles.y <= 180)
            rAngle = 180 - redPivot.transform.eulerAngles.y;
        else
            rAngle = (360f - redPivot.transform.eulerAngles.y) + 180f;
        redAngle.text = rAngle.ToString("F0");

        if (blackPivot.transform.eulerAngles.y > 90)
            bAngle = (360f - blackPivot.transform.eulerAngles.y) + 90f;
        else
            bAngle = 90f - blackPivot.transform.eulerAngles.y;
        blackAngle.text = bAngle.ToString("F0");

        pAngle = (360f - purplePivot.transform.eulerAngles.y) % 360f;
        purpleAngle.text = pAngle.ToString("F0");
        redMass.text = "300";

    }

    // Update is called once per frame
    void Update()
    {
        float rAngle, bAngle, pAngle;

        if (redPivot.transform.eulerAngles.y <= 180)
            rAngle = 180 - redPivot.transform.eulerAngles.y;
        else
            rAngle = (360f - redPivot.transform.eulerAngles.y) + 180f;
        redAngle.text = rAngle.ToString("F0");

        if (blackPivot.transform.eulerAngles.y > 90)
            bAngle = (360f - blackPivot.transform.eulerAngles.y) + 90f;
        else
            bAngle = 90f - blackPivot.transform.eulerAngles.y;
        blackAngle.text = bAngle.ToString("F0");

        pAngle = (360f - purplePivot.transform.eulerAngles.y) % 360f;
        purpleAngle.text = pAngle.ToString("F0");
    }

    public void OnAddRedMassButtonClick()
    {
        PV.RPC("RPC_OnAddRedMassButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAddBlackMassButtonClick()
    {
        PV.RPC("RPC_OnAddBlackMassButtonClick", RpcTarget.AllBuffered);
    }

    public void OnAddPurpleMassButtonClick()
    {
        PV.RPC("RPC_OnAddPurpleMassButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSubRedMassButtonClick()
    {
        PV.RPC("RPC_OnSubRedMassButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSubBlackMassButtonClick()
    {
        PV.RPC("RPC_OnSubBlackMassButtonClick", RpcTarget.AllBuffered);
    }

    public void OnSubPurpleMassButtonClick()
    {
        PV.RPC("RPC_OnSubPurpleMassButtonClick", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_OnAddRedMassButtonClick()
    {
        redMass.text = (Int32.Parse(redMass.text) + 1).ToString();
    }

    [PunRPC]
    void RPC_OnAddBlackMassButtonClick()
    {
        blackMass.text = (Int32.Parse(blackMass.text) + 1).ToString();
    }

    [PunRPC]
    void RPC_OnAddPurpleMassButtonClick()
    {
        purpleMass.text = (Int32.Parse(purpleMass.text) + 1).ToString();
    }

    [PunRPC]
    void RPC_OnSubRedMassButtonClick()
    {
        redMass.text = (Int32.Parse(redMass.text) - 1).ToString();
    }

    [PunRPC]
    void RPC_OnSubBlackMassButtonClick()
    {
        blackMass.text = (Int32.Parse(blackMass.text) - 1).ToString();
    }

    [PunRPC]
    void RPC_OnSubPurpleMassButtonClick()
    {
        purpleMass.text = (Int32.Parse(purpleMass.text) - 1).ToString();
    }
}
