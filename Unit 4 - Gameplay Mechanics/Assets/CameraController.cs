using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, -horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
