using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uı : MonoBehaviour
{
    public GameObject canvas; // Canvas nesnesini bu alandan ata

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab tuşu kontrolü
        {
            canvas.SetActive(true);// Canvas'i açıp/kapatan fonksiyonu çağır
        }
    }

    public void CanvasClose()
    {
        canvas.SetActive(false);
    }
    
    
}
