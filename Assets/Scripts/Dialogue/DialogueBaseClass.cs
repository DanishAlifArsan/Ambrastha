using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBaseClass : MonoBehaviour
{
    public bool isFinished { get; private set; }

    protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, float delay) {
        for (int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Space));
        isFinished = true;
    }
}
