using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform goPosition;
    public Transform backPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "BackHome")
        {
            PhotonRoom.room.moveToRoom(4);
        }
        else if (gameObject.name == "BackTarget")
        {

            other.gameObject.GetComponent<UserMovement>().TeleportGo(-backPosition.position + new Vector3(0, 0, -10));
        }
        else if (gameObject.name == "GoTarget")
        {

            
            other.gameObject.GetComponent<UserMovement>().TeleportGo(backPosition.position - goPosition.position);


        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
