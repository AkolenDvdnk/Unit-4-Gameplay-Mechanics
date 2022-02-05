using UnityEngine;

public class Minion : MonoBehaviour
{
    public float fireRate = 2f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float fireCountdown;

    private void Update()
    {
        if (Boss.instance == null || PlayerAbility.instance == null)
            return;

        Fire();
    }
    private void Fire()
    {
        if (Time.time > fireCountdown)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            fireCountdown = Time.time + fireRate;
        }
    }
}
