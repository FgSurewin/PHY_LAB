using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTableController : MonoBehaviour
{
    public void OnAddRedAngleButtonClick()
    {
        ForceTableStrings.fts.OnAddRedAngleButtonClick();
    }

    public void OnAddBlackAngleButtonClick()
    {
        ForceTableStrings.fts.OnAddBlackAngleButtonClick();
    }

    public void OnAddPurpleAngleButtonClick()
    {
        ForceTableStrings.fts.OnAddPurpleAngleButtonClick();
    }

    public void OnSubRedAngleButtonClick()
    {
        ForceTableStrings.fts.OnSubRedAngleButtonClick();
    }

    public void OnSubBlackAngleButtonClick()
    {
        ForceTableStrings.fts.OnSubBlackAngleButtonClick();
    }

    public void OnSubPurpleAngleButtonClick()
    {
        ForceTableStrings.fts.OnSubPurpleAngleButtonClick();
    }

    public void OnAddRedMassButtonClick()
    {
        ForceTableData.ftd.OnAddRedMassButtonClick();
    }

    public void OnAddBlackMassButtonClick()
    {
        ForceTableData.ftd.OnAddBlackMassButtonClick();
    }

    public void OnAddPurpleMassButtonClick()
    {
        ForceTableData.ftd.OnAddPurpleMassButtonClick();
    }

    public void OnSubRedMassButtonClick()
    {
        ForceTableData.ftd.OnSubRedMassButtonClick();
    }

    public void OnSubBlackMassButtonClick()
    {
        ForceTableData.ftd.OnSubBlackMassButtonClick();
    }

    public void OnSubPurpleMassButtonClick()
    {
        ForceTableData.ftd.OnSubPurpleMassButtonClick();
    }

    public void OnResetButtonClick()
    {
        ForceTableStrings.fts.OnResetButtonClick();
    }

    public void OnStartButtonClick()
    {
        ForceTableStrings.fts.OnStartButtonClick();
    }

    public void OnSaveDataButtonClick()
    {
        Debug.Log("Save button is clicked");
        databaseManager.DM.saveLab2Data(loginManager.LM.user.UserId);
    }

    //move to lab 1 room or lab 3 room.  
    public void OnMoveRoomButtonClick(GameObject button)
    {
        if (button.name == "MoveToRoom1")
            PhotonRoom.room.moveToRoom(1);
        else if (button.name == "MoveToRoom3")
            PhotonRoom.room.moveToRoom(3);
        else if (button.name == "MoveToRoom4")
            PhotonRoom.room.moveToRoom(4);
        else { };
    }
}
