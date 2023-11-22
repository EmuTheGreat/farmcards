using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            Vector3 newPosition = transform.position - new Vector3(deltaMousePosition.x, deltaMousePosition.y, 0) * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
            lastMousePosition = Input.mousePosition;
        }
    }
}
