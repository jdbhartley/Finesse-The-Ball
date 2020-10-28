using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScorePanel : MonoBehaviour {
    public Transform Star1;
    public Transform Star2;
    public Transform Star3;

    public GameObject StarPrefab;
    GameObject thisStar1;
    GameObject thisStar2;
    GameObject thisStar3;

    public AudioClip StarSound;
    AudioSource thisAudioSource;

    Vector3 starInitSize = new Vector3(5f, 5f, 5f);
    Quaternion starInitRotation = new Quaternion(0, 0, 180, 0);

    private void Start()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }

    public void StartShowScore(int NumberOfStars)
    {
        StartCoroutine(ShowScore(NumberOfStars));
    }

    IEnumerator ShowScore(int NumberOfStars)
    {

        switch (NumberOfStars)
        {
            case 1:
                thisStar1 = Instantiate(StarPrefab, Star1);
                thisStar1.transform.localScale = starInitSize;
                thisStar1.transform.localRotation = starInitRotation;
                thisStar1.transform.DORotate(Vector3.zero, 1f);
                thisStar1.transform.DOScale(1f, 1f);
                thisAudioSource.PlayOneShot(StarSound);
                break;
            case 2:
                thisStar1 = Instantiate(StarPrefab, Star1);
                thisStar1.transform.localScale = starInitSize;
                thisStar1.transform.DOScale(1f, 1f);
                thisStar1.transform.localRotation = starInitRotation;
                thisStar1.transform.DORotate(Vector3.zero, 1f);
                thisAudioSource.PlayOneShot(StarSound);
                yield return new WaitForSeconds(0.5f);
                thisStar2 = Instantiate(StarPrefab, Star2);
                thisStar2.transform.localScale = starInitSize;
                thisStar2.transform.DOScale(1f, 1f);
                thisStar2.transform.localRotation = starInitRotation;
                thisStar2.transform.DORotate(Vector3.zero, 1f);
                thisAudioSource.PlayOneShot(StarSound);
                break;
            case 3:
                thisStar1 = Instantiate(StarPrefab, Star1);
                thisStar1.transform.localScale = starInitSize;
                thisStar1.transform.DOScale(1f, 1f);
                thisStar1.transform.localRotation = starInitRotation;
                thisStar1.transform.DORotate(Vector3.zero, 1f);
                thisAudioSource.PlayOneShot(StarSound);
                yield return new WaitForSeconds(0.5f);
                thisStar2 = Instantiate(StarPrefab, Star2);
                thisStar2.transform.localScale = starInitSize;
                thisStar2.transform.DOScale(1f, 1f);
                thisStar2.transform.localRotation = starInitRotation;
                thisStar2.transform.DORotate(Vector3.zero, 1f);
                thisAudioSource.PlayOneShot(StarSound);
                yield return new WaitForSeconds(0.5f);
                thisStar3 = Instantiate(StarPrefab, Star3);
                thisStar3.transform.localScale = starInitSize;
                thisStar3.transform.DOScale(1f, 1f);
                thisStar3.transform.localRotation = starInitRotation;
                thisStar3.transform.DORotate(Vector3.zero, 1f);
                thisAudioSource.PlayOneShot(StarSound);
                break;
            default:
                break;
        }
    }
}
