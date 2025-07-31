using UnityEngine;

public class ChanceSplit : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _spawner.OnCubeCreated += HandleCubeCreated;
    }

    private void HandleCubeCreated(Cube cube)
    {
        cube.OnClicked += HandleCubeClick;
    }

    private void HandleCubeClick(Cube cube)
    {
        int indexForDecriseScale = 2;
        int indexForDecriseChanceSpleet = 2;
        float maximumChance = 100f;
        float minimumChance = 0f;
        float chance = Random.Range(minimumChance, maximumChance + 1);

        Vector3 scale = cube.transform.localScale / indexForDecriseScale;
        float chanceToSplite = cube.ChanceToSplit / indexForDecriseChanceSpleet;

        if (chance <= chanceToSplite)
        {
            var newCubes = _spawner.CreateRedusedCubes(cube, scale, chanceToSplite);
            _exploder.ApplyExplosionToNewCubesOnly(cube.transform.position, newCubes);
        }
        else
        {
            _exploder.ApplyExplosionToAll(cube.transform.position);
        }
    }

    private void OnDisable()
    {
        _spawner.OnCubeCreated -= HandleCubeCreated;
    }
}
