using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeStateManage : MonoBehaviour
{
    public static BungeeStateManage Bungeestate = null;
    public bool isSwitch = false;
    public GameObject origin = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Bungeestate == null || Bungeestate != this)
            Bungeestate = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
