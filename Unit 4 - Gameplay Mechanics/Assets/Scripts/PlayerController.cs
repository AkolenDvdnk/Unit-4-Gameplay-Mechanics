using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public GameObject endGameText;

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
        DestroyPlayer();
    }
    private void FixedUpdate()
    {
        if (!PlayerAbility.instance.isSmashing)
        {
            rb.AddForce(focalPoint.forward * speed * verticalInput);
        }
    }
    private void DestroyPlayer()
    {
        if (transform.position.y < -10)
        {
            endGameText.SetActive(true);
            Destroy(gameObject);
        }
    }
}
