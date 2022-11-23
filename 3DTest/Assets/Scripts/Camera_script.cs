using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 camPos;
    public Transform player;
    public int rotSpeed;
    public bool camera_zoomedIn = false;
    public bool camera_zoomedOut = true;

    public Transform player_Cam;
    public Transform cam;

    public static float camDirectionX;
    public static float camDirectionY;

    GameObject tracker;
    Rigidbody rb;
    //SphereCollider sc;

    void Start()
    {
        camPos = player.position + new Vector3(-80, 75, 0);
        rb = GetComponent<Rigidbody>();
        //sc = GetComponent<SphereCollider>();

        //transform.position = camPos;
        //transform.rotation = Quaternion.Euler(20, 90, 0);

        tracker = GameObject.Find("tracker");
        tracker.transform.position = player.transform.position;
        player_Cam.transform.LookAt(player);
        transform.position = player.position + new Vector3(0, 65, -110);
        transform.rotation = Quaternion.Euler(20, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //camDirectionX += Input.GetAxis("Mouse X")*Time.deltaTime * 90;
        player_Cam.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 2f);
        player_Cam.RotateAround(player.transform.position + new Vector3(0, 50, 0),
                transform.right, Input.GetAxis("Mouse Y") * -2f);
        if (Input.GetKeyDown(KeyCode.F))
        {
            player_Cam.transform.LookAt(player);
            transform.position = player.position + new Vector3(0, 65, -110);
            transform.rotation = Quaternion.Euler(20, 0, 0);

        }
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            player_Cam.RotateAround(player.transform.position + new Vector3(0, 50, 0),
                transform.right, -1f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player_Cam.RotateAround(player.transform.position + new Vector3(0, 50, 0),
                transform.right, 1f);
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.position = player.position + new Vector3(-40, 65, -40);
        transform.LookAt((player.localPosition) + new Vector3(0, 20, 0));
        rb.velocity = new Vector3(0, 0, 0);
    }
}
