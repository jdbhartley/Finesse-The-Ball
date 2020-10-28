using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {
    public int LevelNumber;
    public List<GameObject> Numbers;
    public GameObject StarPrefab;
    public Transform Star1;
    public Transform Star2;
    public Transform Star3;
    public GameObject Lock;
    public float NumberPadding;

    bool locked;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
    }

    public void SetStars(int NumberOfStars)
    {
        switch(NumberOfStars)
        {
            case 1:
                Instantiate(StarPrefab, Star1);
                break;
            case 2:
                Instantiate(StarPrefab, Star1);
                Instantiate(StarPrefab, Star2);
                break;
            case 3:
                Instantiate(StarPrefab, Star1);
                Instantiate(StarPrefab, Star2);
                Instantiate(StarPrefab, Star3);
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Level") >= LevelNumber)
        {
            locked = false;
            //List Numbers with padding  
            if (LevelNumber < 10)
            {
                Instantiate(Numbers[LevelNumber], transform);
            }
            if (LevelNumber > 9 && LevelNumber < 20)
            {
                GameObject go = Instantiate(Numbers[LevelNumber / 10], transform);
                go.transform.localPosition -= new Vector3(NumberPadding, 0, 0);
                GameObject go2 = Instantiate(Numbers[LevelNumber % 10], transform);
                go2.transform.localPosition += new Vector3(NumberPadding, 0, 0);
            }
            if (LevelNumber > 19)
            {
                GameObject go = Instantiate(Numbers[LevelNumber / 10], transform);
                go.transform.localPosition -= new Vector3(NumberPadding + 0.05f, 0, 0);
                GameObject go2 = Instantiate(Numbers[LevelNumber % 10], transform);
                go2.transform.localPosition += new Vector3(NumberPadding + 0.05f, 0, 0);
            }
        }
        else
        {
            Instantiate(Lock, transform);
            locked = true;
        }
    }
    private void OnMouseDown()
    {
        if (locked == false)
        {
            SceneManager.LoadScene(LevelNumber);
            Debug.Log("Level " + LevelNumber.ToString() + " Has Been Selected");
        }
    }
}
