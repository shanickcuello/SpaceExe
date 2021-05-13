using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool attackPlayer, avaibleToShoot, watchOver;
    public GameObject bulletGO, gun, playerGO;
    public float rateOFFire, speedRotation;
    Light light;
    Player playerSCR;

    //Private Variables


    private void Awake()
    {
        playerGO = FindObjectOfType<Player>().gameObject;
        playerSCR = FindObjectOfType<Player>();
        avaibleToShoot = true;
        light = GetComponentInChildren<Light>();
        watchOver = true;
        attackPlayer = false;
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        AttackPlayer();
        WatchOver();
    }

    public void AttackPlayer()
    {
        if (attackPlayer)
        {
            watchOver = false;
            light.color = Color.red;
            transform.forward = playerGO.transform.position - transform.position;

            if (avaibleToShoot)
            {
                Instantiate(bulletGO, gun.transform.position, gun.transform.rotation);
                bulletGO.GetComponent<Bullet>().homming = true;
                avaibleToShoot = false;
                Invoke("AvaibleToShoot", rateOFFire);
            }
        }
    }

    public void AvaibleToShoot()
    {
        avaibleToShoot = true;
    }

    public void WatchOver()
    {
        if (watchOver)
        {
            
            light.color = Color.white;
            transform.Rotate(transform.up, speedRotation * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerGO)
        {
            watchOver = false;
            
            attackPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerGO)
        {
            attackPlayer = false;
            watchOver = true;

        }
    }


}
