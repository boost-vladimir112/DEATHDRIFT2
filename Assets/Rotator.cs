using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Ось вращения (по умолчанию Y)
    public float rotationSpeed = 100f;        // Скорость вращения в градусах/сек

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
