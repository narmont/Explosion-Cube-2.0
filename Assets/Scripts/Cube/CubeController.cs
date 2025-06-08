using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private void OnEnable() => _player.OnCubeClicked += HandleCubeClick;
    private void OnDisable() => _player.OnCubeClicked -= HandleCubeClick;

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
            _spawner.CreateRedusedCubes(cube, scale, chanceToSplite);

            _exploder.ExplodedSplit();
        }
        
        Destroy(cube.gameObject);
    }
}
