using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_AI_script : MonoBehaviour
{
    public GameObject[] enemyArray;
    public GameObject enemy;

    bool has_triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        //enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (enemyArray != null)
        {
            print("enemies cleared");
            Destroy(gameObject);
            this.enabled = false;
        }
        return;*/
        if (has_triggered == true)
        {
            int count = enemyArray.Length;
            for (int i = 0; i < enemyArray.Length; i++)
            {
                if (enemyArray[i] == null)
                {
                    count -= 1;
                }
            }
            if (count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && has_triggered == false)
        {
            has_triggered = true;
            for (int i = 0; i < enemyArray.Length - 1; i++)
            {
                float rand = Random.Range(-75, 75);
                Vector3 randPos = new Vector3(rand, 30, rand);
                enemyArray[i + 1] = GameObject.Instantiate(enemy,
                    transform.position + randPos, Quaternion.Euler(0, 0, 0));
                print(enemyArray[i].name + "\n" + " loaded into enemyArray");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject enemy in enemyArray)
            {
                if (enemy != null)
                {
                    enemy.transform.Translate(Vector3.forward * 20f * Time.deltaTime);
                }
            }
        }
        else
        { 
        }
    }
}
