using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlatform_script : MonoBehaviour
{
    public GameObject respawnThisPlat;
    Vector3 respawnPos;
    private void Start()
    {
        respawnPos = respawnThisPlat.transform.position; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawnThisPlat.transform.position = respawnPos;
            platform2script.current = 1;
        }
    }
}
