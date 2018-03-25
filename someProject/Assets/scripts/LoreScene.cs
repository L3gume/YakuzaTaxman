using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoreScene : MonoBehaviour {
	
	// Audio Manager
	public AudioManager AudioManager;

	// Use this for initialization
	void Start () {
		AudioManager.Play("lore");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			OnClick();
		}
	}

	public void OnClick()
	{
		StartCoroutine(ChangeLevel());
	}

	IEnumerator ChangeLevel()
	{
		AudioManager.Stop("lore");
		AudioManager.Play("drum");

		float fadeTime = GameObject.Find("Fading").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(2);

		SceneManager.LoadScene("MainScene");
	}
}
