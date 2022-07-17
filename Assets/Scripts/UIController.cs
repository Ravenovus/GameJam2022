using GameJam.DiceManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI victoryText;
    public static UIController instance;
    public Image D4, D6, D8, D20;
    public Image D4C, D6C, D8C, D20C;

    public Image fadeScreen;
    public float fade_speed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;
    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fade_speed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }
        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fade_speed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        if(DiceHealth.instance.currentdie == DiceType.D8)
        {
            D20.gameObject.SetActive(false);
            D20C.gameObject.SetActive(false);
            D8C.gameObject.SetActive(true);
        }
        if (DiceHealth.instance.currentdie == DiceType.D6)
        {
            D8.gameObject.SetActive(false);
            D8C.gameObject.SetActive(false);
            D6C.gameObject.SetActive(true);
        }
        if (DiceHealth.instance.currentdie == DiceType.D4)
        {
            D6.gameObject.SetActive(false);
            D6C.gameObject.SetActive(false);
            D4C.gameObject.SetActive(true);
        }
    }

    public void ResetUI()
    {
        D6.gameObject.SetActive(true);
        D8.gameObject.SetActive(true);
        D20.gameObject.SetActive(true);
        D20C.gameObject.SetActive(true);
        D8C.gameObject.SetActive(false);
        D6C.gameObject.SetActive(false);
        D4C.gameObject.SetActive(false);
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void SetVictoryTextOn()
    {
        victoryText.gameObject.SetActive(true);
    }
}
