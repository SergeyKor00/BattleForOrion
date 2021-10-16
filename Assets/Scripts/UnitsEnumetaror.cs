using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsEnumerator : IEnumerator
{
    private UnitsEnum currentObj;

    private Players type;


    public UnitsEnumerator(Players type)
    {
        this.type = type;

    }
    public object Current
    {
        get { return currentObj; }
    }
    public bool MoveNext()
    {
        currentObj = (currentObj == null) ? UnitsEnum.FirstCreated[type] : currentObj.Next;
        return currentObj != null;
    }
    public void Reset()
    {
        currentObj = null;
    }
}
