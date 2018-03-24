using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color col = new Color();
        ColorUtility.TryParseHtmlString("#D02222FF", out col);
        text.color = col;
    }

    public void onClick()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("MainScene");
            SceneManager.UnloadSceneAsync("Menu");
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("Menu");
            SceneManager.UnloadSceneAsync("MainScene");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}