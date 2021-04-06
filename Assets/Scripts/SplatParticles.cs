using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatParticles : MonoBehaviour
{
    public ParticleSystem splatParticles;
    public GameObject splatPrefab;
    private Transform _splatHolder;
    private readonly List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        _splatHolder = GameObject.FindWithTag("Splat Holder").transform;
    }

    private void OnParticleCollision(GameObject other)
    {
        splatParticles.GetCollisionEvents(other, m_CollisionEvents);
        int count = m_CollisionEvents.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject splat = Instantiate(splatPrefab, m_CollisionEvents[i].intersection, Quaternion.identity);
            splat.transform.SetParent(_splatHolder,true);
            BloodSplitter splatScript = splat.GetComponent<BloodSplitter>();
            splatScript.Initialize(BloodSplitter.SplatLocation.Wall);
        }
    }
}