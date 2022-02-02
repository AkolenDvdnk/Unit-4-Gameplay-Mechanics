using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;

    [Header("Powerup")]
    public float powerupStrength = 15f;
    public float powerupTime = 5f;

    public GameObject powerupIndicator;

    private bool hasPowerup;

    [Header("Unity stuff")]
    public Transform focalPoint;

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
        MovePlayer();

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    private void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddForce(focalPoint.forward * speed * verticalInput);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);

            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
        }
    }
    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(powerupTime);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
