using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Exploder))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Exploder _explode;

    public event Action<Cube> Splited;
    public Vector3 Position => transform.position;
    public float ChanceToSplit { get; private set; } = 100;

    private void Awake()
    {
        _explode = GetComponent<Exploder>();
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void Init(Vector3 position, Vector3 scale, float chance)
    {
        transform.position = position;
        transform.localScale = scale;
        ChanceToSplit = chance; 
    }

    public void Explode()
    {
        float maximumChance = 100f;
        float minimumChance = 0f;
        float chance = Random.Range(minimumChance, maximumChance + 1);

        if (chance <= ChanceToSplit)
        {
            Splited?.Invoke(this);
        }
        else
        {
            _explode.Exploded();
        }    
    }
}
