using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    public string defaultText = "Score";
    public string errorText = "wottefuck";

    public Color defaultColor;
    public Color errorColor;

    public TextMeshProUGUI textObj;

    //float score = 0;

    LevelController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<LevelController>();

        foreach (Transform child in transform)
        {
            if (textObj == null) textObj = child.GetComponent<TextMeshProUGUI>();
        }

        textObj.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        textObj.text = defaultText + ": " + controller.score;
        //score = controller.score;
    }
}
