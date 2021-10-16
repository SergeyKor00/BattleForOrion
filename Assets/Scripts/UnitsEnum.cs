using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Players
{
    human, programm1
}

public class UnitsEnum : MonoBehaviour, IEnumerable
{

    public Players myType;

    public bool InScene;

    // компонент юнита под контролем игрока, содержит интерфейс перебора IEnumerator
    public static UnitsEnum FirstCreate { get; private set; }
    public static UnitsEnum LastCreate { get; private set; }

    public UnitsEnum Next { get; private set; }
    public UnitsEnum Prev { get; private set; }

    public static Dictionary<Players, UnitsEnum> FirstCreated = new Dictionary<Players, UnitsEnum>();
    public static Dictionary<Players, UnitsEnum> LastCreated = new Dictionary<Players, UnitsEnum>();

    public bool IsStantion { get { return gameObject.layer == 8; } }

    public IEnumerator GetEnumerator()
    {
        return new UnitsEnumerator(myType);
    }
    void Awake()
    {
        if (!InScene)
            return;

        Add();
    }


    public void Add()
    {
        if (!FirstCreated.ContainsKey(myType))
        {
            FirstCreated.Add(myType, this);
            LastCreated.Add(myType, this);
            // return;
        }
        else
        {
            if (LastCreated[myType] != null)
            {
                LastCreated[myType].Next = this;
                Prev = LastCreated[myType];
            }

            LastCreated[myType] = this;
        }
    }


    private void OnDestroy()
    {
        if(FirstCreated[myType] == LastCreated[myType])
        {
            FirstCreated.Remove(myType);
            LastCreated.Remove(myType);
        }
        else
        {
            if (FirstCreated[myType] == this) // Измененный код
                FirstCreated[myType] = Next;

            if (LastCreated[myType] == this)
                LastCreated[myType] = Prev;

            if (Prev != null)
                Prev.Next = Next;
            if (Next != null)
                Next.Prev = Prev;
        }


       
    }

}



