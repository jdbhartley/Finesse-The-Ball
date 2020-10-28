using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxBG : MonoBehaviour {
    Transform camera;
    Vector3 startingPos;
    Vector2 cameraStartingPos;
    float xOffset;
    float lastXOffset;

    public float Speed;


	// Use this for initialization
	void Start () {
        camera = Camera.main.transform;
        startingPos = transform.localPosition;
        cameraStartingPos = camera.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Get Camera Offset;
        xOffset = camera.position.x - cameraStartingPos.x;
        transform.localPosition = startingPos - new Vector3((xOffset / 100) * Speed, 0, 0);
	}
}
