using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _pointPosition;
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private int _divisionScale = 2;
    [SerializeField] private int _divisionChanceSplit = 2;

    public event Action<Cube> OnCubeCreated;

    private void Start()
    {
        CreateInitialCubes();
    }

    private List<Cube> CreateRedusedCubes(Cube cube, Vector3 scale, float chanceToSplite)
    {
        int minRandomValue = 2;
        int maxRandomValue = 6;
        int countCubes = Random.Range(minRandomValue, maxRandomValue + 1);
        var newCubes = new List<Cube>();

        for (int i = 0; i < countCubes; i++)
        {
            var newCube = CreateCube(cube, cube.Position);
            newCube.Init(cube.transform.position, scale, chanceToSplite);
            newCubes.Add(newCube);
        }

        return newCubes;
    }

    private void CreateInitialCubes()
    {
        CreateCube(_prefabCube, _pointPosition.position);
        CreateCube(_prefabCube, _pointPosition.position + new Vector3(5, 0, 0));
        CreateCube(_prefabCube, _pointPosition.position + new Vector3(1, 0, -6));
    }

    private Cube CreateCube(Cube cube, Vector3 position)
    {
        Cube newCube = Instantiate(cube, position, Quaternion.identity);
        newCube.OnClicked += HandleCubeClick;

        OnCubeCreated?.Invoke(newCube);
        return newCube;       
    }

    private void HandleCubeClick(Cube cube)
    {
        Vector3 cubePosition = cube.transform.position;

        cube.OnClicked -= HandleCubeClick;

        if (cube.ShouldSplit())
        {
            cube.GetSplitParameters(_divisionScale, _divisionChanceSplit, out Vector3 scale, out float chanceToSplite);
            var newCubes = CreateRedusedCubes(cube, scale, chanceToSplite);
            _exploder.ApplyExplosionCube(cubePosition, newCubes);
        }
        else
        {      
            _exploder.ApplyExplosionAll(cubePosition);
        }

        Destroy(cube.gameObject);
    }
}
