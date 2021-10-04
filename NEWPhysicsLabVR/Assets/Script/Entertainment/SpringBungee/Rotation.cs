using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject box;

    void Update()
    {
        box.transform.Rotate(Vector3.forward * 50f * Time.deltaTime);
    }
}
