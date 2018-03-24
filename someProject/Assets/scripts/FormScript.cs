using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormScript : MonoBehaviour
{
    [SerializeField]
    private RectTransform _transform;

    [SerializeField]
    public RectTransform childPrefab;
    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            RectTransform newField = Instantiate(childPrefab);
            newField.transform.SetParent(_transform, false);
        }
    }
}