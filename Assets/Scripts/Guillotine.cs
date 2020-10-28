using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour
{
    public float Speed;
    public bool Reverse;
    private Vector3 maxRot;
    private Vector3 dir = Vector3.forward;
    bool goingRight = true;

    private void Start()
    {
        if (Reverse)
        {
            dir *= -1;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        float modifier = Mathf.Abs(this.transform.rotation.z) * 220;
        Debug.Log("Modifier" + modifier);

        this.transform.Rotate(dir * (Speed-modifier) * Time.deltaTime);
        Debug.Log("Rotation: " + this.transform.rotation.z);
        if (this.transform.rotation.eulerAngles.z > 65 && this.transform.rotation.eulerAngles.z < 250 && dir.z > 0)
        {
           dir *= -1;
        }  
        else if (this.transform.rotation.eulerAngles.z < 300 && this.transform.rotation.eulerAngles.z > 250 && dir.z < 0)
        {
            dir *= -1;
        }
    }
}
