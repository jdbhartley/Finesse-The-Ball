using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public float Speed;
    public Vector3 Direction;
    public bool launched = false;
    AudioSource audioSource;
    public AudioClip launch;
    public AudioClip hit;
    public float ProximityToPlayer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (launched)
        {
            transform.Translate(Direction * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Death();
        }
        else if (!collision.CompareTag("Arrow"))
        {
            launched = false;
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < ProximityToPlayer)
            {
                audioSource.PlayOneShot(hit);
            }
            Destroy(this.GetComponent<Collider2D>());
            StartCoroutine(DestroySelf());
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    public void Launch()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < ProximityToPlayer)
        {
            audioSource.PlayOneShot(launch);
        }
        launched = true;
    }
}
