using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class collectible_fruit_script : MonoBehaviour
{
    Text tc;
    Text ts;
    Text to;
    Text ta;
    Text tm;
    Text tk;
    Text[] fruitTextArray = new Text[6];
    float yTransf;
    float osc;
    public bool doNotHover;
    bool stopMoving;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        fruitTextArray[0] = GameObject.Find("PlayerCamera/Canvas/ui_fruit_cherry").GetComponent<Text>();
        fruitTextArray[1] = GameObject.Find("PlayerCamera/Canvas/ui_fruit_strawberry").GetComponent<Text>();
        fruitTextArray[2] = GameObject.Find("PlayerCamera/Canvas/ui_fruit_orange").GetComponent<Text>();
        fruitTextArray[3] = GameObject.Find("PlayerCamera/Canvas/ui_fruit_apple").GetComponent<Text>();
        fruitTextArray[4] = GameObject.Find("PlayerCamera/Canvas/ui_fruit_melon").GetComponent<Text>();
        fruitTextArray[5] = GameObject.Find("PlayerCamera/Canvas/ui_fruit_key").GetComponent<Text>();

        tc = fruitTextArray[0];
        ts = fruitTextArray[1];
        to = fruitTextArray[2];
        ta = fruitTextArray[3];
        tm = fruitTextArray[4];
        tk = fruitTextArray[5];

        for (int i = 0; i < fruitTextArray.Length; i++)
        {
            fruitTextArray[i].transform.position = new Vector3(2500, fruitTextArray[i].transform.position.y);
        }
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
        if (!doNotHover && !stopMoving)
        {
            transform.position = new Vector3(startPos.x,
                startPos.y + Mathf.Sin(Time.time * osc) * 5,
                startPos.z);
        }
        else
        {
            
        }
        yTransf++;
        transform.rotation = Quaternion.Euler(
            transform.rotation.x, yTransf, transform.rotation.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Fruit"))
        {
            if (this.gameObject.name == "cherry")
            {
                global_variables.cherry += 1;
                tc.text = "Cherries: " + global_variables.cherry.ToString();
                global_variables.score += 100;

                if (global_variables.cherry == 5)
                {
                    tc.text = "COMPLETE!";
                }
                StartCoroutine(MoveText());
            }
            else if (this.gameObject.name == "strawberry")
            {
                global_variables.strawberry += 1;
                ts.text = "Strawberries: " + global_variables.strawberry.ToString();
                global_variables.score += 300;
                if (global_variables.strawberry == 5)
                {
                    ts.text = "COMPLETE!";
                }
                StartCoroutine(MoveText());
            }
            else if (this.gameObject.name == "orange")
            {
                global_variables.orange += 1;
                to.text = "Oranges: " + global_variables.orange.ToString();
                global_variables.score += 500;
                if (global_variables.orange == 5)
                {
                    to.text = "COMPLETE!";
                }
                StartCoroutine(MoveText());
            }
            else if (this.gameObject.name == "apple")
            {
                
                global_variables.apple += 1;
                ta.text = "Apples: " + global_variables.apple.ToString();
                global_variables.score += 700;
                if (global_variables.apple == 4)
                {
                    ta.text = "COMPLETE!";
                }
                StartCoroutine(MoveText());
            }
            else if (this.gameObject.name == "melon")
            {
                global_variables.melon += 1;
                tm.text = "Melons: " + global_variables.melon.ToString();
                global_variables.score += 1000;
                if (global_variables.melon == 5)
                {
                    tm.text = "COMPLETE!";
                }
                StartCoroutine(MoveText());
            }
            else if (this.gameObject.name == "key")
            {
                global_variables.key += 1;
                tk.text = "Keys: " + global_variables.key.ToString();
                global_variables.score += 5000;
                if (global_variables.key == 4)
                {
                    tk.text = "COMPLETE!";
                }
                StartCoroutine(MoveText());
            }
        }
    }
    IEnumerator MoveText()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time 
            + "\n" + gameObject.name + " collected.");
        transform.position = new Vector3(-100, -100);

        stopMoving = true;

        for (int i = 0; i < fruitTextArray.Length; i++)
        {
            fruitTextArray[i].transform.position = new Vector3(1660, fruitTextArray[i].transform.position.y);
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        for (int i = 0; i < fruitTextArray.Length; i++)
        {
            fruitTextArray[i].transform.position = new Vector3(2500, fruitTextArray[i].transform.position.y);
        }
        GameObject.Destroy(gameObject);
        this.enabled = false;
    }
}
