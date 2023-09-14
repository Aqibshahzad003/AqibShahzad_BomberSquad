using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public TextMeshProUGUI hearttext;
    private int heart;
    public static bool gameOver = false;

    int dynamite;
    public TextMeshProUGUI dynamitetext;

    int wave;
    public TextMeshProUGUI wavetext;

    public GameObject PowerUp;
    public static bool hasPower = false;
    public static bool hasDynamtie = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        //setting the value of liffe
        heart = 3;
        hearttext.text = heart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (heart == 0)   //if life is down to zero then reload the scene 
        {
            gameOver = true;
        }
        if(dynamite >= 1)
        {
            hasDynamtie = true;
        }
        else
        {
            hasDynamtie = false;
        }
    }

    public void IncreaseHeart()  //increasing life
    {
        Debug.Log("Increaselife function called");

        if (heart < 3)
        {
            Debug.Log("Life Increased");
            heart++;
            hearttext.text = heart.ToString();
        }
    }
    public void DecreaseHeart()  //decreasing life
    {
        heart--;
        hearttext.text = heart.ToString();
    }
    public void IncreaseDynamite()  //increasing dynamite by 1
    {
        Debug.Log("dnynamite increase function called");

        if (dynamite < 3)
        {
            dynamite++;
            dynamitetext.text = dynamite.ToString();
        }
    }
    public void DecreaseDynamite()  //decreasing by 1
    {
        dynamite--;
        dynamitetext.text = dynamite.ToString();
    }

   
    public void ShowPowerUp()
    {
        hasPower = true;

        if (hasPower)
        {
            PowerUp.gameObject.SetActive(true);
        }
       
        StartCoroutine(StopPowerUp());

    }
    IEnumerator StopPowerUp()
    {
        Debug.Log("PowerUP!!");
        yield return new WaitForSeconds(10f);
        hasPower = false;
        PowerUp.gameObject.SetActive(false);

        StopCoroutine(StopPowerUp());
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
