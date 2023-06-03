using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    DialogueSystem dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = DialogueSystem.instance;
    }

    public string[] s = new string[] {"Hello World:Danish", "testing"}; 

    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log(dialogueSystem.isSpeaking + " " + dialogueSystem.isWaitingForInput);
            if (!dialogueSystem.isSpeaking || dialogueSystem.isWaitingForInput)
            {
                if (index >= s.Length)
                {
                    return;
                }
                Say(s[index]);
                index++;
            }
        }
    }

    private void Say(string s) {
        string[] parts = s.Split(":");
        string speech = parts[0];
        string speaker = (parts.Length >= 2)? parts[1] : "";

        dialogueSystem.Say(speech, speaker);
    }
}
