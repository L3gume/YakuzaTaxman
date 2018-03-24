using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : MonoBehaviour {

    public string[] names = { "Eric", "Justin", "Evan", "Michael", 
        "Bogdan", "Goku", "Krilin", "Bulma", "Piccolo", "Vegeta", "Majin",
    "Freeza", "Cell", "Goku", "Kuririn", "Videl", "Elie"};

    public string GetName(){
        return names[(int) Random.Range(0, names.Length-1)];
    }

    //return post it info, list of tuple
}
