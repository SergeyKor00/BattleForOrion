using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPace : MonoBehaviour
{

    public GameObject[] hidenList;

    private void Start()
    {
       // StartCoroutine(deactive());
    }

    private IEnumerator deactive()
    {
        yield return new WaitForSeconds(0.3f);
        foreach(var go in hidenList)
        {
            go.SetActive(false);
        }
    }
    public void Active()
    {
        foreach(var go in hidenList)
        {
            go.SetActive(true);
        }
    }
}
