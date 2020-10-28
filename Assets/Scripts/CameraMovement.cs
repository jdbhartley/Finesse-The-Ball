using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
    public Transform Target;
    Tween cameraTween;
    public float TweenDuration;
    Vector3 startingPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //Keep in screen bounds.
        if (Target.position.x > 0 && Target.position.x < 34.2)
        {
            transform.position = new Vector3(Target.position.x, 0, -10f);
        }
	}
}
