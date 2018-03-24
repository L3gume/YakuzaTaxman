using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Eppy;

public class PostItGenerator : MonoBehaviour
{
    public enum Field { NAME, LASTNAME, DATE };

    public int maxDifficulty = 100;

    public string[] names = { "Eric", "Justin", "Evan", "Michael",
        "Bogdan", "Goku", "Krilin", "Bulma", "Piccolo", "Vegeta", "Majin",
        "Goku", "Kuririn", "Videl", "Elie", "Princilia", "Prattipatti", "Bananapearapple",
        "Parapicca", "Spookybooky", "Marianopolis"};

    public string[] lastNames = {"Hi", "Ly", "Lee", "Lar", "Poo", "Nail", "Tremblay", "Vuong",
    "Laflamme", "Vacquier", "Dawson", "Vanier", "Dimutru", "Pairon", "Waldipushi", "Dumbledoreia",
        "Prattipatti", "Lllilli", "Applepearbanana", "McGameJam2018"};
    
    public string[] dates = { "January 1", "February 6", "December 15", "Septempber 8" };
    // deductable, donations, income

    public string getRandomType(int currentTurn){
        return null;
    }

    public List<Tuple<Field, string>> GeneratePostIt(int currentTurn){
        int numberOfTypes = UnityEngine.Random.Range(1,(int)(System.Enum.GetNames(typeof(Field)).Length * difficultyMultiplier(currentTurn)));

        //Generate unique set of Fields
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
            Tuple<Field, string> randomField = newRandomTuple(currentTurn, fields[i]);
        }
        return null;
    }

    public Tuple<Field, string> newRandomTuple(int currentTurn, Field _randomField){

        switch(_randomField){
            //TODO: handle return appropriate string and string difficulty
                
        }

        return null;
    }

    //difficulty caps at maxDifficulty
    public float difficultyMultiplier(int _currentTurn){
        if (_currentTurn < maxDifficulty)
            return (float)(_currentTurn / maxDifficulty);
        else
            return 1.00f;
    }

    //return post it info, list of tuple
    //getPostIt(int currentTurn)

}
