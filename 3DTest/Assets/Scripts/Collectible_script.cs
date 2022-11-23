using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible_script : MonoBehaviour
{
    //SphereCollider sc;
    Text t;
    
    int points = global_variables.pacDots;
    int remaining;
    float osc;
    float yTransf;

    public bool doNotHover;
    SphereCollider sc;

    //float osc2;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        

        sc = GetComponent<SphereCollider>();

        t = GameObject.Find("PlayerCamera/Canvas/ui_pdCount").GetComponent<Text>();
        
        
        startPos = transform.position;
        osc = Random.Range(-2.5f, 2.5f);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(osc) <= 1)
        {
            osc = Random.Range(1f, 2f);
        }
        else
        {

        }
        if (!doNotHover)
        {
            transform.position = new Vector3(startPos.x,
                startPos.y + Mathf.Sin(Time.time * osc) * 5,
                startPos.z);
        }                  
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Pac-Dot"))
        {
            global_variables.pacDots += 1;
            remaining = GameObject.FindGameObjectsWithTag("Pac-Dot").Length - 1;
            /*print("Pac-Dots collected: " + global_variables.pacDots
                + "\n" + "Pac-Dots left: " + remaining);*/
            t.text = ("Pac-Dots: " + global_variables.pacDots.ToString());
            global_variables.score += 10;
            //GameObject.Destroy(gameObject);
            gameObject.SetActive(false);
            this.enabled = false;
        }
        if (global_variables.pacDots == 200)
        {
            t.text = "COMPLETE!";
        }
    }
   
    void updateText()
    {

    }
}
