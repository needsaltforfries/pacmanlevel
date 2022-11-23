using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_movement : MonoBehaviour
{
    public static GameObject playerMesh;
    public static Renderer playerMeshRender;
    public Image playerHealthBar;

    public static float p_health = 4;
    public static float p_maxHealth = 4;
    public static bool isInvulnerable = false;
    public int move_speed;
    public int rot_speed;
    public int camera_rotSpeed;
    public float playerRotY;

    public Vector3 jump;
    Vector3 playerSPos;
    public float jump_height;
    public static float attack_damage = global_variables.attackDamage * 1;
    public bool onCooldown;
    public bool isGrounded;
    public bool isAttacking;

    public static bool controls_locked = false;

    public GameObject playerCam;
    Animator anim;
    //AnimationEvent evt;
    Rigidbody rb;
    SphereCollider k1;

    public enemy_script enemyScript;

    float playerDirection;
    // Start is called before the first frame update
    void Start()
    {
        print("controls ready");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 8.0f, 0.0f);
        playerCam = GameObject.Find("tracker/PlayerCamera");
        k1 = GetComponent<SphereCollider>();
        k1.enabled = false;
        playerMesh = GameObject.Find("pac_man_mesh");
        playerMeshRender = GetComponentInChildren<Renderer>();
        playerHealthBar = playerCam.GetComponentInChildren<Image>();
        playerSPos = transform.position;
}

    // Update is called once per frame
    void Update()
    {
        playerDirection = playerCam.GetComponent<Transform>().rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Euler(0, playerDirection, 0);
        //Controls
        if (Input.GetKey(KeyCode.W) && controls_locked == false)
        {
            transform.rotation = Quaternion.Euler(0, playerDirection, 0);
            ControlPlayer();
        }
        else if (Input.GetKey(KeyCode.S) && controls_locked == false)
        {
            transform.rotation = Quaternion.Euler(0, playerDirection + 180, 0);
            ControlPlayer();
        }
        else
        {
            anim.SetBool("is_walking", false);
        }
        if (Input.GetKey(KeyCode.A) && controls_locked == false)
        {
            transform.rotation = Quaternion.Euler(0, playerDirection + 270, 0);
            ControlPlayer();

        }
        else if (Input.GetKey(KeyCode.D) && controls_locked == false)
        {
            transform.rotation = Quaternion.Euler(0, playerDirection + 90, 0);
            ControlPlayer();
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded == true && controls_locked == false)
        {
            rb.AddForce(jump * jump_height, ForceMode.Impulse);
            anim.SetBool("is_airborne", true);
            anim.SetBool("is_jumping", true);
            isGrounded = false;
            //controls_locked = false;
        }
        if (Input.GetKey(KeyCode.R) && isGrounded == true)
        {
            controls_locked = true;
            attack_damage = 2;
            anim.Play("kick1_edit");
        }
        else if (Input.GetKey(KeyCode.E) && isGrounded == true)
        {
            controls_locked = true;
            attack_damage = 1;
            anim.Play("punch1");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }
        if (rb.velocity.y > 65)
        {
            rb.velocity = new Vector3(0, 65, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("is_jumping", false);
        anim.SetBool("is_airborne", false);

        anim.Play("land");
        isGrounded = true;
        //controls_locked = false;
        k1.enabled = false;
    }
    void Jump_completed()
    {
        anim.SetBool("is_jumping", false);

    }
    void Attack_start()
    {
        k1.enabled = true;
        controls_locked = true;

        transform.Translate(Vector3.Lerp(transform.position, Vector3.forward * 5, 1f));
    }
    void Attack_end()
    {
        k1.enabled = false;
        controls_locked = false;
    }
    void Attack_kick1_active()
    {
        k1.enabled = true;
    }
    void Attack_kick1_end()
    {
        controls_locked = false;
        k1.enabled = false;
    }
    /*void Punch1_start()
    { 
    }
    void Punch1_end()
    { 
    }
    void Punch2_start()
    { 
    }
    void Punch2_end()
    { 
    }
    */
    void Punch3_start()
    {
        rb.AddForce(jump * jump_height, ForceMode.Impulse);
        k1.enabled = true;
        isGrounded = false;
    }
    void Punch3_end()
    {
        controls_locked = false;
        k1.enabled = false;
        isGrounded = false;
        anim.Play("falling");
        anim.SetBool("is_airborne", true);
    }
    void ControlPlayer()
    {
        if (controls_locked == false)
        {
            anim.SetBool("is_walking", true);
            transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<enemy_script>().Enemy_CurrentHealth -= Player_movement.attack_damage;

            float enemyCHealth = other.gameObject.GetComponent<enemy_script>().Enemy_CurrentHealth;
            float enemyMHealth = other.gameObject.GetComponent<enemy_script>().Enemy_MaxHealth;

            other.gameObject.GetComponent<enemy_script>().HealthBar.fillAmount =
                enemyCHealth / enemyMHealth;

            print("hit enemy: " + other.name
                + "\n" + "damage dealt: " + Player_movement.attack_damage + ", "
                + "enemy health: " + other.gameObject.GetComponent<enemy_script>().Enemy_CurrentHealth);
            other.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 50f, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Launchable"))
        {
            print("hit breakable");
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * jump_height * .75f + transform.forward * jump_height * 2, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("ChainStarter"))
        {
            anim.SetBool("inChain", true);
            anim.Play("pelletdash");
            Debug.Log("touched Chain Starter");
        }
    }
    public void ReceiveDamage()
    {
        controls_locked = false;
        float playerCHealth = p_health;
        float playerMHealth = p_maxHealth;

        playerHealthBar.fillAmount = .75f - (1 - (playerCHealth / playerMHealth));
        Player_movement.isInvulnerable = true;
        if (p_health == 0)
        {
            Player_movement.isInvulnerable = false;
            Player_movement.playerMesh.GetComponent<Renderer>().material.color = new Color(1f, 210 / 255f, 0f, 1f);
            transform.position = playerSPos;
            p_health = 4;
            playerCHealth = p_health;
            playerHealthBar.fillAmount = .75f - (1 - (playerCHealth / playerMHealth));
        }
        else
        {
            StartCoroutine(Invulnerable()); 
        }
    }
    IEnumerator Invulnerable()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        Player_movement.playerMesh.GetComponent<Renderer>().material.color = new Color(0f, 0.05f, 255f, 0.3f);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Player_movement.isInvulnerable = false;
        Player_movement.playerMesh.GetComponent<Renderer>().material.color = new Color(1f, 210 / 255f, 0f, 1f);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
