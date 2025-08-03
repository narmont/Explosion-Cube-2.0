using UnityEngine;

public class SpawnerDivision : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private int _divisionScale = 2;
    [SerializeField] private int _divisionChanceSplit = 2;

    private void OnEnable()
    {
        _spawner.OnCubeCreated += HandleCubeCreated;
    }

    private void OnDisable()
    {
        _spawner.OnCubeCreated -= HandleCubeCreated;
    }

    private void HandleCubeCreated(Cube cube)
    {
        if (cube != null)
            cube.OnClicked += HandleCubeClick;
    }

    private void HandleCubeClick(Cube cube)
    {
        if (cube.ShouldSplit())
        {
            var (scale, chanceToSplite) = cube.GetSplitParameters(_divisionScale, _divisionChanceSplit);

            var newCubes = _spawner.CreateRedusedCubes(cube, scale, chanceToSplite);
            _exploder.ApplyExplosionCube(cube.transform.position, newCubes);
        }
        else
        {
            _exploder.ApplyExplosionToAll(cube.transform.position);
        }
    }
}
