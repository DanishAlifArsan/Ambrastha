using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : DialogueBaseClass
{
    [SerializeField] private string input;
    [SerializeField] private float delay;
    [SerializeField] private string speakerName;
    [SerializeField] private TextMeshProUGUI speakerBox;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image imageHolder;
    private TextMeshProUGUI textHolder;
    // Start is called before the first frame update
    private void Awake()
    {
        textHolder = GetComponent<TextMeshProUGUI>();
        textHolder.text = "";
        imageHolder.preserveAspect = true;
    }

    private void Start()
    {
        speakerBox.text = speakerName;
        imageHolder.color = new Color(1, 1, 1, 1);
        imageHolder.sprite = characterSprite;
        StartCoroutine(WriteText(input,textHolder,delay));
    }

    // Update is called once per frame
    void Update()
    {
      if (isFinished)
      {
         imageHolder.color = new Color(1, 1, 1, 0);
      }
    }
}
