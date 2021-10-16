using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squadron : PlayerController
{

    public List<GameUnit> myUnits;

    public float totalSpeed;

    public Transform enemyStation;

    public Mission2 plot;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(checkShips());
    }

    private IEnumerator checkShips()
    {
        foreach (var unit in myUnits)
        {
            unit.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);


        foreach (var unit in myUnits)
        {
            unit.RightClick(enemyStation, true, true);
            unit.SetSpeed(1.5f);
        }

        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if(selectedObj.Count == 0)
            {
                plot.Victory();
                yield break;
            }

        }
    }

    private void Update()
    {
        
    }
}
