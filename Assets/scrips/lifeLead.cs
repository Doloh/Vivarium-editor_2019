using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeLead : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<lifeOne>().Speed = 0.3f;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "life")
        {
            col.gameObject.GetComponent<lifeOne>().Suivi = gameObject.transform;
        }
    }
}
