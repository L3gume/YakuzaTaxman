using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Eppy;

public class PostItGenerator
{
    public enum Field { Name, LastName, Date, Email }

    // difficulty caps depending on number of turns
    public int maxDifficulty = 100;

    public string[] names = { "Eric", "Justin", "Evan", "Michael",
        "Bogdan", "Goku", "Krilin", "Bulma", "Piccolo", "Vegeta", "Majin",
        "Goku", "Kuririn", "Videl", "Elie", "Princilia", "Prattipatti", "Bananapearapple",
        "Parapicca", "Spookybooky", "Marianopolis"};

    public string[] lastNames = {"Hi", "Ly", "Lee", "Lar", "Poo", "Nail", "Tremblay", "Vuong",
    "Laflamme", "Vacquier", "Dawson", "Vanier", "Dimutru", "Pairon", "Waldipushi", "Dumbledoreia",
        "Prattipatti", "Lllilli", "Applepearbanana", "McGameJam2018"};
    
    public string[] dates = { "January 1", "February 6", "December 15", "Septempber 8" };
    // TODO: deductable, donations, income

    /// <summary>
    /// Generates the post it.
    /// </summary>
    /// <returns>The post it type Tuple.</returns>
    /// <param name="currentTurn">Current turn of player in int.</param>
    /// 
    public List<Tuple<Field, string>> GeneratePostIt(int currentTurn){
        // generate random number of types of field
        int numberOfTypes = UnityEngine.Random.Range(1,(int)(System.Enum.GetNames(typeof(Field)).Length * difficultyMultiplier(currentTurn)));

        // generate unique set of Fields
        var fields = new List<Field>();

        // obtain a list of new fields
        for (int i = 0; i < numberOfTypes; i++){                
            Array values = Enum.GetValues(typeof(Field));
            Field randomField = (Field)values.GetValue(UnityEngine.Random.Range(0, numberOfTypes)); //generate new field
            // checks if field exist already
            while (fields.Contains(randomField)){
                randomField = (Field)values.GetValue(UnityEngine.Random.Range(0, numberOfTypes)); //regenerate new field
            }
            fields.Add(randomField);
        }

        var postIt = new List<Tuple<Field, string>>();
        // create list of Tuples
        for (int i = 0; i < numberOfTypes; i++){
            Tuple<Field, string> randomField = NewRandomTuple(currentTurn, fields[i]);
            postIt.Add(randomField);
        }
        return postIt;
    }

    //Randome tuple generator
    private Tuple<Field, string> NewRandomTuple(int _currentTurn, Field _randomField){
        String answer;
        switch(_randomField)
        {
            //TODO: handle return appropriate string and string difficulty
            case Field.Name:
                {
                    int maxIndex = (int)(names.Length * difficultyMultiplier(_currentTurn));    //set max difficulty
                    int minIndex = (int)(names.Length * minDifficultyMultiplier(_currentTurn));  //set min difficulty
                    answer = names[UnityEngine.Random.Range(minIndex, maxIndex)];
                    return new Tuple<Field, string>(Field.Name, answer);
                }
            case Field.LastName:
                {
                    int maxIndex = (int)(lastNames.Length * difficultyMultiplier(_currentTurn)); //set max difficulty
                    int minIndex = (int)(lastNames.Length * minDifficultyMultiplier(_currentTurn)); //set min difficulty
                    answer = lastNames[UnityEngine.Random.Range(minIndex, maxIndex)];
                    return new Tuple<Field, string>(Field.LastName, answer);
                }
            case Field.Date:
                return new Tuple<Field, string>(Field.Date, dates[UnityEngine.Random.Range(0, dates.Length)]);
        }
        return null;
    }

    //difficulty caps at maxDifficulty
    private float difficultyMultiplier(int _currentTurn){
        if (_currentTurn < maxDifficulty)
            return (float)(_currentTurn / maxDifficulty);
        else
            return 1.00f;
    }
    //mininimum difficulty
    private float minDifficultyMultiplier(int _currentTurn){
        int minRange = (int)(difficultyMultiplier(_currentTurn) * 2.00f); //varies from 0,1 or 2
        return (float) minRange / 3.00f;
    }
}
