using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Eppy;

public class GameManager : MonoBehaviour
{
    // UI Canvas
    public Canvas Canvas;

    // Audio Manager
    public AudioManager AudioManager;
    
    // Camera Shake
    public CameraShake CameraShake;
    public float Amount = 0.0f;
    public float Duration = 0.0f;

    // Canvas offsets
    public float XOffset;
    public float YOffset;

    // Start countdown
    public Text StartCountDownText;
    private float _countdown = 4.0f;

    // Timer
    public Text TimerText;
    private float _timeLeft = 60.0f;

    // Score
    public Text ScoreText;
    private int _score = 0;

    // Lives
    public Transform HandSprite;
    private int _livesRemaining = 5;

    // Post it
    public Transform PostItPrefab;
    public Transform PostItTextPrefab;
    private Transform _currentPostIt;
    private List<Tuple<PostItGenerator.Field, string>> _currentPostItValues;
    private PostItGenerator _postItGenerator;
    private Vector3 _postItSpawnPosition;

    // Paper
    public Transform PaperPrefab;
    public Transform TextInputPrefab;
    public Text JapaneseFontTextPrefab;
    private Transform _currentPaper;
    private Transform _leavingPaper;
    private bool _animatingPapers = false;
    private Vector3 _paperTargetPosition;
    private Vector3 _paperSpawnPosition;
    private Vector3 _paperDespawnPosition;

    // Done button
    public Button DoneButton;
    private bool _doneButtonClicked = false;
    private bool _gameOver = false;
    private bool _gameStarted = false;

    // Blood splash
    public GameObject Blood;

    // Music
    private string _currentSong;

    public GameObject GameOverMenu;

