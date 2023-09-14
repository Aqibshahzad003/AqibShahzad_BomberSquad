using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public GameObject childParticle;
    public float radius;
    public float BombBlastForce;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Blast", 1.5f);
    }

    void Blast()
    {

        Collider[] col = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in col)
        {
            
            
                Enemy enemyscript = nearbyObject.GetComponent<Enemy>();

                if (enemyscript != null)
                {
                    enemyscript.Destroy();
                }
                Debug.Log("exploded");
            
        }
        childParticle.SetActive(true);
        Destroy(gameObject, .35f);
    }
}
