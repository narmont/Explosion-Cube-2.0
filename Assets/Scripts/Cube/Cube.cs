using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnClicked;
    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;
    public float ChanceToSplit { get; private set; } = 100;

    public void Init(Vector3 position, Vector3 scale, float chanceToSplit)
    {
        transform.position = position;
        transform.localScale = scale;
        ChanceToSplit = chanceToSplit;
    }

    public void HandleClick()
    {
        OnClicked?.Invoke(this);
    }
}
