using UnityEngine;

public class PlatformRotating : MonoBehaviour
{
    public float autoRotateSpeed = 0.1f; // �������� ���� ��������
    public float dragRotateSpeed = 0.2f; // �������� �������� ��� ��������������

    private bool isDragging = false;
    private Vector2 lastMousePosition;

    void Update()
    {
        // �������������� ��������, ���� �� �������������
        if (!isDragging)
        {
            transform.Rotate(0, autoRotateSpeed, 0);
        }

        // ���������� �����
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;
            transform.Rotate(0, -delta.x * dragRotateSpeed, 0); // �������� �� �����������
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ���������� ������� (��������� ����������)
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
                transform.Rotate(0, -delta.x * dragRotateSpeed * 0.1f, 0); // �������� ���������������� ��� ������
                lastMousePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }
}
