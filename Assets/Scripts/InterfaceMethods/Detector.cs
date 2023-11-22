using UnityEngine;

public class MouseClickDetection : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Столкновение с объектом: " + hit.collider.gameObject.name);
        }
    }
}

