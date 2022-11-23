using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadZone_script : MonoBehaviour
{
    public GameObject[] LoadTheseObjects;
    public GameObject[] UnloadTheseObjects;
    private void Start()
    {
        for (int i = 0; i < LoadTheseObjects.Length; i++)
        {
            LoadTheseObjects[i].SetActive(false);
        }
        for (int i = 0; i < UnloadTheseObjects.Length; i++)
        {
            UnloadTheseObjects[i].SetActive(true);
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < LoadTheseObjects.Length; i++)
            {
                LoadTheseObjects[i].SetActive(true);
            }
            for (int i = 0; i < UnloadTheseObjects.Length; i++)
            {
                UnloadTheseObjects[i].SetActive(false);
            }
        }
    }
}
