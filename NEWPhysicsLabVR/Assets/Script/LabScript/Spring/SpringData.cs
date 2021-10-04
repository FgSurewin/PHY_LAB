using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class SpringData : MonoBehaviour
{

    public static SpringData spData;
    private PhotonView PV;

    public TextMeshPro[] BoardWeight = new TextMeshPro[3];
    public TextMeshPro[] BoardObject1SpringLength = new TextMeshPro[3];


    public float[] weight= new float[3];
    public float[] length = new float[3];
    public int currentSpr1 = 0;



    // Start is called before the first frame update
    void Start()
    {
        if (SpringData.spData == null)
        {
            SpringData.spData = this;
        }

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            BoardWeight[i].text = weight[i].ToString();
            BoardObject1SpringLength[i].text = length[i].ToString();
        }
    }


    public void OnSaveButtonClick()
    {
        Debug.Log("Save button is clicked");
        databaseManager.DM.saveLab3Data(loginManager.LM.user.UserId);
    }
}
