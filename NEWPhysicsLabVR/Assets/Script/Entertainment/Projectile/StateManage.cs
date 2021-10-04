using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManage : MonoBehaviour
{
    public static StateManage state = null;
    public bool isSwitch = false;
    public GameObject origin = null;


    private void Awake()
    {
        if (state == null || state != this)
            state = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //cannon.transform.rotation = Quaternion.Euler(-1 * 30, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMoveRoomButtonClick(GameObject button)
    {
        if (button.name == "BackHome")
            PhotonRoom.room.moveToRoom(4);
        else if (button.name == "MoveToRoom2")
            PhotonRoom.room.moveToRoom(2);


    }

}
