using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LEarning : MonoBehaviour
{
    [TextArea]
    public string[] questList;

    public int index = 0;

    public GameObject info;
    public Text text;

    public Transform cam;

    public Transform fighters, frigates, crusers;

    public GameObject target;

    public GameObject station;

    public void Next(int i)
    {

        if(i == index)
        {
            info.SetActive(true);
            text.text = questList[index];
            index++;

            switch (i) {
                case 0:
                    StartCoroutine(Camera());
                    break;
                case 1:
                    fighters.gameObject.SetActive(true);
                    foreach (var f in fighters.GetComponentsInChildren<Transform>())
                        f.SetParent(null);
                    break;
                case 4:
                    target.SetActive(true);
                    break;
                case 5:
                    frigates.gameObject.SetActive(true);
                    break;
                case 6:
                    crusers.gameObject.SetActive(true);
                    break;
                case 7:
                    station.SetActive(true);
                    break;

            }

        }


    }

    private IEnumerator Camera()
    {
        while (true)
        {
            Vector3 start = cam.position;
            yield return new WaitForSeconds(1.0f);
            if (cam.position != start)
            {
                Next(index);
                yield break;
            }
        }
        
    }
}
