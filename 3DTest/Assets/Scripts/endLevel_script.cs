using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endLevel_script : MonoBehaviour
{
    int changeRotation;
    Text tc;
    Text ts;
    Text to;
    Text ta;
    Text tm;
    Text tk;
    Text levelCompleteBanner;
    Text score;
    Text[] fruitTextArray = new Text[6];

    // Start is called before the first frame update
    void Start()
    {
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

        score = GameObject.Find("PlayerCamera/Canvas/ui_score").GetComponent<Text>();
        levelCompleteBanner = GameObject.Find("PlayerCamera/Canvas/ui_levelComplete").GetComponent<Text>();
        score.text = "Score: ";
        score.color = new Color(255,255,255, 0);
        levelCompleteBanner.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        changeRotation++;
        transform.rotation = Quaternion.Euler(90, 0, changeRotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Endgame.");
            Player_movement.controls_locked = true;
            Vector3 keepPos = other.gameObject.transform.position;
            other.gameObject.transform.position = keepPos;
            levelCompleteBanner.color = new Color(255, 255, 255, 1);
            for (int i = 0; i < fruitTextArray.Length; i++)
            {
                fruitTextArray[i].transform.position = new Vector3(1660, fruitTextArray[i].transform.position.y);
            }

            StartCoroutine(MoveText());

            

        }
    }
    IEnumerator MoveText()
    {
        //Print the time of when the function is first called.
        Debug.Log("Coroutine Stage 1 at: " + Time.time
            + "\n" + "Score: " + global_variables.score);

        score.color = new Color(255, 255, 255, 1);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        float points = global_variables.score;
        score.text = "Score: " + points.ToString();
        Debug.Log("Coroutine Stage 2 at: " + Time.time);
        yield return new WaitForSeconds(3);
        Debug.Log("Coroutine Stage 3." + "\n"
            + "Closing Stage...");
        Application.Quit();
    }
}
