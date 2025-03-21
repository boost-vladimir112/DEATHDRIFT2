using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Tutorial : MonoBehaviour
{
    public GameObject TutorPanel;
    void Start()
    {
        bool deviceIsDesktop = YG2.envir.isDesktop;
        if(deviceIsDesktop )
        {
            TutorPanel.SetActive(true);
        }
    }

   
}
