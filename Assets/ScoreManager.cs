using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    //ScoreManager tipinde bir instance oluþturduk.
    public static ScoreManager instance;

    //Score deðerinin tutulmasý için
    public int score;

    //Scoreun ekraanda yazýlmasý için
    private Text scoreTxt;


    private void Awake()
    {

        //Tek birkez olmasý için.
        makeSingleton();
        scoreTxt = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    private void makeSingleton()
    {
        //Eðer instace deðeri nulldan farklý ise;
        if (instance != null)
        {
            //objeyi destroy yapýlmasý
            Destroy(gameObject);
        }
        else
        {
            //Eðer null ise dont destrol yapýlmasý
            instance = this;
            DontDestroyOnLoad(this); //Bu þekilde scoreManagerda kalan scorelar aktif olarak kullanýlabilir.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Baþlangýç score deðeri 0 olmasý.
        addScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Bir sonraki seviye için
        if (scoreTxt == null)
        {
            scoreTxt = GameObject.Find("ScoreText").GetComponent<Text>();
        }
    }

    //Score deðerinin eklenmesi
    public void addScore(int value)
    {
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreTxt.text = score.ToString();
    }

    //Gameover olduðu zaman score deðerinin sýfýrlanmasý:
    public void ResetScore()
    {
        score = 0;
    }
}
