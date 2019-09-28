using System;
using System.Collections;
using System.Collections.Generic;
using MagicLeap;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class CaloriesProgressBar : MonoBehaviour
{

    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;
    public int TitleFontSize = 10;

    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public int Alert = 20;
    public Color BarAlertColor;

    [Header("Sound Alert")]
    public AudioClip sound;
    public bool repeat = false;
    public double RepeatRate = 1f;

    private Image bar, barBackground;
    private double nextPlay;
    private AudioSource audiosource;
    private Text txtTitle;
    private double barValue;
    public double BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp((float)value, 0, 100);
            barValue = value;
            UpdateValue(barValue);

        }
    }

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        txtTitle = transform.Find("Text").GetComponent<Text>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
        audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        txtTitle.text = Title;
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;
        txtTitle.fontSize = TitleFontSize;

        bar.color = BarColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);


    }

    void UpdateValue(double val)
    {
        bar.fillAmount = (float)val;
        txtTitle.text = Title + " " + val * 100 + "%";

        if (Alert >= val * 100)
        {
            bar.color = BarAlertColor;
        }
        else
        {
            bar.color = BarColor;
        }

    }


    public void UpdateProgressBar(double value)
    {

        UpdateValue(value);
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;
        txtTitle.fontSize = TitleFontSize;

        bar.color = BarColor;
        barBackground.color = BarBackGroundColor;

        barBackground.sprite = BarBackGroundSprite;

    }

}
