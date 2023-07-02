using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prolog : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogUI;
    [SerializeField] private GameObject maskImage;
    [SerializeField] private GameObject[] dh;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (GameObject d in dialogUI)
        {
            d.SetActive(false);
        }
        maskImage.SetActive(false);
        StartCoroutine(Prologue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Prologue() {    
        dialogUI[0].SetActive(true);
        yield return new WaitUntil(() => !dh[0].activeInHierarchy);
        dialogUI[0].SetActive(false);
        StartCoroutine(ShowMask());
    }

    private IEnumerator ShowMask() {
        dialogUI[1].SetActive(true);
        maskImage.SetActive(true);
        yield return new WaitUntil(() => !dh[1].activeInHierarchy);
        dialogUI[1].SetActive(false);
        maskImage.SetActive(false);
        SceneManager.LoadScene(1);
    }   
}
