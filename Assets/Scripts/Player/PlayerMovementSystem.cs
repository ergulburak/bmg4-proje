using System;
using UnityEngine;
using Zombies;

namespace Player
{
    public class PlayerMovementSystem : MonoBehaviour
    {
        public float damage = 20f;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public CircleCollider2D col;

        [HideInInspector]
        public Vector3 pos
        {
            get { return transform.position; }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<CircleCollider2D>();
            _player = GetComponent<PlayerMovementSystem>();
            _saw = GetComponentInChildren<Saw>();
        }

        public void Push(Vector2 force)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
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

        private Camera _cam;
        public Trajectory trajectory;
        private PlayerMovementSystem _player;
        private Saw _saw;
        [SerializeField] private float pushForce = 4f;

        private bool _isDragging = false;
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private Vector2 _direction;
        private Vector2 _force;
        private float _distance;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                OnDragStart();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                OnDragEnd();
            }

            if (_isDragging)
            {
                OnDrag();
            }
        }

        private void OnDragStart()
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            _startPoint = _cam.ScreenToWorldPoint(Input.mousePosition);
            trajectory.Show();
        }

        private void OnDrag()
        {
            _endPoint = _cam.ScreenToWorldPoint(Input.mousePosition);
            _distance = Vector2.Distance(_startPoint, _endPoint);
            _direction = (_startPoint - _endPoint).normalized;
            _force = _direction * _distance * pushForce;

            //just for debug
            Debug.DrawLine(_startPoint, _endPoint);


            trajectory.UpdateDots(_player.pos, _force);
        }

        private void OnDragEnd()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            _player.DesactivateRb();
            _player.ActivateRb();
            _saw.DesactivateRb();
            _saw.ActivateRb();
            if (_force.x > 0)
            {
                if (_saw.turnTorque > 0)
                    _saw.turnTorque *= -1;
            }
            else
            {
                if (_saw.turnTorque < 0)
                    _saw.turnTorque *= -1;
            }

            _player.Push(_force);
            _saw.Push(_force);
            trajectory.Hide();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                other.gameObject.GetComponent<ZombieSystem>().TakeDamage(damage,other.contacts[0].point);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.gameObject.CompareTag("Flag") && GameManager.instance.EnemyCount == 0)
            {
                Debug.Log("Oyun Bitti");
            }
        }
    }
}