using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hazard_script : MonoBehaviour
{
    BoxCollider bc;
    public Image playerHealthBar;
    GameObject player;
    float pl_health;
    float pl_maxHealth;
    Vector3 playerSPos;
    void Start()
    {
        GetComponent<BoxCollider>();
        pl_health = Player_movement.p_health;
        pl_maxHealth = Player_movement.p_maxHealth;
        player = GameObject.Find("player_pacman");
        Vector3 playerSPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_movement.p_health = 0;
            pl_health = 0;
            float playerCHealth = pl_health;
            float playerMHealth = pl_maxHealth;

            playerHealthBar.fillAmount = .75f - (1 - (playerCHealth / playerMHealth));
            Player_movement.isInvulnerable = true;
            Player_movement.playerMesh.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0.3f);
            Player_movement.controls_locked = true;
            Debug.Log("died in kill hazard");
            StartCoroutine(p_ded());
            
        }
    }
    IEnumerator p_ded()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        Player_movement.isInvulnerable = false;
        Player_movement.playerMesh.GetComponent<Renderer>().material.color = new Color(1f, 210 / 255f, 0f, 1f);
        player.transform.position = playerSPos;
        Player_movement.controls_locked = false;
        Player_movement.p_health = 4;
        pl_health = 4;
        float playerCHealth = pl_health;
        float playerMHealth = pl_maxHealth;

        playerHealthBar.fillAmount = .75f - (1 - (playerCHealth / playerMHealth));
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
