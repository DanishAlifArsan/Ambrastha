using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prolog : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogUI;
    [SerializeField] private GameObject[] dh;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private GameObject[] background;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (GameObject d in dialogUI)
        {
            d.SetActive(false);
        }
        foreach (GameObject b in background)
        {
            b.SetActive(false);
        }
        StartCoroutine(BlackScreen());
    }

    private IEnumerator BlackScreen() {
        dialogUI[3].SetActive(true);
        yield return new WaitUntil(() => !dh[3].activeInHierarchy);
        dialogUI[3].SetActive(false);
        StartCoroutine(Prologue());
    }

    private IEnumerator Prologue() {    
        dialogUI[0].SetActive(true);
        background[0].SetActive(true);
        sfx.Play();
        yield return new WaitUntil(() => !dh[0].activeInHierarchy);
        dialogUI[0].SetActive(false);
        StartCoroutine(ChangeBG());
    }

    private IEnumerator ChangeBG() {
        dialogUI[4].SetActive(true);
        background[0].SetActive(false);
        background[1].SetActive(true);
        yield return new WaitUntil(() => !dh[4].activeInHierarchy);
        dialogUI[4].SetActive(false);
        StartCoroutine(ReturnBG());
    }

    private IEnumerator ReturnBG() {
        dialogUI[5].SetActive(true);
        background[0].SetActive(true);
        background[1].SetActive(false);
        yield return new WaitUntil(() => !dh[5].activeInHierarchy);
        dialogUI[5].SetActive(false);
        StartCoroutine(ShowMask());
    }

    private IEnumerator ShowMask() {
        dialogUI[1].SetActive(true);
        yield return new WaitUntil(() => !dh[1].activeInHierarchy);
        dialogUI[1].SetActive(false);
        StartCoroutine(HideMask());
    }   

    private IEnumerator HideMask() {
        dialogUI[2].SetActive(true);
        yield return new WaitUntil(() => !dh[2].activeInHierarchy);
        dialogUI[2].SetActive(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
