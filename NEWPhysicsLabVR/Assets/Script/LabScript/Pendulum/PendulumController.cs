using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
  
    public void OnStartButtonClick()
    {
        PendulumMove.pen.OnStartButtonClick();
    }

    public void OnStopButtonClick()
    {
        PendulumMove.pen.OnStopButtonClick();
    }

    public void OnResetButtonClick()
    {
        PendulumMove.pen.OnResetButtonClick();
    }

    public void OnLengthAddButtonClick()
    {
        PendulumMove.pen.OnLengthAddButtonClick();
    }

    public void OnlengthSubButtonClick()
    {
        PendulumMove.pen.OnLengthSubButtonClick();
    }


    //pass firebase user ID to save pendulum lab data to firebase realtime database 
    public void OnSaveButtonClick()
    {
        Debug.Log("Save button is clicked");
        databaseManager.DM.saveLab1Data(loginManager.LM.user.UserId);
    }


    //move to lab 2 room or lab 3 room.  
    public void OnMoveRoomButtonClick(GameObject button)
    {
        if (button.name == "MoveToRoom2")
            PhotonRoom.room.moveToRoom(2);
        else if (button.name == "MoveToRoom3")
            PhotonRoom.room.moveToRoom(3);
        else if (button.name == "MoveToRoom4")
            PhotonRoom.room.moveToRoom(4);
        else { };
    }
}
