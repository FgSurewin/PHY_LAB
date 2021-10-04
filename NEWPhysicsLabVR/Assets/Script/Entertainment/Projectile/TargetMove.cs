using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetMove : MonoBehaviour
{

    private PhotonView PV;

    public static System.Random rd;

    public GameObject Flag = null;
    public GameObject Monster = null;
    public GameObject Board = null;
    public GameObject BackTarget = null;
    public TextMeshPro BoardText = null;

    private float distance = 153f;

    // Start is called before the first frame update
    void Start()
    {
        rd = new System.Random();
        PV = GetComponent<PhotonView>();
        Flag.SetActive(false);
        
        distance = Monster.transform.position.z - Rotate.CannonState.startPosition.z;
        BackTarget.transform.position = new Vector3(BackTarget.transform.position.x, BackTarget.transform.position.y, Monster.transform.position.z);
        Board.transform.position = new Vector3(Board.transform.position.x, Board.transform.position.y, Monster.transform.position.z - 20f);
        transform.position = new Vector3(transform.position.x, transform.position.y, Monster.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        BoardText.text = "Distance : " + Mathf.Ceil(distance).ToString() + "m";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "ProjectileBall")
        {
            PV.RPC("RPC_ToggleFlag", RpcTarget.AllBuffered, true);
            PV.RPC("RPC_ToggleMonster", RpcTarget.AllBuffered, false);
            PV.RPC("RPC_ToggleBoard", RpcTarget.AllBuffered, false);
        }
    }

    public void FindEnemy()
    {
        PV.RPC("RPC_FindEnemy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_FindEnemy()
    {

        int test = rd.Next(60,120);
        Monster.transform.position = new Vector3(Monster.transform.position.x, Monster.transform.position.y, test);
        BackTarget.transform.position = new Vector3(BackTarget.transform.position.x, BackTarget.transform.position.y, Monster.transform.position.z);
        Board.transform.position = new Vector3(Board.transform.position.x, Board.transform.position.y, Monster.transform.position.z - 20f);
        transform.position = new Vector3(transform.position.x, transform.position.y, Monster.transform.position.z);
        distance = Monster.transform.position.z - Rotate.CannonState.startPosition.z;
        PV.RPC("RPC_ToggleFlag", RpcTarget.AllBuffered, false);
        PV.RPC("RPC_ToggleMonster", RpcTarget.AllBuffered, true);
        PV.RPC("RPC_ToggleBoard", RpcTarget.AllBuffered, true);
    }

    [PunRPC]
    void RPC_ToggleFlag(bool toggle)
    {
        Flag.SetActive(toggle);
    }

    [PunRPC]
    void RPC_ToggleMonster(bool toggle)
    {
        Monster.SetActive(toggle);
    }

    [PunRPC]
    void RPC_ToggleBoard(bool toggle)
    {
        Board.SetActive(toggle);
    }
}

