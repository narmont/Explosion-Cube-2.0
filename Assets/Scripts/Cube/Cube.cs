using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnClicked;
    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;
    private float _chanceToSplite = 100;

    public void Init(Vector3 position, Vector3 scale, float chanceToSplit)
    {
        transform.position = position;
        transform.localScale = scale;
        _chanceToSplite = chanceToSplit;
    }

    public bool ShouldSplit()
    {
        float minimumChance = 0f;
        float maximumChance = 100f;
        float chance = Random.Range(minimumChance, maximumChance + 1);
        
        return chance <= _chanceToSplite;
    }

    public (Vector3 scale, float newChance) GetSplitParameters(int divisionScale, int divisionChance)
    {
        Vector3 newScale = transform.localScale / divisionScale;
        float newChance = _chanceToSplite / divisionChance;
        
        return (newScale, newChance);
    }

    public void HandleClick()
    {
        OnClicked?.Invoke(this);
    }
}
