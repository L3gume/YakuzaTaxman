using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Eppy;

public class GameManager : MonoBehaviour
{
	// UI Canvas
	public Canvas Canvas;

	// canvas offsets
	public float xOffset;
	public float yOffset;
	// Timer
	public Text TimerText;
	private float _timeLeft = 120.0f;
	
	// Score
	public Text ScoreText;
	private int _score = 0;
	
	// Lives
	public Transform handSprite;
	private int _livesRemaining = 5;
	
	// Post it
	public Transform PostItPrefab;
	public Transform PostItTextPrefab;
	private Transform _currentPostIt;
	private List<Tuple<PostItGenerator.Field, string>> _currentPostItValues;
	private PostItGenerator _postItGenerator;
	private Vector3 postItSpawnPosition;
	
	// Paper
	public Transform PaperPrefab;
	public Transform TextInputPrefab;
	private Transform _currentPaper;
	private Transform _leavingPaper;
	private bool _animatingPapers = false;
	private Vector3 paperTargetPosition;
	private Vector3 paperSpawnPosition;
	private Vector3 paperDespawnPosition;
	
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
		
		
		postItSpawnPosition = new Vector3(-700 + xOffset, -280 + yOffset, 0);
		paperTargetPosition = new Vector3(630 + xOffset, 60 + yOffset, 0);
		paperSpawnPosition = new Vector3(630 + xOffset, 1040 + yOffset, 0);
		paperDespawnPosition = new Vector3(630 + xOffset, -960 + yOffset, 0);
		
		// Generate first post it
		_postItGenerator = new PostItGenerator();
		GeneratePostIt();
		
		// Generate first paper
		GeneratePaper();
		_currentPaper.GetComponent<RectTransform>().position = paperTargetPosition;	
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
		
		// Check if done button is clicked
		if (_doneButtonClicked && !_animatingPapers)
		{
			if (IsPaperCorrect()) {
				// Update score
				_score++;
				ScoreText.text = "Score: " + _score;
			} else { //User submitted a form which was incorrect
				// Decrement lives by one
				_livesRemaining--;

				//Change the input source image of the hand to remove a finger.
				handSprite.GetComponent<SpriteRenderer>().sprite.name = "pixelated_hand_" + _livesRemaining + "lives.png";
			}
			
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
			float step = 8000.0f * Time.deltaTime;
			_currentPaper.GetComponent<RectTransform>().position = Vector3.MoveTowards(_currentPaper.GetComponent<RectTransform>().position, paperTargetPosition, step);
			_leavingPaper.GetComponent<RectTransform>().position = Vector3.MoveTowards(_leavingPaper.GetComponent<RectTransform>().position, paperDespawnPosition, step);
			
			// At target position
			if (_currentPaper.GetComponent<RectTransform>().position == paperTargetPosition)
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
		_currentPostIt.GetComponent<RectTransform>().position = postItSpawnPosition;
		
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
		_currentPaper.GetComponent<RectTransform>().position = paperSpawnPosition;
		
		// Add values from post it to paper
		foreach (var tuple in _currentPostItValues)
		{
			var newField = Instantiate(TextInputPrefab);
			newField.SetParent(_currentPaper, false);
			newField.Find("FieldName").GetComponent<Text>().text = tuple.Item1.ToString();
		}
	}

	// Checks whether the player has correctly completed each input for the paper
	private bool IsPaperCorrect()
	{
		foreach (Tuple<PostItGenerator.Field, string> tuple in _currentPostItValues)
		{
			bool fieldCorrect = false;
			foreach (TextInputFieldScript textInputField in _currentPaper.GetComponentsInChildren<TextInputFieldScript>())
			{
				if (tuple.Item1.Equals(textInputField.field) && tuple.Item2.Equals(textInputField.name))
				{
					fieldCorrect = true;
					break;
				}
			}
			if (!fieldCorrect)
			{
				return false;
			}
		}
		return true;
	}

	private void DoneButtonOnClick()
	{
		_doneButtonClicked = true;
		DoneButton.enabled = false;
	}
}
