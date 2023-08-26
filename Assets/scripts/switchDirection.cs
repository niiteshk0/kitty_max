using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisonEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Max")
        {
            col.gameObject.GetComponent<maxScript>().SwitchDirection();   
        }
    }
}
