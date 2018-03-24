using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Eppy;

public class PostItGenerator
{

    // difficulty caps depending on number of turns
    public int maxDifficulty = 100;

    // fields
    public enum Field
    {
        Name,
        LastName,
        Date,
        Email,
        Income,
        TaxDeductable
    }

    public string[] names = { "Eric", "Justin", "Evan", "Michael",
        "Bogdan", "Goku", "Krilin", "Bulma", "Piccolo", "Vegeta", "Majin",
        "Goku", "Kuririn", "Videl", "Elie", "Princilia", "Prattipatti", "Bananapearapple",
        "Parapicca", "Spookybooky", "Marianopolis"};

    public string[] lastNames = {"Hi", "Ly", "Lee", "Lar", "Poo", "Nail", "Tremblay", "Vuong",
    "Laflamme", "Vacquier", "Dawson", "Vanier", "Dimutru", "Pairon", "Waldipushi", "Dumbledoreia",
        "Prattipatti", "Lllilli", "Applepearbanana", "McGameJam2018"};

    public string[] dates = { "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December" };

    public string[] emails = {"evan@gmail.com", "justin@gmail.com", "michael@gmail.com", "eric@gmail.com", "bogdan@gmail.com", "leo@gmail.com",
    "mcgamejam@gmail.com", "elieharfouche@gmail.com", "bananapearapple@gmail.com", "sushimaster@gmail.com", "whatsmyname@gmail.com", "imhungryfeedme@gmail.com",
        "needcaffeine@gmail.com", "eric@gmaii.com", "helloonetwofour@gmail.com", "impossibrutoolong@gmaii.com"};

    public int maxIncome = 1000000;

    public int maxTaxDeductable = 1000000;

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
                {
                    answer = dates[UnityEngine.Random.Range(0, dates.Length)]; //generate random date
                    answer = answer + " " + UnityEngine.Random.Range(1, 31);
                    return new Tuple<Field, string>(Field.Date, answer);
                }
            case Field.Email:
                {
                    int maxIndex = (int)(emails.Length * difficultyMultiplier(_currentTurn));    //set max difficulty
                    int minIndex = (int)(emails.Length * minDifficultyMultiplier(_currentTurn));  //set min difficulty
                    answer = emails[UnityEngine.Random.Range(minIndex, maxIndex)];
                    return new Tuple<Field, string>(Field.Email, answer);
                }
            case Field.Income:
                {
                    answer = "" + UnityEngine.Random.Range(0, maxIncome * difficultyMultiplier(_currentTurn));
                    return new Tuple<Field, string>(Field.Income, answer);
                }
            case Field.TaxDeductable:
                {
                    answer = "" + UnityEngine.Random.Range(0, maxTaxDeductable * difficultyMultiplier(_currentTurn));
                    return new Tuple<Field, string>(Field.TaxDeductable, answer);
                }
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
    //mininimum difficulty 0, 1, or 2 out of 3
    private float minDifficultyMultiplier(int _currentTurn){
        int minRange = (int)(difficultyMultiplier(_currentTurn) * 2.00f); //varies from 0,1 or 2
        return (float) minRange / 3.00f;
    }
}
