using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathFloor : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            LosePanel.SetActive(true);

            AudioListener.pause = true;
            Time.timeScale = 0f;
        }
        if(other.CompareTag("Car"))
        {
            Destroy(other.gameObject);
        }
    }
}
