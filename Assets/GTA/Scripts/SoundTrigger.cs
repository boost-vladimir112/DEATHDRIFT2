using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
 
    [SerializeField] private AudioSource m_Source;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           
            m_Source.Play();
        }
    }
}
