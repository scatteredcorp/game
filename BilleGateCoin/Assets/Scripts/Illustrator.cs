using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Illustrator : MonoBehaviour
{
    public GameObject inventoryTitle;
    public GameObject scrollbar;
    
    // Update is called once per frame
    void Update()
    { 
        if (transform.childCount > 0)
        {
            inventoryTitle.SetActive(true);
            if (transform.childCount > 2)
            {
                scrollbar.SetActive(true);
            }
            else
            {
                scrollbar.SetActive(false);
            }
        }
        else
        {
            inventoryTitle.SetActive(false);
            scrollbar.SetActive(false);
        }
    }
}
