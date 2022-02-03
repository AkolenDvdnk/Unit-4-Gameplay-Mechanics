using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public PowerupType currentPowerup = PowerupType.None;

    public float powerupStrength = 15f;
    public float powerupTime = 5f;

    [HideInInspector] public bool hasPowerup;

    public GameObject rocketPrefab;
    public GameObject powerupIndicator;

    private Coroutine powerupCountdown;

    private void Update()
    {
        IndicatorBehavior();
        CheckInput();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.Push)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    private void CheckInput()
    {
        if (currentPowerup == PowerupType.Rocket && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
    }
    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            GameObject tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<Rocket>().Fire(enemy.transform);
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
}
