﻿using UnityEngine;

public class Minion : MonoBehaviour
{
    public float fireRate = 2f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float fireCountdown;

    private void Update()
    {
        if (Time.time > fireCountdown)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            fireCountdown = Time.time + fireRate;
        }
    }
    
}
