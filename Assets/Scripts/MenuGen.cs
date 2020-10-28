using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Newtonsoft.Json;

public class MenuGen : MonoBehaviour {
    public GameObject LevelButton;
    public GameObject LockIcon;
    public GameObject PageButton;
    public List<GameObject> Numbers;
    public List<GameObject> Levels;
    List<LevelScores> LevelScoresList = new List<LevelScores>();

    public int NumberOfLevelsWidth = 4;
    public int NumberOfLevelsHeight = 6;
    public float Padding;
    int totalLevels = 48;
    int levelnumber = 1;
    float movePoint = -5f;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("LevelScores"))
        {
            //Create the LevelScoresList
            for (int i = 1; i <= totalLevels; i++)
            {
                LevelScores levelScores = new LevelScores();
                levelScores.LevelNumber = i;
                levelScores.TotalStars = 0;

                LevelScoresList.Add(levelScores);
            }

            //Serialize it and add to playerprefs as a string
            string json = JsonConvert.SerializeObject(LevelScoresList);
            PlayerPrefs.SetString("LevelScores", json);
        }
    }

    // Use this for initialization
    void Start () {
        //Reset timescale for bug
        Time.timeScale = 1;

        //Grab the scores from JSON
        LevelScoresList = JsonConvert.DeserializeObject<List<LevelScores>>(PlayerPrefs.GetString("LevelScores"));

        //Setup the page of levels
        for (int y = 0; y < NumberOfLevelsHeight; y++)
        {
            for (int x = 0; x < NumberOfLevelsWidth; x++)
            {
                GameObject go = Instantiate(LevelButton, new Vector3(transform.position.x + (x * Padding), transform.position.y - (y * Padding), 0), Quaternion.identity);
                go.GetComponent<LevelButton>().LevelNumber = levelnumber;
                go.GetComponent<LevelButton>().SetStars(LevelScoresList[levelnumber].TotalStars);
                Levels.Add(go);
                levelnumber++;
            }
        }
	}

    public void NextPage()
    {
        movePoint = -5f;

        if (levelnumber > 25)
        {
            levelnumber = 1;
            movePoint = 5f;
        }
        else
        {
            movePoint = -5f;
        }

        int i = 0;
        foreach (GameObject go in Levels)
        {
            go.transform.DOMoveX(go.transform.position.x + movePoint, 0.5f);

            if (i == Levels.Count-1)
            {
                go.transform.DOMoveX(go.transform.position.x + movePoint, 0.5f).OnComplete(FinishedTweenNextPage);
            }

            i++;
        }
    }

    void FinishedTweenNextPage()
    {
        //Remove all
        foreach (GameObject go in Levels)
        {
            Destroy(go);
        }
        Levels.Clear();

        for (int y = 0; y < NumberOfLevelsHeight; y++)
        {
            for (int x = 0; x < NumberOfLevelsWidth; x++)
            {
                GameObject go = Instantiate(LevelButton, new Vector3(transform.position.x + (x * Padding) + -movePoint, transform.position.y - (y * Padding), 0), Quaternion.identity);
                go.GetComponent<LevelButton>().LevelNumber = levelnumber;
                Levels.Add(go);
                go.transform.DOMoveX(transform.position.x + (x * Padding), 0.5f);
                levelnumber++;
            }
        }

        PageButton.transform.Rotate(Vector3.forward * 180);
    }
}
