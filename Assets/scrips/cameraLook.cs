using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLook : MonoBehaviour
{
    public Transform cible;
  
    void Update()
    {
        transform.LookAt(cible);
    }
}
