using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UserMovement : MonoBehaviour
{

    private PhotonView PV;
    private CharacterController cc;
    public GameObject myCamera;


    public Transform vrCamera;
    float speed = 5.0f;
    float toggleAngle = 20.0f;
    bool moveForward;

    public TextMeshPro userName;

    // Switch Camera
    public static GameObject CurrentPlayer = null;
    public GameObject projectileBall = null;
    public GameObject switchButton = null;
    public GameObject BungeeBall = null;

    public static UserMovement User = null;

    // Start is called before the first frame update
    void Start()
    {
        User = this;
        PV = GetComponent<PhotonView>();
        cc = GetComponent<CharacterController>();
        switchButton = GameObject.Find("SwitchIn");
        if (PV.IsMine)
        {
            myCamera.SetActive(true);
        }

        //set user nick name
        PhotonNetwork.NickName = databaseManager.DM.userName;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            BasicMovement();
        }

        //display user nick name on top of user
        userName.text = PV.Owner.NickName;

    }


    void BasicMovement()
    {
        if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90)
        {
            moveForward = true;

        }
        else
        {
            moveForward = false;
        }

        if (moveForward)
        {
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed);
        }
    }

    public void SwitchIn()
    {
        if (!StateManage.state.isSwitch)
        {
            StateManage.state.origin = CurrentPlayer;
            projectileBall = GameObject.Find("ProjectileBall");
            
            CurrentPlayer.GetComponent<UserMovement>().myCamera.transform.SetParent(projectileBall.transform);
            CurrentPlayer.GetComponent<UserMovement>().PV.RPC("RPC_ToggleSwitch", RpcTarget.AllBuffered, true);
        }
    }

    public void SwitchOut()
    {
        if (StateManage.state.isSwitch)
        {
            
            CurrentPlayer.GetComponent<UserMovement>().myCamera.transform.SetParent(StateManage.state.origin.transform);
            CurrentPlayer.GetComponent<UserMovement>().PV.RPC("RPC_ToggleSwitch", RpcTarget.AllBuffered, false);
        }
    }


    public void TeleportGo(Vector3 postion)
    {
        if (PV.IsMine)
        {
            cc.Move(postion);
        }
    }

    public void SwitchInBungeeCamera()
    {
        Debug.Log("Switch In");
        if (!BungeeStateManage.Bungeestate.isSwitch)
        {
            BungeeStateManage.Bungeestate.origin = CurrentPlayer;
            BungeeBall = GameObject.Find("BungeeBall");
            CurrentPlayer.GetComponent<UserMovement>().myCamera.transform.SetParent(BungeeBall.transform);
            BungeeStateManage.Bungeestate.isSwitch = true;
            Debug.Log("Started Coroutine at timestamp : " + Time.time);
            BungeeMove.Bm.DisActiveButtons();
            Invoke("SwitchOutBungeeCamera", 25f);
        }
    }

    public void SwitchOutBungeeCamera()
    {
        Debug.Log("Switch Out");
        if (BungeeStateManage.Bungeestate.isSwitch)
        {
            CurrentPlayer.GetComponent<UserMovement>().myCamera.transform.SetParent(BungeeStateManage.Bungeestate.origin.transform);
            BungeeStateManage.Bungeestate.isSwitch = false;
        }
    }
    public void TeleportUser(Vector3 Pos)
    {
        CurrentPlayer.GetComponent<UserMovement>().transform.position =Pos;
    }

    [PunRPC]
    void RPC_ToggleSwitch(bool toggle)
    {
        StateManage.state.isSwitch = toggle;
        switchButton.SetActive(!toggle);
    }
}
