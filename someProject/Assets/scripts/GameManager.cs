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
	public Image handImage;
	private int _livesRemaining = 5;
	
	// Post it
	public Transform PostItPrefab;
	public Transform PostItTextPrefab;
	private Transform _currentPostIt;
	private List<Tuple<PostItGenerator.Field, string>> _currentPostItValues;
	private PostItGenerator _postItGenerator;
	private Vector2 spawnPostItOffsetMin = new Vector2(49.8324f, 32.1381f);
	private Vector2 spawnPostItOffsetMax = new Vector2(928.967f, 365.361f);
	
	// Paper
	public Transform PaperPrefab;
	public Transform TextInputPrefab;
	private Transform _currentPaper;
	private Transform _leavingPaper;
	private bool _animatingPapers = false;
	private Vector2 targetPaperOffsetMin = new Vector2(796.115f, 55.79f);
	private Vector2 targetPaperOffsetMax = new Vector2(33.8950f, 55.79f);
	private Vector2 spawnPaperOffsetMax = new Vector2(33.8950f, 0.79f);
	private Vector2 despawnPaperOffsetMax = new Vector2(33.8950f, 110.79f);
	
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
		
		// Generate first post it
		_postItGenerator = new PostItGenerator();
		GeneratePostIt();
		
		// Generate first paper
		GeneratePaper();
		_currentPaper.transform.position = new Vector3(796, 56, 0);
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
		if (IsPaperComplete() && !_animatingPapers)
		{
			// Update score
			_score++;

			// Remove old post it
			Destroy(_currentPostIt.gameObject);
			
			// Get new post it
			GeneratePostIt();
			
			// Create new paper
			GeneratePaper();

			_animatingPapers = true;
		}
		
		// If animating incoming and leaving papers
		if (_animatingPapers)
		{
			// Move both papers down until incoming is in correct position, then delete old paper
			float step = 30.0f * Time.deltaTime;
			_currentPaper.GetComponent<RectTransform>().offsetMax = Vector2.MoveTowards(_currentPaper.GetComponent<RectTransform>().offsetMax, targetPaperOffsetMax, step);
			_leavingPaper.GetComponent<RectTransform>().offsetMax = Vector2.MoveTowards(_leavingPaper.GetComponent<RectTransform>().offsetMax, despawnPaperOffsetMax, step);
			
			// At target position
			if (_currentPaper.GetComponent<RectTransform>().offsetMax == targetPaperOffsetMax)
			{
				_animatingPapers = false;
				
				Destroy(_leavingPaper.gameObject);

				_doneButtonClicked = false;
				DoneButton.enabled = true;
			}
		}
	}

	// Generates new post it
	private void GeneratePostIt()
	{
		_currentPostItValues = _postItGenerator.GeneratePostIt(_score); // Get new values
		_currentPostIt = Instantiate(PostItPrefab); // Create new post it prefab
		_currentPostIt.SetParent(Canvas.transform);
		
		// Add values to post it
		foreach (var tuple in _currentPostItValues)
		{
			var newField = Instantiate(PostItTextPrefab);
			newField.SetParent(_currentPostIt, false);
			newField.GetComponent<Text>().text = tuple.Item1.ToString() + ": " + tuple.Item2;
		}
	}

	// Generates a new paper
	// TODO: Make tings better
	private void GeneratePaper()
	{
		_leavingPaper = _currentPaper; // Set the new leaving paper
		
		_currentPaper = Instantiate(PaperPrefab);
		_currentPaper.SetParent(Canvas.transform);
		
		// Spawn paper above the view (to move it in later)
		_currentPaper.GetComponent<RectTransform>().offsetMin = targetPaperOffsetMin;
		_currentPaper.GetComponent<RectTransform>().offsetMax = spawnPaperOffsetMax;
		
		// Add values from post it to paper
		foreach (var tuple in _currentPostItValues)
		{
			var newField = Instantiate(TextInputPrefab);
			newField.SetParent(_currentPaper, false);
			newField.Find("FieldName").GetComponent<Text>().text = tuple.Item1.ToString();
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
