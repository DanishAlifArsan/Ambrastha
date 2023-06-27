using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private int currentWeapon;
    [SerializeField] private GameObject[] weaponGameObject;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    private void Awake()
    {
        SwitchWeapon(0);
    }

    // Update is called once per frame
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
