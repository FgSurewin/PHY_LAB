using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    public float timerCountDown = 8.0f;
    public GameObject Cube;
    bool isPlayerColliding = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BungeeBall")
        {
            Debug.Log("Player Hit the BungeeBall");
            isPlayerColliding = true;
        }

    }
    // Check if the player is still at location
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "BungeeBall" && isPlayerColliding == true)
        {
            Debug.Log(timerCountDown);
            if(timerCountDown == 0)
            {
                DestroyCube();
            }
        }
    }
    // If the player is not colliding reset our timer
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BungeeBall")
        {
            Debug.Log("Player Exited");
            isPlayerColliding = false;
        }
    }
    private void DestroyCube()
    {
        Destroy(Cube);
        Debug.Log("You Win");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerColliding == true)
        {
            timerCountDown -= Time.deltaTime;
            if (timerCountDown < 0)
            {
                timerCountDown = 0;
            }
        }

    }
}
