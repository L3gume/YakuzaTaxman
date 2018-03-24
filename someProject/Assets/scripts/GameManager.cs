using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Eppy;

public class GameManager : MonoBehaviour
{

	// Timer
	public Text TimerText;
	private float _timeLeft = 120.0f;
	
	// Score
	public Text ScoreText;
	private int _score = 0;
	
	// Post it
	private List<Tuple<string, string>> _currentPostItValues;
	
	// Paper

	// Use this for initialization
	void Start ()
	{
		// Initialize text values
		TimerText.text = "Time: " + Mathf.Round(_timeLeft);
		ScoreText.text = "Score: " + _score;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Update timer
		_timeLeft -= Time.deltaTime;
		TimerText.text = "Time: " + Mathf.Round(_timeLeft);

		// Check if time has run out
		if (_timeLeft <= 0.0f)
		{
			// Display game over screen with score
		}
		
		// Check if paper is complete
		if (isPaperComplete())
		{
			// Update score
			_score++;

			// Get new post it
			
			// Create new paper
		}
	}

	// Checks whether the player has completed the needed input for the paper
	bool isPaperComplete()
	{
		return true;
	}
}
