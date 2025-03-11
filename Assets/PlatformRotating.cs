using UnityEngine;

public class PlatformRotating : MonoBehaviour
{
   
    void Update()
    {
        gameObject.transform.Rotate(0, 0.07f, 0);
    }
}
