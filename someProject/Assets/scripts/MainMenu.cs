using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public AudioManager audioManager;
	// Use this for initialization
	void Start ()
	{
		audioManager.Play("menu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
