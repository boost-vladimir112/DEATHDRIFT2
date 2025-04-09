using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // ��� �������� (�� ��������� Y)
    public float rotationSpeed = 100f;        // �������� �������� � ��������/���

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
