using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStar : MonoBehaviour {
    public GameObject PopStars;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().CollectStar();
            Instantiate(PopStars, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
