using System;
using UnityEngine;

namespace Player
{
    public class Saw : MonoBehaviour
    {
        public float turnTorque = 10f;
        [HideInInspector] public Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb.AddTorque(turnTorque,ForceMode2D.Force);
        }
        public void ActivateRb()
        {
            rb.isKinematic = false;
        }

        public void DesactivateRb()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;
        }
        public void Push(Vector2 force)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}