    // Use this for initialization
    private void Start()
    {
        // Initialize text values
        TimerText.text = "Time: " + Mathf.Round(_timeLeft);
        ScoreText.text = "Score: " + _score;

        // Set done button listener
        Button btn = DoneButton.GetComponent<Button>();
        btn.onClick.AddListener(DoneButtonOnClick);

        AudioManager.Play("easy");
        _currentSong = "easy";
        _postItSpawnPosition = new Vector3(-700 + XOffset, -280 + YOffset, 0);
        _paperTargetPosition = new Vector3(630 + XOffset, 60 + YOffset, 0);
        _paperSpawnPosition = new Vector3(630 + XOffset, 1040 + YOffset, 0);
        _paperDespawnPosition = new Vector3(630 + XOffset, -960 + YOffset, 0);

        // Generate first post it
        _postItGenerator = new PostItGenerator();

        // Generate first paper
        StartCountDownText.text = Mathf.Round(_countdown - 1).ToString(CultureInfo.InvariantCulture);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameStarted) // Game has started
        {
            if (!_gameOver)
            {
                // Update timer
                _timeLeft -= Time.deltaTime;
                TimerText.text = "Time: " + Mathf.Round(_timeLeft);

                // Check if time has run out
                if (_timeLeft <= 0.0f)
                {
                    // Display game over screen with score
                    AudioManager.Stop(_currentSong);
                    AudioManager.Stop("slice");
                    AudioManager.Play("yooo");
                    _gameOver = true;
                    return;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    _doneButtonClicked = true;
                }

                // Check if done button is clicked
                if (_doneButtonClicked && !_animatingPapers)
                {
                    if (IsPaperCorrect())
                    {
                        // Update score
                        _score++;
                        if (_score == 5)
                        {
                            AudioManager.Stop("easy");
                            AudioManager.Play("medium");
                            _currentSong = "medium";
                        }
                        else if (_score == 10)
                        {
                            AudioManager.Stop("medium");
                            AudioManager.Play("hard");
                            _currentSong = "hard";
                        }

                        ScoreText.text = "Score: " + _score;

                        _timeLeft += 0.0002f * Mathf.Pow(_score, 3) - 0.02f * Mathf.Pow(_score, 2) + 0.7f * _score + 3;
                    }
                    else
                    {
                        //User submitted a form which was incorrect
                        // Decrement lives by one
                        _livesRemaining--;
                        
                        CameraShake.ShakeCamera(Amount, Duration);
                        
                        switch (_livesRemaining)
                        {
                            case 4:
                                Instantiate(Blood, new Vector3(-4.61f, -4.63f, -100.0f), Quaternion.identity);
                                AudioManager.Play("slice");
                                break;
                            case 3:
                                Instantiate(Blood, new Vector3(-3.91f, -3.49f, -100.0f), Quaternion.identity);
                                AudioManager.Play("slice");
                                break;
                            case 2:
                                Instantiate(Blood, new Vector3(-3.15f, -2.98f, -100.0f), Quaternion.identity);
                                AudioManager.Play("slice");
                                break;
                            case 1:
                                Instantiate(Blood, new Vector3(-2.08f, -2.98f, -100.0f), Quaternion.identity);
                                AudioManager.Play("slice");
                                break;
                            case 0:
                                Instantiate(Blood, new Vector3(-0.44f, -4.72f, -100.0f), Quaternion.identity);
                                AudioManager.Stop(_currentSong);
                                AudioManager.Stop("slice");
                                AudioManager.Play("yooo");
                                _gameOver = true;
                                break;
                        }


                        //Change the input source image of the hand to remove a finger.
                        HandSprite.GetComponent<SpriteRenderer>().sprite =
                            Resources.Load<Sprite>("images/" + "pixelated_hand_" + _livesRemaining + "lives");
                    }

                    // Remove old post it
                    Destroy(_currentPostIt.gameObject);
                    if (!_gameOver)
                    {
                        // Get new post it
                        GeneratePostIt();

                        // Create new paper
                        GeneratePaper();

                        _animatingPapers = true;
                    }
                }

                // If animating incoming and leaving papers
                if (_animatingPapers)
                {
                    // Move both papers down until incoming is in correct position, then delete old paper
                    float step = 8000.0f * Time.deltaTime;
                    _currentPaper.GetComponent<RectTransform>().position =
                        Vector3.MoveTowards(_currentPaper.GetComponent<RectTransform>().position,
                            _paperTargetPosition, step);
                    _leavingPaper.GetComponent<RectTransform>().position =
                        Vector3.MoveTowards(_leavingPaper.GetComponent<RectTransform>().position,
                            _paperDespawnPosition, step);

                    // At target position
                    if (_currentPaper.GetComponent<RectTransform>().position == _paperTargetPosition)
                    {
                        _animatingPapers = false;

                        Destroy(_leavingPaper.gameObject);

                        _doneButtonClicked = false;
                        DoneButton.enabled = true;
                    }
                }
            }
            else
            {
                // Game over
                GameOverMenu.SetActive(true);
            }
        }
        else // Doing countdown timer
        {
            _countdown -= Time.deltaTime;

            if (_countdown > 1.5f)
            {
                StartCountDownText.text = Mathf.Round(_countdown - 1).ToString(CultureInfo.InvariantCulture);
            }
            else if (_countdown > 0.0f)
            {
                StartCountDownText.text = "Start";
            }
            else
            {
                _gameStarted = true;
                StartCountDownText.gameObject.SetActive(false);

                // Generate first post it
                _postItGenerator = new PostItGenerator();
                GeneratePostIt();

                // Generate first paper
                GeneratePaper();
                _currentPaper.GetComponent<RectTransform>().position = _paperTargetPosition;
            }
        }
    }

    // Generates new post it
    private void GeneratePostIt()
    {
        _currentPostItValues = _postItGenerator.GeneratePostIt(_score); // Get new values
        _currentPostIt = Instantiate(PostItPrefab); // Create new post it prefab
        _currentPostIt.SetParent(Canvas.transform);
        _currentPostIt.GetComponent<RectTransform>().position = _postItSpawnPosition;

        // Add values to post it
        foreach (var tuple in _currentPostItValues)
        {
            var newField = Instantiate(PostItTextPrefab);
            newField.SetParent(_currentPostIt, false);
            newField.GetComponent<Text>().text = tuple.Item1.ToString() + ": " + tuple.Item2;
        }
    }

    // Generates a new paper
    private void GeneratePaper()
    {
        var numberOfJapaneseFields = UnityEngine.Random.Range(1, 5);
        
        _leavingPaper = _currentPaper; // Set the new leaving paper

        _currentPaper = Instantiate(PaperPrefab);
        _currentPaper.SetParent(Canvas.transform);

        // Spawn paper above the view (to move it in later)
        _currentPaper.GetComponent<RectTransform>().position = _paperSpawnPosition;

        if (UnityEngine.Random.Range(0, 2) == 0) //Random chance of having japanese text before post its
        {
            numberOfJapaneseFields--;
            
            var japanText = Instantiate(JapaneseFontTextPrefab);
            japanText.transform.SetParent(_currentPaper, false);
            japanText.GetComponent<Text>().text = RandomStringOfLength(UnityEngine.Random.Range(10, 30));
        }

        //Shuffle the postit after level 15
        ListTuple<Field, string>> currentPostItValues = _currentPostItValues;
        if (_score > 14)
        {
             currentPostItValues = ShuffleList<Tuple<Field, string>>(currentPostItValues);
        }
        // Add values from post it to paper
        foreach (var tuple in currentPostItValues)
        {
            if (UnityEngine.Random.Range(0, 5) < numberOfJapaneseFields && numberOfJapaneseFields > 0)
            {
                numberOfJapaneseFields--;
            
                var japanText = Instantiate(JapaneseFontTextPrefab);
                japanText.transform.SetParent(_currentPaper, false);
                japanText.GetComponent<Text>().text = RandomStringOfLength(UnityEngine.Random.Range(10, 30));
            }
            
            var newField = Instantiate(TextInputPrefab);
            newField.SetParent(_currentPaper, false);
            newField.gameObject.GetComponent<TextInputFieldScript>().SetFieldName(tuple.Item1);
        }

        while (numberOfJapaneseFields > 0)
        {
            numberOfJapaneseFields--;
            
            var japanText = Instantiate(JapaneseFontTextPrefab);
            japanText.transform.SetParent(_currentPaper, false);
            japanText.GetComponent<Text>().text = RandomStringOfLength(UnityEngine.Random.Range(10, 30));
        }
    }
    
    //Shuffles the list
    private List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
            randomList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }

    // Checks whether the player has correctly completed each input for the paper
    private bool IsPaperCorrect()
    {
        foreach (Tuple<PostItGenerator.Field, string> tuple in _currentPostItValues)
        {
            bool fieldCorrect = false;
            foreach (TextInputFieldScript textInputField in
                _currentPaper.GetComponentsInChildren<TextInputFieldScript>())
            {
                if (tuple.Item1.Equals(textInputField.field) && tuple.Item2.Equals(textInputField.inputField.text))
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

    private string RandomStringOfLength(int n)
    {
        var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        char[] str = new char[n];
        
        for (int i = 0; i < n; i++)
        {
            str[i] = allowedChars[UnityEngine.Random.Range(0, allowedChars.Length - 1)];
        }

        return new string(str);
    }
}
