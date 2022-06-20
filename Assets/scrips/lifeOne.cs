using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeOne : MonoBehaviour
{
    private float speed = 1f;
    private float rayRange = 3f;
    private Transform suivi;

    public Transform Suivi { get => suivi; set => suivi = value; }
    public float Speed { get => speed; set => speed = value; }

    //par defaut false, detecter si le script lifeLead est sur l'objet pour mettre à true en start
    public bool lifeLead = false;


    void Start()
    {
    }

    private void Update()
    {
        orientationDeLife();        
    }

    void FixedUpdate()
    {
        float rspeed = Speed * Time.deltaTime * 10;
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
            if (hit.collider.tag == "life" && lifeLead == false)
            {
                // on suit la life rencontré
                Suivi = hit.transform;
            } else
            {
                //si on rencontre un obstacle rotation de mon objet sur lui même
                transform.Rotate(-Random.value * 10, -Random.value * 5, 0);

                // si on rencontre un obstacle on decroche parfois de la life qu'on suivait
                if (Random.value < 0.0001f && lifeLead == false)
                {
                   // Debug.Log(gameObject.name + "decrochage" + ": " + random);
                    Suivi = null;
                }
            }
        }
        else //si aucune detection
        {
            //pas de rotation
            transform.Rotate(0, 0, 0);

            //sauf si on suit une life
            if (Suivi != null && lifeLead == false)
            {
                // RETROUVER la life suivi si la distance entre life et life suivi est plus grand que ***    
                if(Vector3.Distance(transform.position, Suivi.position) > 2f)
                {
                    // utilise une rotation smooth en direction de la life suivi
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(Suivi.position - transform.position),
                        1f * Time.deltaTime);
                } else
                {
                    //// IMITER la life suivi quand on est proche             
                    this.gameObject.transform.rotation = Suivi.rotation;
                }                         
            }
        }
    }
    



}
