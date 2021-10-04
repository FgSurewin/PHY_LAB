using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;


public class ProjectileData : MonoBehaviour
{
    public static ProjectileData pjtData;
    private PhotonView PV;

    public TextMeshPro[] BoardSpeed = new TextMeshPro[3];
    public TextMeshPro[] BoardAngle = new TextMeshPro[3];
    public TextMeshPro[] BoardDistance = new TextMeshPro[3];
    public GameObject[] AddDistanceButton = new GameObject[3];
    public GameObject[] SubDistanceButton = new GameObject[3];
    public int currentRow = 0;

    public float[] speed = new float[3];
    public float[] angle = new float[3];
    public float[] distance = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        if (ProjectileData.pjtData == null)
        {
            ProjectileData.pjtData = this;
        }

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            BoardSpeed[i].text = speed[i].ToString();
            BoardAngle[i].text = angle[i].ToString();
            BoardDistance[i].text = distance[i].ToString();
        }
    }

    public void OnAddDistanceButtonClick(GameObject Addbutton)
    {
        if (Addbutton.name == "BoardDistanceAdd1")
            PV.RPC("RPC_OnAddDistanceButton1Click", RpcTarget.AllBuffered);
        else if (Addbutton.name == "BoardDistanceAdd2")
            PV.RPC("RPC_OnAddDistanceButton2Click", RpcTarget.AllBuffered);
        else
            PV.RPC("RPC_OnAddDistanceButton3Click", RpcTarget.AllBuffered);
    }

    public void OnSubDistanceButtonClick(GameObject Subbutton)
    {
        if (Subbutton.name == "BoardDistanceSub1")
            PV.RPC("RPC_OnSubDistanceButton1Click", RpcTarget.AllBuffered);
        else if (Subbutton.name == "BoardDistanceSub2")
            PV.RPC("RPC_OnSubDistanceButton2Click", RpcTarget.AllBuffered);
        else
            PV.RPC("RPC_OnSubDistanceButton3Click", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_OnAddDistanceButton1Click()
    {
        distance[0]++;
    }

    [PunRPC]
    void RPC_OnAddDistanceButton2Click()
    {
        distance[1]++;
    }

    [PunRPC]
    void RPC_OnAddDistanceButton3Click()
    {
        distance[2]++;
    }

    [PunRPC]
    void RPC_OnSubDistanceButton1Click()
    {
        if (distance[0] > 0)
        distance[0]--;
    }

    [PunRPC]
    void RPC_OnSubDistanceButton2Click()
    {
        if (distance[1] > 0)
        distance[1]--;
    }

    [PunRPC]
    void RPC_OnSubDistanceButton3Click()
    {
        if (distance[2] > 0)
        distance[2]--;
    }
}
