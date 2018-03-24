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

    public string[] names = { "Ai", "Aoi", "Ko", "Jun", "Kin", 
                            "Iku", "Ine", "Kin", "Jin", "Rai", 
                            "Asa", "Iwa", "Cho", "Aki", "Uta", 
                            "Ken", "Mon", "Rei", "Ran", "Ima", 
                            "Kei", "Ito", "Jiro", "Hama", "Koma", 
                            "Koko", "Mai", "Yuki", "Aiko", "Raku", 
                            "Yoko", "Nami", "Mura", "Etsu", "Akio", 
                            "Moto", "Tori", "Hide", "Ishi", "Kiyo", 
                            "Haya", "Isas", "Tomi", "Taro", "Tane", 
                            "Tami", "Tama", "Kiwa", "Suzu", "Mine", 
                            "Kimi", "Kazu", "Haru", "Kinu", "Masa", 
                            "Suki", "Kuni", "Anzu", "Kane", "Kumi", 
                            "Koto", "Sugi", "Kane", "Riku", "Kome", 
                            "Hatsu", "Hisa", "Chizu", "Yukio", "Chiyo", 
                            "Matsu", "Akemi", "Chika", "Yoshi", "Yoshe", 
                            "Masao", "Isamu", "Machi", "Yemon", "Yasuo", 
                            "Imako", "Azami", "Umeko", "Leiko", "Junko", 
                            "Kyoko", "Kishi", "Shizu", "Hisae", "Ayame", 
                            "Akira", "Shiro", "Hoshi", "Shina", "Kikue", 
                            "Kichi", "Setsu", "Seiko", "Sakae", "Akako", 
                            "Sachi", "Ayako", "Kazuo", "Harue", "Nishi", 
                            "Natsu", "Etsuko", "Katsu", "Naoko", "Hideyo", 
                            "Haruko", "Morie", "Mitsu", "Kaoru", "Chiyoko", 
                            "Hanako", "Hisoka", "Misao", "Mikie", "Mieko", 
                            "Yukiko", "Tsuhgi", "Tamiko", "Kagami", "Hisano", 
                            "Tamako", "Takeko", "Suzuki", "Shizue", "Shizko", 
                            "Sakura", "Saburo", "Namiko", "Nagisa", "Miyuki", 
                            "Mineko", "Hiroko", "Midori", "Masato", "Chikako", 
                            "Masago", "Makoto", "Kukiko", "Hiroshi", "Hideaki", 
                            "Komako", "Kohana", "Kiwako", "Kimiyo", "Kimiko", 
                            "Kikuko", "Kazuko", "Kameyo", "Kameko", "Takeshi", 
                            "Akasuki", "Shizuyo", "Setsuko", "Mitsuko", "Akihiko", 
                            "Michiko", "Matsuko", "Kiyoshi", "Kiyoshi", "Hoshiko", 
                            "Yasahiro", "Hirohito", "Murasaki", "Masahiro", "Hiromasa", 
                            "Toshihiro" };

    public string[] lastNames = { "Abe", "Arai", "Ito", "Ota", "Endo", 
                                "Goto", "Ohno", "Hara", "Imai", "Ando", 
                                "Aoki", "Ikeda", "Kudo", "Kato", "Kubo", 
                                "Chiba", "Ishii", "Fujii", "Miura", "Mori", 
                                "Ono", "Fukuda", "Maeda", "Harada", "Sano", 
                                "Fujita", "Inoue", "Kondo", "Kimura", "Kaneko", 
                                "Okada", "Ueno", "Matsuo", "Ishida", "Ogawa", 
                                "Hirano", "Matsui", "Nomura", "Kojima", "Onishi", 
                                "Sato", "Nakano", "Otsuka", "Saito", "Fujimoto", 
                                "Saito", "Ueda", "Wada", "Masuda", "Sakai", 
                                "Morita", "Murata", "Takeda", "Kikuchi", "Sasaki", 
                                "Takada", "Hayashi", "Shimizu", "Noguchi", "Sakurai", 
                                "Iwasaki", "Ishikawa", "Shibata", "Okamoto", "Takagi", 
                                "Maruyama", "Matsuda", "Uchida", "Fujiwara", "Miyamoto", 
                                "Miyazaki", "Hasegawa", "Tanaka", "Sugimoto", "Sugawara", 
                                "Nakayama", "Hashimoto", "Nakagawa", "Nakamura", "Sugiyama", 
                                "Tamura", "Murakami", "Sakamoto", "Kinoshita", "Yamada", 
                                "Nakajima", "Matsumoto", "Suzuki", "Takeuchi", "Taniguchi", 
                                "Yokoyama", "Yamamoto", "Kobayashi", "Nishimura", "Yamazaki", 
                                "Watanabe", "Yoshida", "Yamashita", "Yamaguchi", "Takahashi" };

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
        int numberOfTypes = UnityEngine.Random.Range(1, ((int)(System.Enum.GetNames(typeof(Field)).Length * difficultyMultiplier(currentTurn) + 1)) % System.Enum.GetNames(typeof(Field)).Length);

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
                    answer = names[(UnityEngine.Random.Range(minIndex, maxIndex) + 1) % names.Length];
                    return new Tuple<Field, string>(Field.Name, answer);
                }
            case Field.LastName:
                {
                    int maxIndex = (int)(lastNames.Length * difficultyMultiplier(_currentTurn)); //set max difficulty
                    int minIndex = (int)(lastNames.Length * minDifficultyMultiplier(_currentTurn)); //set min difficulty
                    answer = lastNames[(UnityEngine.Random.Range(minIndex, maxIndex) + 1) % lastNames.Length];
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
                    answer = generateEmail(names[UnityEngine.Random.Range(minIndex, maxIndex)], lastNames[UnityEngine.Random.Range(minIndex, maxIndex)]);
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
    
    //Gererate an email from first and last name
    private String generateEmail(String firstName, String lastName)
    {
        //string to be constructed
        String outputEmail = String.Empty;

        //variable used to introduce randomness
        int arbitror = UnityEngine.Random.Range(0, 2);

        //generate a random email scheme including "firstname.lastname" or "lastname<fisrtNameFirstLetter>"
        switch (arbitror)
        {
            case 0:
                outputEmail = firstName + "." + lastName;
                break;
            case 1:
                outputEmail = lastName + firstName.ToCharArray()[0];
                break;
            default:
                break;
        }

        arbitror = UnityEngine.Random.Range(0, 4);
        
        //generate randomness for having a number or not after the email
        if (arbitror == 0)
        {
            outputEmail += UnityEngine.Random.Range(0, 100);
        }

        outputEmail += "@";
        
        arbitror = UnityEngine.Random.Range(0, 4);

        //generate randomness for the domain of the email
        switch (arbitror)
        {
            case 0:
                outputEmail += "gmail.com";
                break;
            case 1:
                outputEmail += "mail.mcgill.ca";
                break;
            case 2:
                outputEmail += "outlook.com";
                break;
            case 3:
                outputEmail += "hotmail.com";
                break;
            default:
                break;
        }

        return outputEmail;
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
