using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class followchain_script : MonoBehaviour
{
    public Transform[] target;
    public GameObject player;
    public float speed;
    public bool isActive;
    private int current = 1;
    bool collected;
    SphereCollider sc;
    public Text t;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        dist = Vector3.Distance(transform.position, target[current].transform.position);
        sc = GetComponent<SphereCollider>();
        t = GameObject.Find("PlayerCamera/Canvas/ui_pdCount").GetComponent<Text>();
        collected = false;
        Debug.Log("distance between current and dot:" + dist);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, target[current].transform.position);
        if (isActive == true && dist > 0.01)
        {
             player.transform.position = Vector3.MoveTowards(player.transform.position,
                target[current].position, speed * Time.deltaTime);

             transform.position = Vector3.MoveTowards(transform.position,
                target[current].position, speed * Time.deltaTime);

             GetComponent<Rigidbody>().MovePosition(transform.position);
             GetComponent<Rigidbody>().MovePosition(player.transform.position);
        }
        else
        {
            if (isActive == true)
            {           
                Debug.Log("Current: " + current); 
                global_variables.pacDots++;
                t.text = "Pac-Dots: " + global_variables.pacDots.ToString();
                //target[current].transform.position = new Vector3(-100,-100,-100);              
                target[current].gameObject.SetActive(false);
                current = (current + 1) % target.Length;
                dist = Vector3.Distance(transform.position, target[current].transform.position);
            }
        }
        if (current <= 0)
        {
            //global_variables.pacDots++;
            t.text = ("Pac-Dots: " + global_variables.pacDots.ToString());
            Player_movement.controls_locked = false;
            isActive = false;
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            GameObject.Destroy(target[current].gameObject);
            Debug.Log(global_variables.pacDots + "pacdots collected");
            global_variables.score += target.Length * 10;
            Debug.Log("Score: " + global_variables.score);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            isActive = true;
            Player_movement.controls_locked = true;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            target[0].position = new Vector3(0, 0, 0);
            global_variables.pacDots++;
            t.text = ("Pac-Dots: " + global_variables.pacDots.ToString());
            collected = true;
            //this.transform.position = new Vector3(0,0,0);
        }
        else
        { 
        }
    }
}
