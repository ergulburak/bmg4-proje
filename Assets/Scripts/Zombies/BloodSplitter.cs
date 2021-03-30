using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodSplitter : MonoBehaviour
{
    public GameObject[] splats;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MakeSplat();
        }
    }

    public void MakeSplat()
    {
        Instantiate(splats[Random.Range(0, splats.Length)], transform.position, transform.rotation);
    }
}