using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LearningScript : MonoBehaviour
{

    public List<GameObject> learnPlanes;

    private int index;

    private GameObject currPlane;


    private void Start()
    {
        currPlane = learnPlanes[0];
    }
    public void Next()
    {
        if(index < 7)
        {
            index++;
        }
        currPlane.SetActive(false);

        currPlane = learnPlanes[index];
        currPlane.SetActive(true);


    }

    public void Prev()
    {
        if (index > 0)
        {
            index--;
        }
        currPlane.SetActive(false);

        currPlane = learnPlanes[index];
        currPlane.SetActive(true);
    }

    public void LearnMission()
    {
        SceneManager.LoadScene("Learning");
    }
}
