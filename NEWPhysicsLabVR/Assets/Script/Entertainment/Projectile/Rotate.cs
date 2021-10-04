using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private PhotonView PV;
    public static Rotate CannonState;

    // Display Text
    public TextMeshPro speedText;
    public TextMeshPro angleText;

    // Cannon members
    public GameObject cannon;
    public GameObject ballPisition;
    public Vector3 startPosition;
    public GameObject increaseAngle;
    public GameObject decreaseAngle;
    public GameObject increaseSpeed;
    public GameObject decreaseSpeed;
    public Color originalColor;

    // State
    public float angle = 0f;
    public float speed = 10f;


    private void Awake()
    {
        if (CannonState == null || CannonState != this)
            CannonState = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalColor = increaseAngle.GetComponent<Renderer>().material.color;
        PV = GetComponent<PhotonView>();
        startPosition = ballPisition.transform.position;
        //Debug.Log("startPosition -> " + startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed : " + speed.ToString() + "m/s";
        angleText.text = "Angle : " + angle.ToString() + "°";
    }

    public void CheckColor()
    {
        //Debug.Log("CheckColor function");

        PV.RPC("RPC_CheckColor", RpcTarget.AllBuffered);
    }

    public void RotateCannon()
    {
        //Debug.Log("RotateCannon function");

        PV.RPC("RPC_RotateCannon", RpcTarget.AllBuffered);
    }

    public void IncreaseAngle()
    {
        //Debug.Log("IncreaseAngle function");

        PV.RPC("RPC_IncreaseAngle", RpcTarget.AllBuffered);
    }

    public void DecreaseAngle()
    {
        //Debug.Log("DecreaseAngle function");

        PV.RPC("RPC_DecreaseAngle", RpcTarget.AllBuffered);
    }

    public void IncreaseSpeed()
    {
        //Debug.Log("IncreaseSpeed function");

        PV.RPC("RPC_IncreaseSpeed", RpcTarget.AllBuffered);
    }

    public void DecreaseSpeed()
    {
        //Debug.Log("DecreaseSpeed function");

        PV.RPC("RPC_DecreaseSpeed", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_CheckColor()
    {
        if (angle == 90f)
        {
            increaseAngle.GetComponent<Renderer>().material.color = Color.red;
            decreaseAngle.GetComponent<Renderer>().material.color = originalColor;
        }
        else if (angle == 0f)
        {
            decreaseAngle.GetComponent<Renderer>().material.color = Color.red;
            increaseAngle.GetComponent<Renderer>().material.color = originalColor;
        }
        else
        {
            increaseAngle.GetComponent<Renderer>().material.color = originalColor;
            decreaseAngle.GetComponent<Renderer>().material.color = originalColor;
        }
    }

    [PunRPC]
    void RPC_SppedCheckColor()
    {
        if (speed == 100f)
        {
            increaseSpeed.GetComponent<Renderer>().material.color = Color.red;
            decreaseSpeed.GetComponent<Renderer>().material.color = originalColor;
        }
        else if (speed == 0f)
        {
            decreaseSpeed.GetComponent<Renderer>().material.color = Color.red;
            increaseSpeed.GetComponent<Renderer>().material.color = originalColor;
        }
        else
        {
            increaseSpeed.GetComponent<Renderer>().material.color = originalColor;
            decreaseSpeed.GetComponent<Renderer>().material.color = originalColor;
        }
    }

    [PunRPC]
    void RPC_RotateCannon()
    {
        cannon.transform.rotation = Quaternion.Euler(-1 * angle, 0, 0);
        PV.RPC("RPC_CheckColor", RpcTarget.AllBuffered);

    }

    [PunRPC]
    void RPC_IncreaseAngle()
    {
        if (angle < 90f)
            angle += 10f;
        startPosition = ballPisition.transform.position + new Vector3(0, 0.8f, -0.5f);
        PV.RPC("RPC_RotateCannon", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_DecreaseAngle()
    {
        if (angle > 0f)
            angle -= 10f;
        startPosition = ballPisition.transform.position - new Vector3(0, 0.8f, -0.5f);
        PV.RPC("RPC_RotateCannon", RpcTarget.AllBuffered);
    }


    [PunRPC]
    void RPC_IncreaseSpeed()
    {
        if (speed < 100f)
            speed += 10f;
        PV.RPC("RPC_SppedCheckColor", RpcTarget.AllBuffered);

    }

    [PunRPC]
    void RPC_DecreaseSpeed()
    {
        if (speed > 10f)
            speed -= 10f;
        PV.RPC("RPC_SppedCheckColor", RpcTarget.AllBuffered);
    }
}
