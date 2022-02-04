using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;

    [Header("Unity stuff")]
    public Transform focalPoint;

    private float verticalInput;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        if (!PlayerAbility.instance.isSmashing)
        {
            rb.AddForce(focalPoint.forward * speed * verticalInput);
        }
    }
}
