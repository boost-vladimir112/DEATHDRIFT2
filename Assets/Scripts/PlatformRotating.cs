using UnityEngine;

public class PlatformRotating : MonoBehaviour
{
    public float autoRotateSpeed = 0.1f; // Скорость авто вращения
    public float dragRotateSpeed = 0.2f; // Скорость вращения при перетаскивании

    private bool isDragging = false;
    private Vector2 lastMousePosition;

    void Update()
    {
        // Автоматическое вращение, если не перетаскиваем
        if (!isDragging)
        {
            transform.Rotate(0, autoRotateSpeed, 0);
        }

        // Управление мышью
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;
            transform.Rotate(0, -delta.x * dragRotateSpeed, 0); // вращение по горизонтали
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Управление пальцем (мобильные устройства)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastMousePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - lastMousePosition;
                transform.Rotate(0, -delta.x * dragRotateSpeed * 0.1f, 0); // уменьшил чувствительность для пальца
                lastMousePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }
}
