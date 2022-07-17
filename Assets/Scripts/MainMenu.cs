using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform credits;


   
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        credits.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.gameObject.SetActive(false);
    }

    public void ShowJamPage()
    {
        Application.OpenURL("https://itch.io/jam/gmtk-jam-2022");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
