using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speedBullet;
    public float damage;
    public GameObject explosionFX;
    public bool homming;
    GameObject playerGO;
    Vector3 lastDir = new Vector3();

    private void Awake()
    {
        playerGO = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
        Boom();
    }

    private void Boom()
    {
        if (Vector3.Distance(transform.position, playerGO.transform.position) < 1)
        {
            playerGO.GetComponent<Player>().TakeDamage(damage);
            Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(gameObject);
            GoalsManager.instance.playerGetBullet = true;
        }
    }

    private void Fly()
    {
        if (homming)
        {
            //if (Vector3.Distance(transform.position, playerGO.transform.position) < 3)
            //{
                transform.forward = (playerGO.transform.position - transform.position).normalized;
                transform.position += transform.forward * Time.deltaTime * speedBullet;
            //}
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * speedBullet;
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
