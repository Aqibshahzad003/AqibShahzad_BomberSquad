using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float movingSpeed = 3.0f;
    private Gamemanager gamemanager;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//finding teh player for following
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        //-----------Player follwing system-------------//
        if (player != null)   
        {
            if (!Gamemanager.gameOver)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;
                directionToPlayer.Normalize();

                transform.LookAt(player.transform);  

                transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
            }
        }
        if(Gamemanager.gameOver)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Destroyer")  //if the enemy falls for some reason
        {
            Destroy(gameObject);
        }
    }

}
