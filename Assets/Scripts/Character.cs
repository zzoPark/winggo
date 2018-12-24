using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Range(1, 10)] public float health;
    [Range(1, 10)] public float maxSpeed;
    [Range(1, 10)] public float acceleration;
    [Range(1, 10)] public float weight;

    private static float _healthUnit = 50f;
    private static float _speedUnit = 100f;
    private static float _accelUnit = 0.1f;
    private static float _weightUnit = 0.5f;

    [SerializeField] private float _startSpeed = 25f;
    [SerializeField] private float _maxUpSpeed = 15f;
    [SerializeField] private float _upForce = 300f;
    [SerializeField] private float _healthDecreaseAmount = 5f;
    [SerializeField] private Image _healthBar;
    [SerializeField] private LayerMask _whatIsObstacle;

    [ReadOnly] private float _health;
    [ReadOnly] private float _maxSpeed;
    [ReadOnly] private float _acceleration;
    [ReadOnly] private float _weight;
    private float _totalHealth;

    private bool _wing = false;
    private bool _down = true;
    private bool _crashed = false;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _totalHealth = health * _healthUnit;
        _health = _totalHealth;
        _maxSpeed = maxSpeed * _speedUnit;
        _acceleration = acceleration * _accelUnit;
        _weight = weight * _weightUnit;
    }

    private void Start()
    {
        _rigidbody.mass = _weight;
        _rigidbody.velocity = new Vector2(_startSpeed, 0);
    }

    private void Update()
    {
        _wing = true;
        _crashed = false;

        if (_rigidbody.velocity.y < 0)
        {
            _down = true;
        }
        else
        {
            _down = false;
        }

        _animator.SetBool("Wing", _wing);
        _animator.SetBool("Down", _down);

        DecreaseHealth();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Up"))
        {
            if (_rigidbody.velocity.y < _maxUpSpeed)
            {
                _rigidbody.AddForce(Vector2.up * _upForce);
            }
        }

        if (_rigidbody.velocity.x < _maxSpeed)
        {
            _rigidbody.AddForce(Vector2.right * _acceleration);
        }
    }

    private void DecreaseHealth()
    {
        _health -= _healthDecreaseAmount * Time.deltaTime;
        UpdateHealthBar();
    }

    private void TakeDamage(float amount)
    {
        _health -= amount;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _health / _totalHealth;
    }
}
