using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject Projectile;
    public float Delay;
    public bool Enabled;
    public bool ReverseArrow;

    public void Start()
    {
        StartCoroutine(ShootArrows());
    }

    public IEnumerator ShootArrows()
    {
        while (Enabled)
        {
            LaunchArrow();
            Projectile.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Projectile.SetActive(true);
            yield return new WaitForSeconds(Delay);
        }
    }

    void LaunchArrow()
    {
        GameObject obj = Instantiate(Projectile, Projectile.transform.position, Projectile.transform.rotation);
        if (ReverseArrow)
        {
            obj.transform.localScale *= -1;
        }
        obj.GetComponent<Arrow>().Launch();
    }

}
