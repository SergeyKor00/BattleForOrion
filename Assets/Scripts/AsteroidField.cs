using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public int miningCount, maxCount;


    public bool FieldFull { get { return miningCount == maxCount; } }
}
