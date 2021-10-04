using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        ProjectileMove.pjt.OnStartButtonClick();
    }

    public void OnStopButtonClick()
    {
        ProjectileMove.pjt.OnStopButtonClick();
    }

    public void OnResetButtonClick()
    {
        ProjectileMove.pjt.OnResetButtonClick();
    }

    public void OnSpeedAddButtonClick()
    {
        ProjectileMove.pjt.OnSpeedAddButtonClick();
    }

    public void OnSpeedSubButtonClick()
    {
        ProjectileMove.pjt.OnSpeedSubButtonClick();
    }

    public void OnAngleAddButtonClick()
    {
        ProjectileMove.pjt.OnAngleAddButtonClick();
    }

    public void OnAngleSubButtonClick()
    {
        ProjectileMove.pjt.OnAngleSubButtonClick();
    }

    public void OnSaveButtonClick()
    {
        Debug.Log("Save button is clicked");
        databaseManager.DM.saveLab4Data(loginManager.LM.user.UserId);
    }

    public void OnMoveRoomButtonClick(GameObject button)
    {
        if (button.name == "MoveToRoom1")
            PhotonRoom.room.moveToRoom(1);
        else if (button.name == "MoveToRoom2")
            PhotonRoom.room.moveToRoom(2);
        else if (button.name == "MoveToRoom3")
            PhotonRoom.room.moveToRoom(3);
        else if(button.name == "Teleport")
        {
            PhotonRoom.room.moveToRoom(5);
        };
    }

}