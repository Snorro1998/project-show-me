using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InkText : MonoBehaviour
{
    public LevelController controller;

    public string defaultText = "Ink";
    public string errorText = "Insufficient ink!";

    public Color defaultColor;
    public Color errorColor;

    public bool showError = false;

    public TextMeshProUGUI textObj;
    public Slider inkBar;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (textObj == null) textObj = child.GetComponent<TextMeshProUGUI>();
            if (inkBar == null) inkBar = child.GetComponent<Slider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        inkBar.value = controller.inkLevel;

        if (controller.inkLevel == 0)
        {
            textObj.text = errorText;
            textObj.color = errorColor;
        }

        else
        {
            textObj.text = defaultText;
            textObj.color = defaultColor;
        }
    }
}
