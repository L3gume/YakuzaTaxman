using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Eppy;

public class PostItGenerator : MonoBehaviour
{
    public enum Field { NAME, LASTNAME, DATE };

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
        int numberOfTypes = Random.Range(1,System.Enum.GetNames(typeof(Field)).Length);

        return null;
    }

    //return post it info, list of tuple
    //getPostIt(int currentTurn)

}
