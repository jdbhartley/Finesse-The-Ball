using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    public GameObject ParticleSystems;
    public GameObject FinishText;
    public GameObject ScorePanel;
    float timeFinished;
    byte totalStars;
    bool finished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && finished == false)
        {
            finished = true;
            Debug.Log("COMPLETE");
            totalStars = collision.GetComponent<Player>().GetStars();

            foreach (ParticleSystem ps in ParticleSystems.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();
            }

            GetComponent<AudioSource>().Play();
            FinishText.transform.DOLocalMoveY(3.73f, 1.6f);
            
            timeFinished = Time.timeSinceLevelLoad;
            Debug.Log(timeFinished + " FINISHED TIME");

            //Tell player hes finished to cancel input
            collision.GetComponent<Player>().Finished();

            //Show the Score Panel
            ScorePanel.transform.DOLocalMoveY(-1f, 2.2f).OnComplete(ShowScore);

            //Add it to the JSON
            List<LevelScores> levelScores = JsonConvert.DeserializeObject<List<LevelScores>>(PlayerPrefs.GetString("LevelScores"));

            //Set the Stars
            levelScores[SceneManager.GetActiveScene().buildIndex].TotalStars = totalStars;

            //Update playerprefs
            PlayerPrefs.SetString("LevelScores", JsonConvert.SerializeObject(levelScores));

            //Update total level
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void ShowScore()
    {
        ScorePanel.GetComponent<ScorePanel>().StartShowScore(totalStars);
    }
}
