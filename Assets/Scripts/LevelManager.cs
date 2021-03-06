using GameJam.DiceManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] float waitToRespawn = 1.5f;
    public static LevelManager instance;
    public int LevelToLoad;



    private void Awake()
    {
        instance = this;
    }

    public void RespawnPlayer()
    {
        //AudioManager.audioManager.PlaySFX(15); add after recording breaking
        StartCoroutine(RespawnCoroutine());

    }

    private IEnumerator RespawnCoroutine()
    {
        
        DiceManager.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fade_speed));
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fade_speed) + .2f);
        UIController.instance.FadeFromBlack();
        UIController.instance.UpdateHealthDisplay();
        DiceManager.instance.gameObject.SetActive(true);

    }

    public void EndLevel()
    {
        Debug.Log("outsidetheco");
        StartCoroutine(EndLevelCoRoutine());
    }

    public IEnumerator EndLevelCoRoutine()
    {

        
        //DiceManager.instance.DisableInput();

        UIController.instance.SetVictoryTextOn();
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(2f);       
        
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex+1)%3);
    }
}
