using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeController : MonoBehaviour
{
    public void OnEnterBungeeButtonClick()
    {

        BungeeMove.Bm.SetKin();
        UserMovement.User.SwitchInBungeeCamera();
        
    }

    public void OnSubSpringStrengthButtonClick()
    {
        BungeeMove.Bm.SubSpring();
    }
    public void OnAddSpringStrengthButtonClick()
    {
        BungeeMove.Bm.AddSpring();
    }
    public void OnAddUserWeightButtonClick()
    {
        BungeeMove.Bm.AddUserWeight();
    }
    public void OnSubUserWeightButtonClick()
    {
        BungeeMove.Bm.SubUserWeight();
    }
    public void OnMoveToObservationPlatformButtonClick()
    {
        UserMovement.User.TeleportUser(new Vector3(3, -130, -172));
        BungeeMove.Bm.GenerateBox();
    }
    public void OnResetButtonClick()
    {
        BungeeMove.Bm.ResetValue();
    }
    public void OnGoBackButtonClick()
    {
        UserMovement.User.TeleportUser(new Vector3(0, 3, -6));
        BungeeMove.Bm.DestroyBox();
    }
}
