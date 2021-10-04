using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class BungeeMove : MonoBehaviour
{
    private PhotonView PV;
    public static BungeeMove Bm;
    public GameObject BungeeBall;
    public GameObject[] Buttons = new GameObject[6];
    public GameObject Chest;
    internal Rigidbody RBody;
    private int childcount;
    private Transform t;
    private SpringJoint spring;

    public TextMeshPro UserWeight;
    public TextMeshPro SpringStrength;
    public TextMeshPro UserCurrentPos;
    public TextMeshPro ChestCurrentPos;
    internal void Start()
    {
        if (BungeeMove.Bm == null)
        {
            BungeeMove.Bm = this;
        }
        
        PV = GetComponent<PhotonView>();
        //add Rigibody component to the gameobject
        Bm.gameObject.AddComponent<Rigidbody>();
        //get gameobject's Rigibody the Assign to RBody
        Bm.RBody = Bm.gameObject.GetComponent<Rigidbody>();
        Bm.RBody.isKinematic = true;
        //Number of child objects in gameobject
        //Add Spring Joint components to each of the sub-objects

        childcount = Bm.transform.childCount;
        for (int i = 0; i < childcount; i++)
        {
            t = Bm.transform.GetChild(i);
            t.gameObject.AddComponent<SpringJoint>();
            spring = t.gameObject.GetComponent<SpringJoint>();
            spring.connectedBody = i == 0 ? Bm.RBody : Bm.transform.GetChild(i - 1).GetComponent<Rigidbody>();
            spring.enableCollision = true;
            spring.spring = 200;
        }
    }
    private void Update()
    {

        UserWeight.text = "User Weight: " + (BungeeBall.GetComponent<Rigidbody>().mass * 10).ToString("0") + "Kg";
        SpringStrength.text = "Spring Strength: " + (Bm.GetComponentInChildren<SpringJoint>().spring / 2).ToString();
        UserCurrentPos.text = "User Current Position: " + BungeeBall.transform.position.y.ToString("0") + "M";
    }
    public void SubSpring()
    {
        PV.RPC("RPC_SubSpring", RpcTarget.AllBuffered);
    }

    public void AddSpring()
    {
        PV.RPC("RPC_AddSpring", RpcTarget.AllBuffered);
    }

    public void AddUserWeight()
    {
        PV.RPC("RPC_AddUserWeight", RpcTarget.AllBuffered);
    }

    public void ResetValue()
    {
        Debug.Log("rest");
        PV.RPC("RPC_ResetValue", RpcTarget.AllBuffered);
    }

    public void SubUserWeight()
    {
        PV.RPC("RPC_SubUserWeight", RpcTarget.AllBuffered);
    }

    public void DisActiveButtons()
    {
        PV.RPC("RPC_DisActiveButtons", RpcTarget.AllBuffered);
    }

    public void ActiveButtons()
    {
        PV.RPC("RPC_ActiveButtons", RpcTarget.AllBuffered);
    }

    public void GenerateBox()
    {
        PV.RPC("RPC_GenerateBox", RpcTarget.AllBuffered);
    }

    public void SetKin()
    {
        PV.RPC("RPC_SetKin", RpcTarget.AllBuffered);
    }

    public void DestroyBox()
    {
        PV.RPC("RPC_DestroyBox", RpcTarget.AllBuffered);
    }


    [PunRPC]
     void RPC_SubSpring()
    {
        for (int i = 0; i < childcount; i++)
        {
            t = Bm.transform.GetChild(i);
            spring = t.gameObject.GetComponent<SpringJoint>();
            spring.spring = spring.spring-2;
        }

    }
    [PunRPC]
    void RPC_AddSpring()
    {
        for (int i = 0; i < childcount; i++)
        {
            t = Bm.transform.GetChild(i);
            spring = t.gameObject.GetComponent<SpringJoint>();
            spring.spring = spring.spring + 2;
        }
    }
    [PunRPC]
     void RPC_AddUserWeight()
    {
        BungeeBall.GetComponent<Rigidbody>().mass += 1;
    }

    [PunRPC]
    void RPC_SubUserWeight()
    {
        BungeeBall.GetComponent<Rigidbody>().mass -= 1;
    }

    [PunRPC]
    void RPC_ResetValue()
    {
        for (int i = 0; i < childcount; i++)
        {
        t = Bm.transform.GetChild(i);
        spring = t.gameObject.GetComponent<SpringJoint>();
        spring.spring = 100;
        }
        BungeeBall.GetComponent<Rigidbody>().mass = 10;
        BungeeBall.transform.position = new Vector3(0, 2, -98);
        BungeeBall.GetComponent<Rigidbody>().isKinematic = true;

    }
    [PunRPC]
    void RPC_DisActiveButtons()
    {
        for(int i=0;i<Buttons.Length;i++)
        Buttons[i].SetActive(false);
        Invoke("ActiveButtons", 25f);
    }
    [PunRPC]
    void RPC_ActiveButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
            Buttons[i].SetActive(true);
    }

    [PunRPC]
    void RPC_GenerateBox()
    {
        float RandomY = Random.Range(-30f, -100f);
        Vector3 spawnPos = new Vector3(1.8f, RandomY, -109.2f);
        Instantiate(Chest, spawnPos, Chest.transform.rotation);
        ChestCurrentPos.text = "Chest Current Position: " + GameObject.FindGameObjectWithTag("Box").transform.position.y.ToString("0") + "M";
    }
    
    [PunRPC]
    void RPC_SetKin()
    {
        BungeeBall.GetComponent<Rigidbody>().isKinematic = false;
    }
    
    [PunRPC]
    void RPC_DestroyBox()
    {
        Destroy(GameObject.FindGameObjectWithTag("Box"));
    }
    

}
