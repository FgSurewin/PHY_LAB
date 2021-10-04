using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    public string userName;
    public string email;

    public User(string n, string e)
    {
        userName = n;
        email = e;
    }
}