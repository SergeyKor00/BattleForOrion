using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    // движение камеры по игровому полю
    public float leftCorner;
    public float rightCorner;
    public float backCorner;
    public float forwardCorner;
    // public GameObject activeBuild;

    public float speed;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z > backCorner && (int)Input.mousePosition.y < 2)
          transform.position -= Vector3.forward * speed * Time.deltaTime;
        else if (transform.position.z < forwardCorner && (int)Input.mousePosition.y > Screen.height - 2)
            transform.position += Vector3.forward * speed * Time.deltaTime;
        if (transform.position.x > leftCorner && (int)Input.mousePosition.x < 2)
            transform.position -= Vector3.right * speed * Time.deltaTime;
        else if (transform.position.x < rightCorner && (int)Input.mousePosition.x > Screen.width - 2)
            transform.position += Vector3.right * speed * Time.deltaTime;

    }
}
