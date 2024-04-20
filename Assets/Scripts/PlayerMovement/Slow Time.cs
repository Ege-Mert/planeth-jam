using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    private bool isSlowMotion = false;
    public ImageController ImageControllerr;

    private void Start()
    {
        ImageControllerr = GetComponent<ImageController>();
    }

    void Update()
    {
        // Tab tuşuna basıldığında
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleSlowMotion();
        }
        
        // Yavaş hareket modunda iken T tuşuna basıldığında
        if (isSlowMotion && Input.GetKeyDown(KeyCode.Tab)) //Buray tümü atandığını kontrol eden bir bool gelecek t tuşu yerine 
        {
          //      CancelSlowMotion();
            
        }
    }

    void ToggleSlowMotion()
    {
        // Yavaş hareket modunu aç veya kapat
        if (!isSlowMotion)
        {
            Time.timeScale = 0.2f; // Zamanı yavaşlat
            isSlowMotion = true;
        }
        else
        {
            Time.timeScale = 1f; // Zamanı normale geri döndür
            isSlowMotion = false;
        }
    }

    public void CancelSlowMotion()
    {
        // Yavaş hareketi iptal et
        Time.timeScale = 1f; // Zamanı normale geri döndür
        isSlowMotion = false;
        Debug.Log("slowmo iptal");
    }
}
