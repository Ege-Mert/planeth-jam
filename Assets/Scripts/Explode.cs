using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explode : MonoBehaviour
{
    public GameObject[] breakablewalltest;
    [SerializeField]private BoxCollider boxCollider;


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            for (int i = 0; i < breakablewalltest.Length; i++)
            {
                breakablewalltest[i].GetComponent<Rigidbody>().useGravity = true;
            }
            foreach (Transform child in transform)
            {
             
                child.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(750f, 1750f), transform.position, Random.Range(80f,100f));
                Destroy(gameObject,3.5f);
            }
        }
        
    }
}