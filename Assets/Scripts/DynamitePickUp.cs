using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamitePickUp : MonoBehaviour
{
    private ParticleSystem DestroyParticle;

    // Start is called before the first frame update
    void Start()
    {
        DestroyParticle = GetComponentInChildren<ParticleSystem>();

        Invoke("TimeOut", 6f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 100f * Time.deltaTime);
    }
    void TimeOut()
    {
        DestroyParticle.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject,.4f);

    }
}
