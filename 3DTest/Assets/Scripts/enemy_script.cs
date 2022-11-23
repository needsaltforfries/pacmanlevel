using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_script : MonoBehaviour
{
    public GameObject player;
    public GameObject player_cam;
    Player_movement playerScript;
    public float Health;
    public float playerHealthPoints;
    public Image HealthBar;
    public Image playerHealth;

    public float Enemy_MaxHealth;
    public float Enemy_CurrentHealth;
    public bool dead = false;
    //public int enemyCount;
    float playerDirection;
    //public static GameObject[] enemies;
    Canvas cv;
    SphereCollider sc;
    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < 4; i++)
            enemies[i]++;
        */
        /*enemies = GameObject.FindGameObjectsWithTag("Enemy");
        print("enemies found: " + enemies.Length);

        enemies[0] = GameObject.Find("Enemy1");
        enemies[1] = GameObject.Find("Enemy2");
        print(enemies[0].name);

        //enemies[i]. = Enemy_MaxHealth = Health;
        */
        Enemy_MaxHealth = Health;
        Enemy_CurrentHealth = Enemy_MaxHealth;
        cv = gameObject.transform.GetChild(0).GetComponent<Canvas>();
        HealthBar = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        player_cam = GameObject.Find("PlayerCamera");
        playerHealth = GameObject.Find("PlayerCamera").GetComponent<Image>();
        sc = GetComponent<SphereCollider>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //cv.transform.LookAt(player_cam.transform);
        playerDirection = player_cam.transform.rotation.eulerAngles.y;

        transform.LookAt(player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        cv.transform.rotation = Quaternion.Euler(0, playerDirection, 0);

        if (Enemy_CurrentHealth <= 0)
        {
            dead = true;
            global_variables.score += 300;
            Destroy(gameObject, 1.5f);
            this.enabled = false;
        }

    }
    public void takeDamage()
    {

        HealthBar.fillAmount = Enemy_CurrentHealth / Enemy_MaxHealth;
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && Player_movement.isInvulnerable == false)
        {
            Player_movement.p_health -= 1;

            playerScript.ReceiveDamage();
            
            print("taken damage!"
                +"\n" + "current health: " + Player_movement.p_health);
            other.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 65f, ForceMode.Impulse);
        }

    }

}
