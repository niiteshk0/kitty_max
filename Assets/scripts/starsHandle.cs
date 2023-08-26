using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starsHandle : MonoBehaviour
{
    public GameObject[] stars;
    private int coinsCount;
    // Start is called before the first frame update
    void Start()
    {
        coinsCount = GameObject.FindGameObjectsWithTag("coin").Length;
    }

    public void starsAchieved()
    {
        int coinsLeft = GameObject.FindGameObjectsWithTag("coin").Length;
        int coinsCollected = coinsCount - coinsLeft;

        float percentage =float.Parse (coinsCollected.ToString() )/ float.Parse (coinsCount.ToString()) * 100f;
        
        if(percentage >= 33f && percentage < 66)
        {
            // one star
            stars[0].SetActive(true);
        }
        else if (percentage >= 66 && percentage < 70)
        {
            // two star
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        } 
        else if(percentage <= 30)
        { }

        else
        {
            // three star
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }

    }
}
    