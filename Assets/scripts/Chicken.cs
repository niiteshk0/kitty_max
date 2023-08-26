using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{

    public GameObject player;
    public bool follow = false;
    //private kittyScript _kitty;
    // Start is called before the first frame update
    void Start()
    {
        //_kitty = player.GetComponent<kittyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!follow)
            return;

        if(player.GetComponent<SpriteRenderer>().flipX)
        {
            //kitty move right so chicken should also move right
            this.gameObject.transform.position = Vector2.MoveTowards(
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z), 6f * Time.deltaTime);
        }
        else
        {
            //kityy move left so chicken should also move left
            this.gameObject.transform.position = Vector2.MoveTowards(
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z), 6f * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            //check if already has children or not 
            int children = player.transform.childCount;
            if (children == 2)
                return;

            follow = true;
            transform.SetParent(player.transform);   // ye krne pe kitty ka child ban jayega chicken if touch hua to 
        }

        if (coll.tag == "Max")
        {
            follow = false;
            transform.parent = null;
        }
    }
}
