using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _pointPosition;
    [SerializeField] private Cube _prefabCube;

    public event Action<Cube> OnCubeCreated;

    public List<Cube> CreateRedusedCubes(Cube cube, Vector3 scale, float chanceToSplite)
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

    private void Start()
    {
        CreateInitialCubes();
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
        cube.OnClicked -= HandleCubeClick;
        Destroy(cube.gameObject);
    }
}
