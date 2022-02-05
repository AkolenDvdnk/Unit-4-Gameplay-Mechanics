using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private Vector3 lookDirection;
    private GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        MoveEnemy();
        DestroyEnemy();
    }
    private void MoveEnemy()
    {
        if (player != null)
         lookDirection = (player.transform.position - transform.position).normalized;

        rb.AddForce(lookDirection * speed);
    }
    private void DestroyEnemy()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
