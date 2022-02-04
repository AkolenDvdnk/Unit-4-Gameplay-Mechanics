using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;

    [Header("Unity stuff")]
    public Transform focalPoint;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        if (!PlayerAbility.instance.isSmashing)
        {
            float verticalInput = Input.GetAxis("Vertical");

            rb.AddForce(focalPoint.forward * speed * verticalInput);
        }
    }
}
