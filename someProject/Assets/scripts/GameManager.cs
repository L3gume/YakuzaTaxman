using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Eppy;

public class GameManager : MonoBehaviour
{
	// UI Canvas
	public Canvas Canvas;

	// Timer
	public Text TimerText;
	private float _timeLeft = 120.0f;
	
	// Score
	public Text ScoreText;
	private int _score = 0;
	
	// Lives

	private int _livesRemaining = 5;
	
	// Post it
	public Transform PostItPrefab;
	private Transform _currentPostIt;
	private List<Tuple<string, string>> _currentPostItValues;
	
	// Paper
	public Transform PaperPrefab;
	public Transform TextInputPrefab;
	private Transform _currentPaper;
	private Transform _leavingPaper;
	private bool _animatingPapers = false;
	
	
	// Done button
	public Button DoneButton;
	private bool _doneButtonClicked = false;

	// Use this for initialization
	private void Start ()
	{
		// Initialize text values
		TimerText.text = "Time: " + Mathf.Round(_timeLeft);
		ScoreText.text = "Score: " + _score;
		
		// Set done button listener
		Button btn = DoneButton.GetComponent<Button>();
		btn.onClick.AddListener(DoneButtonOnClick);
	}
	
	// Update is called once per frame
	private void Update ()
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
		if (IsPaperComplete())
		{
			// Update score
			_score++;

			// Get new post it
			
			// Create new paper


			_animatingPapers = true;
		}
		
		// If animating incoming and leaving papers
		if (_animatingPapers)
		{
			// Move both papers down until incoming is in correct position, then delete old paper
		}
	}

	// Checks whether the player has completed the needed input for the paper
	private bool IsPaperComplete()
	{
		if (_doneButtonClicked)
		{
			return true;
		}
		
		return false;
	}

	private void DoneButtonOnClick()
	{
		_doneButtonClicked = true;
		DoneButton.enabled = false;
	}
}
