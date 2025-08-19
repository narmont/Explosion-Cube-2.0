using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    private float _chanceToSplit = 100;

    public event Action<Cube> OnClicked;

    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;

    public void Init(Vector3 position, Vector3 scale, float chanceToSplit)
    {
        transform.position = position;
        transform.localScale = scale;
        _chanceToSplit = chanceToSplit;
    }

    public bool ShouldSplit()
    {
        float minimumChance = 0f;
        float maximumChance = 100f;
        float chance = Random.Range(minimumChance, maximumChance + 1);
        
        return chance <= _chanceToSplit;
    }

    public void GetSplitParameters(int divisionScale, int divisionChance, out Vector3 newScale, out float newChance)
    {
        newScale = transform.localScale / divisionScale;
        newChance = _chanceToSplit / divisionChance;
    }

    public void HandleClick() => OnClicked?.Invoke(this);
}
