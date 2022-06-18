using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeOne : MonoBehaviour
{
    private float speed = 2f;
    private float rayRange = 3f;
    private Transform suivi;

    void Start()
    {
    }

    private void Update()
    {
        orientationDeLife();        
    }

    void FixedUpdate()
    {
        float rspeed = speed * Time.deltaTime * 10;
        transform.Translate(new Vector3(0, 0, rspeed));
    }


    void orientationDeLife ()
    {
        //myRaycast selon une direction (ici devant lui) et une distance
        Vector3 direction = Vector3.forward;
        Ray myRaycast = new Ray(transform.position, transform.TransformDirection(direction * rayRange));
        //pour afficher le Raycast dans la scene
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * rayRange));

        //si mon raycast detecte quelque chose.
        if (Physics.Raycast(myRaycast, out RaycastHit hit, rayRange))
        {
            //rotation de mon objet sur lui même
            transform.Rotate(-Random.value * 20, -Random.value * 10, 0);

            if (hit.collider.tag == "life")
            {
                // l'objet devient "suiveur" de cette "life", et recupere le transform de l'objet suivi
                suivi = hit.transform;
            } else
            {
                float random = Random.value;
                if (random < 0.01f)
                {
                    Debug.Log(gameObject.name + "decrochage" + ": " + random);
                    suivi = null;
                }
            }
        }
        else // si on ne suit rien ou ne detecte rien, on ne tourne pas, sauf si on suit 
        {
            transform.Rotate(0, 0, 0);

            if (suivi != null)
            {
                //on cherche à retrouver la life suivie
                // utilise une rotation smooth en direction de la life suivi
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(suivi.position - transform.position),
                    5f * Time.deltaTime);

                //Le look at n'est pas trés smooth
                //transform.LookAt(suivi);           
            }
        }
    }
}
