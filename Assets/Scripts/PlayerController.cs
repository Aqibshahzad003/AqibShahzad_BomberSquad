using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Gamemanager gamemanager;

    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private float sprintingSpeed;

    [SerializeField] private float rotationspeed;

    public float turnsmoothtime=0.1f;
    private float turnsmoothVelocity;

    [SerializeField] private Animator animator;
    [SerializeField] private float gravity = 9.81f;

    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject DynamitePrefab;

    public GameObject SpawnPoint;
    public float firerate= 0.6f;
    private float nextfiraterate;

    [SerializeField] private float throwForce;
    [SerializeField]private  float UpForce;

    public GameObject enemyspawner;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = FindObjectOfType<Gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        //-------------MovementSystem-----------//
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if(movement.magnitude >= 0.1f  && !Gamemanager.gameOver)
        {
            float targetAangle = Mathf.Atan2(movement.x,movement.z)*Mathf.Rad2Deg;
            float angle =  Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAangle, ref turnsmoothVelocity, turnsmoothtime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            characterController.Move(movement * speed * Time.deltaTime);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            speed = sprintingSpeed;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);

            speed = .9f;
        }
        if(Gamemanager.gameOver)
        {
            animator.SetBool("Death", true);
            Destroy(enemyspawner);
            Invoke("ReplayScene", 4f);
        }


        //----------Making its Gravity----------//
        if (!characterController.isGrounded)
        {
            float gravityMultiplier = 1.0f;
            Vector3 gravityVector = Vector3.down * gravity * gravityMultiplier;

            characterController.Move(gravityVector * Time.deltaTime);
        }
        //----------BombSystem---------//
        if (Input.GetKey(KeyCode.Space)&&Time.time > nextfiraterate)
        {
            nextfiraterate = Time.time + firerate;
            GameObject myBomb = Instantiate(BombPrefab,SpawnPoint.transform.position,transform.rotation);  //spawinging bomb 
            Rigidbody bombrb = myBomb.GetComponent<Rigidbody>();                                          //acceseing its rigidbody for throw force

            Vector3 bombDirection = transform.forward;                                                  

            bombrb.velocity = bombDirection * throwForce + Vector3.up * UpForce;                           //-----------------bomb throw system---------//


            if(Gamemanager.hasPower)  
            {
                firerate = 0.15f;   //increasing the firerate if it has power
            }
            else
            {
                firerate = 0.6f;   //changing it back to original value if it doesnt
            }
        }

        if(Input.GetMouseButtonDown(0)&& Time.time > nextfiraterate && Gamemanager.hasDynamtie) //MineSystem
        {
           Instantiate(DynamitePrefab,transform.position,transform.rotation); //instantiating object with position and rotation same as it is
            gamemanager.DecreaseDynamite(); //decreasing dynamite text by -1
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DynamitePickUp")
        {
            gamemanager.IncreaseDynamite();  //increase text value by +1
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Heart")
        {
            gamemanager.IncreaseHeart();//increase text value by +1
            Destroy(other.gameObject);

        }
        else if(other.gameObject.tag == "Box")
        {
            gamemanager.ShowPowerUp(); //Active the gameobject to show the player has power
            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "Destroyer")
        {
            Gamemanager.gameOver = true;  //if the player falls
            gamemanager.Replay(); //if the player falls
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            Destroy(hit.gameObject);
            gamemanager.DecreaseHeart();

            Debug.Log("enemy hittig");
        }
    }
    
    public void ReplayScene()
    {
        SceneManager.LoadScene(0);
    }
    
}
