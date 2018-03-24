using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputFieldScript : MonoBehaviour, IPaperComponent
{
    public InputField inputField;

    public Text fieldName;

    public float mfX;

    public float mfY;

    private RectTransform inputTransform;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(inputField != null);
        Debug.Assert(fieldName != null);

        inputTransform = inputField.GetComponent<RectTransform>();
        Debug.Assert(inputTransform != null);
        mfX = inputTransform.localPosition.x - inputTransform.localScale.x / 2.0f;
        mfY = inputTransform.localPosition.y - inputTransform.localScale.y / 2.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            rescale(inputTransform.sizeDelta.x + 30.0f, inputTransform.sizeDelta.y);
        }
    }

    string IPaperComponent.getContent()
    {
        return inputField.text;
    }

    // Scales the input "line" so it fits the intended length.
    void rescale(float x, float y)
    {
//        Vector2 vScale = inputTransform.localScale;
//        inputTransform.localScale = new Vector2(x, y);
//        inputTransform.localPosition = new Vector3(mfX + transform.localScale.x / 2.0f, mfY + transform.localScale.y / 2.0f, 0.0f);
//        mfX = inputTransform.localPosition.x - inputTransform.localScale.x / 2.0f;
//        mfY = inputTransform.localPosition.y - inputTransform.localScale.y / 2.0f;
        inputTransform.sizeDelta = new Vector2(x, y);
    }
}