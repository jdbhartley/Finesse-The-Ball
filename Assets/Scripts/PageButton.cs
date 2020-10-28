using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButton : MonoBehaviour {

    private void OnMouseDown()
    {
        GameObject.FindWithTag("MenuGen").GetComponent<MenuGen>().NextPage();
    }
}
