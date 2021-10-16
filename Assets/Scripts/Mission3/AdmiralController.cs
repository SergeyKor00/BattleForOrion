using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmiralController : PlayerController
{
    public Mission3 plot;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkShips());
    }


    private IEnumerator checkShips()
    {
        yield return new WaitForSeconds(10.5f);
        foreach (UnitsEnum unit in UnitsEnum.FirstCreated[Players.programm1])
        {
            var ship = unit.GetComponent<GameUnit>();

            pointObject.position = ship.transform.position + ship.transform.forward * 100.0f;
            ship.RightClick(pointObject, true, true);

            ship.SetSpeed(2.0f);

        }


        while (true)
        {
            try
            {
                if (UnitsEnum.FirstCreated[Players.programm1] == null)
                {

                    plot.Victory();
                    yield break;
                }
                //yield return new WaitForSeconds(2.0f);
            }
            catch (KeyNotFoundException)
            {
                Debug.Log("Catch");
                plot.Victory();
                yield break;
            }
            yield return new WaitForSeconds(2.0f);
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
