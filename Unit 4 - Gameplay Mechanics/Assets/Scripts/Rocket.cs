using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public float strength;

    private Transform target;
    
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
        transform.LookAt(target);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (target != null)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -other.contacts[0].normal;
                enemyRB.AddForce(away * strength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
    public void Fire(Transform _target)
    {
        target = _target;
    }
}
