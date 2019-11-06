using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;

    public void SetPanel(int panelIndex)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            if(i == panelIndex)
            {
                panels[panelIndex].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    private void Start()
    {
        panels[0].SetActive(true);
    }
}
