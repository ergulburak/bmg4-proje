using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float turnTorque = 10f;
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
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, 360) * Time.fixedDeltaTime);
        rb.AddTorque(turnTorque);
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
    private Player player;
    [SerializeField] private float pushForce = 4f;

    private bool isDragging = false;
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
            isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
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


        trajectory.UpdateDots(player.pos, _force);
    }

    private void OnDragEnd()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        player.DesactivateRb();
        player.ActivateRb();
        if (_force.x > 0)
        {
            if (player.turnTorque > 0)
                player.turnTorque *= -1;
        }
        else
        {
            if (player.turnTorque < 0)
                player.turnTorque *= -1;
        }

        player.Push(_force);
        trajectory.Hide();
    }
}