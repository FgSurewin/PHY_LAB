using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpringController : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SpringMove.spr.OnStartButtonClick();
    }

    public void OnStopButtonClick()
    {
        SpringMove.spr.OnStopButtonClick();
    }

    public void OnWeight1AddButtonClick()
    {
        SpringMove.spr.OnWeight1AddButtonClick();
    }

    public void OnOnWeight1SubButtonClick()
    {
        SpringMove.spr.OnWeight1SubButtonClick();
    }

    /*public void weightPointerClick()
    {
        SpringMove.spr.weightPointerClick();
    }*/

    public void OnSaveButtonClick()
    {
        Debug.Log("Save button is clicked");
        databaseManager.DM.saveLab3Data(loginManager.LM.user.UserId);
    }

    public void OnMoveRoomButtonClick(GameObject button)
    {
        if (button.name == "MoveToRoom1")
            PhotonRoom.room.moveToRoom(1);
        else if (button.name == "MoveToRoom2")
            PhotonRoom.room.moveToRoom(2);
        else if (button.name == "MoveToRoom4")
            PhotonRoom.room.moveToRoom(4);
        else if (button.name == "MoveToEntertainmentArea")
            PhotonRoom.room.moveToRoom(6);
        else { };
    }
    
}

