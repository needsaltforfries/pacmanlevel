using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_script : MonoBehaviour
{
    float osc;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        osc = Random.Range(-2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPos.x,
            startPos.y + Mathf.Cos(Time.time * 2) * 75,
            startPos.z);
    }
}
