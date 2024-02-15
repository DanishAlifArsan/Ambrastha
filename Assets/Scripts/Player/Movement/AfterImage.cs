using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sr;
    // private SpriteRenderer playerSR;
    private Color color;
    private float activeTime = .5f;
    private float timeActivated;
    private float alpha;
    private float alphaSet = 1f;
    private float alphaMultiplier = 0.8f;

    private void OnEnable() {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // playerSR = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        // sr.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;
        timeActivated = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1,1,1, alpha);
        sr.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            AfterImagePool.instance.AddToPool(gameObject);
        }
    }
}
