using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    //ScoreManager tipinde bir instance olu�turduk.
    public static ScoreManager instance;

    //Score de�erinin tutulmas� i�in
    public int score;

    //Scoreun ekraanda yaz�lmas� i�in
    private Text scoreTxt;


    private void Awake()
    {

        //Tek birkez olmas� i�in.
        makeSingleton();
        scoreTxt = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    private void makeSingleton()
    {
        //E�er instace de�eri nulldan farkl� ise;
        if (instance != null)
        {
            //objeyi destroy yap�lmas�
            Destroy(gameObject);
        }
        else
        {
            //E�er null ise dont destrol yap�lmas�
            instance = this;
            DontDestroyOnLoad(this); //Bu �ekilde scoreManagerda kalan scorelar aktif olarak kullan�labilir.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Ba�lang�� score de�eri 0 olmas�.
        addScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Bir sonraki seviye i�in
        if (scoreTxt == null)
        {
            scoreTxt = GameObject.Find("ScoreText").GetComponent<Text>();
        }
    }

    //Score de�erinin eklenmesi
    public void addScore(int value)
    {
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreTxt.text = score.ToString();
    }

    //Gameover oldu�u zaman score de�erinin s�f�rlanmas�:
    public void ResetScore()
    {
        score = 0;
    }
}
