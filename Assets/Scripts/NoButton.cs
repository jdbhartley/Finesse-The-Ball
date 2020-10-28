using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoButton : MonoBehaviour {

    private void OnMouseDown()
    {
        SceneManager.LoadScene(0);
    }
}
