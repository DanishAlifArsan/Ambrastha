using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private int currentWeapon;
    [SerializeField] private GameObject[] weaponGameObject;
    [SerializeField] private Sprite[] maskSprite;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer mask;

    private void Awake()
    {
        SwitchWeapon(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchWeapon(-1);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            SwitchWeapon(1);
        }
    }

    private void SelectWeapon(int index) {
        if(index < 0) {
            index = transform.childCount - 1;
            currentWeapon = transform.childCount - 1;
        }
        if (index > transform.childCount -1)
        {
            index = 0;
            currentWeapon = 0;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i==index);
            weaponGameObject[i].SetActive(i==index);
            weaponGameObject[i].GetComponent<ParticleSystem>().Stop();     
            ResetParticlePosition(i); 
        } 
        if (mask != null)
        {
            mask.sprite = maskSprite[index];    
        }  
    }

    public void SwitchWeapon(int change) {
        currentWeapon += change;
        SelectWeapon(currentWeapon);
    }

    private void ResetParticlePosition(int i) {
        if (player.localScale.x > .1f)
        {
            weaponGameObject[i].transform.eulerAngles = new Vector3(0,90,0);
        } else {
            weaponGameObject[i].transform.eulerAngles = new Vector3(0,-90,0);
        }
    }
}
