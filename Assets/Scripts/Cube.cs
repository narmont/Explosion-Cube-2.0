using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Explode))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Explode _explode;
    [SerializeField] private Player _player;

    private Rigidbody _rigidbody;
    private float _maximumChance = 100f;
    private float _minimumChance = 0f;

    public event Action<Cube> Split;
    public Vector3 Position => transform.position;
    public float ChanceToSplit { get; private set; } = 100;

    private void Awake()
    {
        _explode = GetComponent<Explode>();
        _rigidbody = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void Init(Cube cube, Vector3 scale, float chance)
    {
        transform.position = cube.transform.position;
        transform.localScale = scale;
        ChanceToSplit = chance; 
    }

    public void Explode()
    {
        float chance = Random.Range(_minimumChance, _maximumChance);

        if (chance <= ChanceToSplit)
        {
            Split?.Invoke(this);
        }
        else
        {
            _explode.Exploded();
        }    
    }
}
