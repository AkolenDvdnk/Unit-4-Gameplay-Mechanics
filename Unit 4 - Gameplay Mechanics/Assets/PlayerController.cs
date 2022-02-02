using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Transform focalPoint;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddForce(focalPoint.forward * speed * verticalInput);
    }
}
