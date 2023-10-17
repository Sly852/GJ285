using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletItem;
    public Transform spawn;
    public float bulletSpeed = 0.025f;

    private float cooldownTime = 5;
    private float cooldownTimer = 0;
    private bool isCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCooldown)
        {
            SpawnBullet();
            isCooldown = true;
            cooldownTimer = cooldownTime;
        }
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                isCooldown = false;
                cooldownTimer = 0;
            }
        }
        
    }


    void SpawnBullet()
    {
        var bullet = Instantiate(bulletItem, spawn.position, spawn.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = -spawn.right * bulletSpeed;
    }
}
