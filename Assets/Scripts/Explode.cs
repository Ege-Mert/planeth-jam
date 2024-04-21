using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explode : MonoBehaviour
{
     public GameObject[] astroit;
   

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            
            for (int i = 0; i < astroit.Length; i++)
            {
                astroit[i].GetComponent<Rigidbody>().useGravity = true;
            }
            foreach (Transform child in transform)
            {
             
                child.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(75f, 175f), transform.position, Random.Range(8f,10f));
                Destroy(gameObject,3.5f);
            }
        }
        
    }
}