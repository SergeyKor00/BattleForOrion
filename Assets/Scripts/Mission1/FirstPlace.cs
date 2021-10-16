using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlace : MonoBehaviour
{

    public List<GameObject> objects;

    public GameObject mainStation;

    private Mission1 main;

    public CameraMove cam;

    private void Start()
    {
        main = GetComponent<Mission1>();
        StartCoroutine(checkStation());
        StartCoroutine(deactive());
    }

    public void Remove(GameObject go)
    {
        objects.Remove(go);
        if(objects.Count == 0)
        {
            mainStation.SetActive(true);

            string mission = "Отлично, это место готово для постройки базы. Создайте дополнительные корабли, а затем уничтожьте остальные станции противника в этом секторе";
            string task = "уничтожить все силы противника";
            main.NextTask(mission, task);

            GetComponent<SecondPace>().Active();
            cam.forwardCorner = 160;
            cam.rightCorner = 50;
        }
    }

    private IEnumerator checkStation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if (mainStation == null)
            {
                main.Loosing();
            }
            else if(!main.gameObject.activeSelf && !UnitsEnum.FirstCreated.ContainsKey(Players.human))
            {
                main.Loosing();
            }


        }
       
    }

    private IEnumerator deactive()
    {
        yield return new WaitForSeconds(0.5f);
        mainStation.SetActive(false);
    }
}
