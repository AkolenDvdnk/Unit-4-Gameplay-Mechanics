using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        CheckInput();
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();        }
    }
    private void Quit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        if (PlayerAbility.instance == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
