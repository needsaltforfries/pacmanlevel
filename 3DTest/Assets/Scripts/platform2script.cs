using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform2script : MonoBehaviour
{
    public Transform[] target;
    public static int current = 1;
    public float speed;
    bool connected;
    GameObject player;
    CapsuleCollider cc;
    Rigidbody rb;
    public bool isOneTimeOnly;
    public bool lockControls;
    int state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        player = GameObject.Find("player_pacman");
    }

    // Update is called once per frame
    void Update()
    {
        if (connected && transform.position != target[current].position)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position,           
                target[current].position, speed * Time.deltaTime);
            player.transform.position = Vector3.MoveTowards(transform.position, 
                target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(transform.position);
        }
        else
        {
            if (connected)
            {
                current = (current + 1) % target.Length;
                
            }
        }
        if (isOneTimeOnly && current == 0)
        {
            rb.velocity = new Vector3(0,0,0);
            current = (current + 1) % target.Length;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player_movement.controls_locked = false;
            Destroy(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            connected = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (lockControls)
            {
                Player_movement.controls_locked = true;
            }
            if (Input.GetKey(KeyCode.Space) && !isOneTimeOnly)
            {
                connected = false;
                current = 1;
            }
            
        }
    }
}
