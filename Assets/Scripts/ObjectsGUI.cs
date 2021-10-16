using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGUI : MonoBehaviour
{

    public SpriteRenderer miniMap, selectRender;

    public Sprite selection;

    private Transform selectTransform;

    private void Start()
    {
        selectTransform = selectRender.transform;
    }
    // Update is called once per frame
    void Update()
    {
        selectTransform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
