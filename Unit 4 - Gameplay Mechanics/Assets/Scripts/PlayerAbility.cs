using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public static PlayerAbility instance;

    public PowerupType currentPowerup = PowerupType.None;

    public float powerupTime = 5f;
    public float fireRate = 1f;

    private float fireCountdown;

    [Header("Push")]
    public float pushStrength = 15f;

    [Header("Smash")]
    public float jumpTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    [HideInInspector] public bool hasPowerup;
    [HideInInspector] public bool isSmashing = false;

    [Header("Unity Stuff")]
    public GameObject rocketPrefab;
    public GameObject powerupIndicator;

    private Coroutine powerupCountdown;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();   
    }
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        IndicatorBehavior();
        CheckPowerup();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.Push)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * pushStrength, ForceMode.Impulse);
        }
    }
    private void CheckPowerup()
    {
        if (currentPowerup == PowerupType.Rocket)
        {
            LaunchRockets();
        }

        if (currentPowerup == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space) && !isSmashing)
        {
            isSmashing = true;
            StartCoroutine(Smash());
        }
    }
    private void LaunchRockets()
    {
        if (Time.time > fireCountdown)
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                if (enemy != null)
                {
                    GameObject tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
                    tmpRocket.GetComponent<Rocket>().Fire(enemy.transform);
                }
            }
            fireCountdown = Time.time + fireRate;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Pick();

            currentPowerup = other.GetComponent<Powerup>().powerupType;

            Destroy(other.gameObject);
        }
    }
    private void Pick()
    {
        hasPowerup = true;

        if (powerupCountdown != null)
        {
            StopCoroutine(powerupCountdown);
        }

        powerupCountdown =  StartCoroutine(PowerupCountdown());
    }
    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasPowerup = false;
        currentPowerup = PowerupType.None;
    }
    private IEnumerator Smash()
    {
        float floor = transform.position.y;
        float _jumpTime = Time.time + jumpTime;

        while (Time.time < _jumpTime)
        {
            rb.velocity = new Vector3(rb.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floor)
        {
            rb.velocity = new Vector3(rb.velocity.x, -smashSpeed * 2f);
            yield return null;
        }

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position,
                explosionRadius, 0f, ForceMode.Impulse);
        }

        isSmashing = false;
    }
    private void IndicatorBehavior()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (hasPowerup)
        {
            powerupIndicator.SetActive(true);
        }
        else
        {
            powerupIndicator.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        if (currentPowerup == PowerupType.Smash)
        {
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
