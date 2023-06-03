using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public ELEMENTS elements;
    public GameObject dialogueBox {get{return elements.dialogueBox;}}
    public TextMeshProUGUI speakerName {get{return elements.speakerName;}}
    public TextMeshProUGUI dialogueText {get{return elements.dialogueText;}}

    Coroutine speaking = null;
    public bool isSpeaking { get {return speaking != null;} }
    [HideInInspector] public bool isWaitingForInput = false;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Say(string dialogue, string speaker) {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(dialogue,speaker));
    }

    private void StopSpeaking() {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
            
        }
        speaking = null;
    }

    private IEnumerator Speaking(string dialogue, string speaker) {
        dialogueBox.SetActive(true);
        dialogueText.text = "";
        speakerName.text = speaker;
        isWaitingForInput = false;

        while(dialogueText.text != dialogue) {
            dialogueText.text += dialogue[dialogueText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        isWaitingForInput = true;

        while (isWaitingForInput)
        {
            yield return new WaitForEndOfFrame();
        }

        StopSpeaking();
    }

    [System.Serializable]
    public class ELEMENTS {
        public GameObject dialogueBox;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI dialogueText;
    }
}
