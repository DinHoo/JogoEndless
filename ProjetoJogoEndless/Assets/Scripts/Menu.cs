using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject creditsPopUp;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.SetResolution(1080, 1920, true);
    }

    void Update()
    {

    }

    public void loadGameScene()
    {
        print("jogo");
        SceneManager.LoadScene("Jogo");
    }

    public void gameExit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void creditsButton()
    {
        creditsPopUp.SetActive(true);
    }

    public void creditsButton(bool b)
    {
        creditsPopUp.SetActive(b);
    }


}
