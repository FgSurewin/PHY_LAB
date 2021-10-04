using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{

    private PhotonView PV;

    private float Gravity = 9.8f;
    private float timer;
    private float sinValue;
    private float cosValue;

    public GameObject ball = null;
    public GameObject fireButton = null;


    private bool isFired;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (isFired)
        {
            timer += Time.deltaTime;

            sinValue = Mathf.Sin(Mathf.Deg2Rad * Rotate.CannonState.angle);
            cosValue = Mathf.Cos(Mathf.Deg2Rad * Rotate.CannonState.angle);

            float xOffset = Rotate.CannonState.speed * timer * cosValue;
            float yOffset = Rotate.CannonState.speed * timer * sinValue - 0.5f * Gravity * timer * timer;
            //float zOffset = speed * timer * cosValue ;

            Vector3 endPos = Rotate.CannonState.startPosition + new Vector3(0, yOffset, xOffset);

            //ball.GetComponent<Rigidbody>().velocity = endPos;
            //ball.GetComponent<Rigidbody>().MovePosition(endPos);
            //ball.GetComponent<Rigidbody>().AddForce(endPos);
            ball.transform.position = endPos;
            if (endPos.y < 0f)
            {
                Debug.Log("OVER TIME -> " + timer);
                Debug.Log("FINAL POSITION -> " + ball.transform.position);
                ball.transform.position = Rotate.CannonState.startPosition;
                isFired = false;
                fireButton.SetActive(true);
                timer = 0;
                endPos = Rotate.CannonState.startPosition;
            }
        }


    }

    public void HandleFired()
    {
        PV.RPC("RPC_HandleFired", RpcTarget.AllBuffered);
    }


    [PunRPC]
    void RPC_HandleFired()
    {
        isFired = true;
        fireButton.SetActive(false);
    }
}

