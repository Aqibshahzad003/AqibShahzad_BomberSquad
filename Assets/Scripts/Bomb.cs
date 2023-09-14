using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float force;

    public float radius;
    public float BombBlastForce;

    private ParticleSystem bombparticle;
    // Start is called before the first frame update
    void Start()
    {
        bombparticle = GetComponentInChildren<ParticleSystem>();
        Invoke("Blast", 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
   
    }
    void Blast()
    {
        bombparticle.Play();

         Collider[] col =  Physics.OverlapSphere(transform.position, radius);  //accesing the colliders of the nearby objects by using a sphere overlap

        foreach(Collider nearbyObject in col)   //destroyinn each gameobject which is in sphere 
        {
            
                Enemy enemyscript = nearbyObject.GetComponent<Enemy>();  //accessing the script to call the functin of destroying

                if(enemyscript != null) 
                {
                   enemyscript.Destroy();   
                }
                Debug.Log("exploded");
            
        }
        Transform Child1 = transform.GetChild(1);
        Child1.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject,.5f);
    }
}
