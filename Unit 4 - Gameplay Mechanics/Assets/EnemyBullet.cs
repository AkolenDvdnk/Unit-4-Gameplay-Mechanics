using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float pushStrength;

    private GameObject player;
    private Vector3 direction;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        if (PlayerAbility.instance != null)
         direction = (player.transform.position - transform.position).normalized;

        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(direction * pushStrength, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
