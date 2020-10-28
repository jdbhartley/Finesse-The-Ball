using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRigidbody : MonoBehaviour
{
    public List<Rigidbody2D> Rigidbodies;
    bool triggered = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Light Gizmo.tiff");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");

        if (triggered == false && collision.CompareTag("Player"))
        {
            foreach (Rigidbody2D rb in Rigidbodies)
            {
                rb.simulated = true;
            }
            triggered = true;
        }
    }
}
