using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.UI;

public class TextInputFieldScript : MonoBehaviour, IPaperComponent
{
    public InputField inputField;

    public Text fieldName;
    
    private RectTransform inputTransform;

    public PostItGenerator.Field field;
    
    public string name;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(inputField != null);
        Debug.Assert(fieldName != null);
    }

    private void Update()
    {
        name = fieldName.text;
        var newName = "NO_NAME";
        
        // Check for the selected type
        switch (field)
        {
            case PostItGenerator.Field.DATE:
                newName = PostItGenerator.Field.DATE.ToString(); break;
            case PostItGenerator.Field.LASTNAME:
                newName = PostItGenerator.Field.LASTNAME.ToString(); break;
            case PostItGenerator.Field.NAME:
                newName = PostItGenerator.Field.NAME.ToString(); break;
            case PostItGenerator.Field.EMAIL:
                newName = PostItGenerator.Field.EMAIL.ToString(); break;
            // TODO: Add more field types here
        }
        // If the selected type diverges from the current one, update
        if (name != newName && newName != "NO_NAME")
        {
            name = newName;
            fieldName.text = name;
        }
    }

    string IPaperComponent.getContent()
    {
        return inputField.text;
    }

    // Scales the input "line" so it fits the intended length.
    public void Rescale(float x)
    {
        inputTransform.sizeDelta = new Vector2(x, inputTransform.sizeDelta.y);
    }
}