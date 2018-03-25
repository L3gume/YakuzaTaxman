using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Audio Manager
    public AudioManager AudioManager;

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
        StartCoroutine(ChangeLevel());
    }
    
    IEnumerator ChangeLevel()
    {
        AudioManager.Play("drum");

        float fadeTime = GameObject.Find("Fading").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(2);
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SceneManager.LoadScene("LoreScene");
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}