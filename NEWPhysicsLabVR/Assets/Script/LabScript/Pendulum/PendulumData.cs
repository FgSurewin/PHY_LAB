using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class PendulumData : MonoBehaviour
{
    public static PendulumData penData;
    private PhotonView PV;

    public TextMeshPro [] BoardTime = new TextMeshPro[6];
    public TextMeshPro [] BoardLength = new TextMeshPro[6];
    public TextMeshPro [] BoardPeriod = new TextMeshPro[6];
    public GameObject[] AddPeriodButton = new GameObject[6];
    public GameObject[] SubPeriodButton = new GameObject[6];
    public int currentRow = 0;

    public float [] totalTime = new float [6];
    public float [] length = new float[6];
    public int [] period = new int[6];
    

    // Start is called before the first frame update
    void Start()
    {
        if (PendulumData.penData == null)
        {
            PendulumData.penData = this;
        }

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            BoardTime[i].text = String.Format("{0:0.00}", totalTime[i]);
            BoardLength[i].text = length[i].ToString();
            BoardPeriod[i].text = period[i].ToString();
        }

    }

    public void OnAddPeriodButtonClick(GameObject Addbutton)
    {
        if (Addbutton.name == "BoardPeriodAdd1")
            PV.RPC("RPC_OnAddPeriodButton1Click", RpcTarget.AllBuffered);
        else if (Addbutton.name == "BoardPeriodAdd2")
            PV.RPC("RPC_OnAddPeriodButton2Click", RpcTarget.AllBuffered);
        else if (Addbutton.name == "BoardPeriodAdd3")
            PV.RPC("RPC_OnAddPeriodButton3Click", RpcTarget.AllBuffered);
        else if (Addbutton.name == "BoardPeriodAdd4")
            PV.RPC("RPC_OnAddPeriodButton4Click", RpcTarget.AllBuffered);
        else if (Addbutton.name == "BoardPeriodAdd5")
            PV.RPC("RPC_OnAddPeriodButton5Click", RpcTarget.AllBuffered);
        else  
            PV.RPC("RPC_OnAddPeriodButton6Click", RpcTarget.AllBuffered);
    }

    public void OnSubPeriodButtonClick(GameObject Subbutton)
    {
        if (Subbutton.name == "BoardPeriodSub1")
            PV.RPC("RPC_OnSubPeriodButton1Click", RpcTarget.AllBuffered);
        else if (Subbutton.name == "BoardPeriodSub2")
            PV.RPC("RPC_OnSubPeriodButton2Click", RpcTarget.AllBuffered);
        else if (Subbutton.name == "BoardPeriodSub3")
            PV.RPC("RPC_OnSubPeriodButton3Click", RpcTarget.AllBuffered);
        else if (Subbutton.name == "BoardPeriodSub4")
            PV.RPC("RPC_OnSubPeriodButton4Click", RpcTarget.AllBuffered);
        else if (Subbutton.name == "BoardPeriodSub5")
            PV.RPC("RPC_OnSubPeriodButton5Click", RpcTarget.AllBuffered);
        else
            PV.RPC("RPC_OnSubPeriodButton6Click", RpcTarget.AllBuffered);
    }


    [PunRPC]
    void RPC_OnAddPeriodButton1Click()
    {
        period[0]++;
    }

    [PunRPC]
    void RPC_OnAddPeriodButton2Click()
    {
        period[1]++;
    }

    [PunRPC]
    void RPC_OnAddPeriodButton3Click()
    {
        period[2]++;
    }

    [PunRPC]
    void RPC_OnSubPeriodButton1Click()
    {
        if (period[0]>0)
        period[0]--;
    }

    [PunRPC]
    void RPC_OnSubPeriodButton2Click()
    {
        if (period[1] > 0)
            period[1]--;
    }

    [PunRPC]
    void RPC_OnSubPeriodButton3Click()
    {
        if (period[2] > 0)
            period[2]--;
    }

    

    [PunRPC]
    void RPC_OnAddPeriodButton4Click()
    {
        period[3]++;
    }

    [PunRPC]
    void RPC_OnAddPeriodButton5Click()
    {
        period[4]++;
    }

    [PunRPC]
    void RPC_OnAddPeriodButton6Click()
    {
        period[5]++;
    }

    [PunRPC]
    void RPC_OnSubPeriodButton4Click()
    {
        if (period[3] > 0)
            period[3]--;
    }

    [PunRPC]
    void RPC_OnSubPeriodButton5Click()
    {
        if (period[4] > 0)
            period[4]--;
    }

    [PunRPC]
    void RPC_OnSubPeriodButton6Click()
    {
        if (period[5] > 0)
            period[5]--;
    }

}
