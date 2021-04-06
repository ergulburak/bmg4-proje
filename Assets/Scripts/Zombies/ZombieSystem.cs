using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Zombies
{
    public class ZombieSystem : MonoBehaviour
    {
        public float health = 100f;
        [SerializeField] public LayerMask wallLayerMask;
        private Rigidbody2D m_Rb2D;
        private BoxCollider2D m_BoxCollider2D;
        public GameObject splatParticlesPrefab;
        public GameObject splatPrefabs;
        private Transform _splatHolder;
        private List<GameObject> _splatPSHolder;

        private void Start()
        {
            _splatPSHolder = new List<GameObject>();
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
            m_Rb2D = GetComponent<Rigidbody2D>();
            _splatHolder = GameObject.FindWithTag("Splat Holder").transform;
        }

        public void TakeDamage(float value, Vector2 hitPoint)
        {
            health -= value;
            if (health <= 0)
            {
                Destroy(this.gameObject, 1f);
            }

            GameObject splat = Instantiate(splatPrefabs, hitPoint, Quaternion.identity);
            splat.transform.SetParent(_splatHolder, true);
            BloodSplitter splatScript = splat.GetComponent<BloodSplitter>();
            if (ObjectPool.Instance.BloodParticlesPool.Count <= 0)
            {
                GameObject splatParticleSystemGameObject =
                    Instantiate(splatParticlesPrefab, hitPoint, Quaternion.identity);
                splatParticleSystemGameObject.transform.position = hitPoint;
                splatParticleSystemGameObject.GetComponent<ParticleSystem>().Play();
                _splatPSHolder.Add(splatParticleSystemGameObject);
            }
            else
            {
                GameObject splatParticleSystemGameObject = ObjectPool.Instance.BloodParticlesPool.Last();
                splatParticleSystemGameObject.SetActive(true);
                ObjectPool.Instance.BloodParticlesPool.Remove(splatParticleSystemGameObject);
                splatParticleSystemGameObject.transform.position = hitPoint;
                splatParticleSystemGameObject.GetComponent<ParticleSystem>().Play();
                _splatPSHolder.Add(splatParticleSystemGameObject);
            }

            splatScript.Initialize(IsGrounded()
                ? BloodSplitter.SplatLocation.Wall
                : BloodSplitter.SplatLocation.Background);
            
            if (_splatPSHolder.Count > 0)
            {
                foreach (var ps in _splatPSHolder.ToList())
                {
                    if (ps.GetComponent<ParticleSystem>().isStopped)
                    {
                        ps.SetActive(false);
                        ObjectPool.Instance.BloodParticlesPool.Add(ps);
                        _splatPSHolder.Remove(ps);
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 9&& m_Rb2D.velocity.magnitude>3f)
            {
                TakeDamage(5,other.contacts[0].point);
            }
        }

        private bool IsGrounded()
        {
            float extraHeightText = .9f;
            var bounds = m_BoxCollider2D.bounds;
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down,
                extraHeightText, wallLayerMask);
            return raycastHit2D.collider != null;
        }
    }
}