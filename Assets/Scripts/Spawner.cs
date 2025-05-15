using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _pointPosition;
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private Player _player;

    private int _minRandomValue = 2;
    private int _maxRandomValue = 6;
    private int _indexForDerciseChanceSpleet = 2;

    private void Start()
    {
        CreateCube(_prefabCube, _pointPosition.position);
        CreateCube(_prefabCube, _pointPosition.position + new Vector3(5, 0, 0));
        CreateCube(_prefabCube, _pointPosition.position + new Vector3(1, 0, -6));
    }

    private Cube CreateCube(Cube cube, Vector3 position)
    {
        Cube newCube = Instantiate(cube, position, Quaternion.identity);

        newCube.Split += CreateRedusedCubes;

        return newCube;
    }

    private void CreateRedusedCubes(Cube newCube)
    {
        newCube.Split -= CreateRedusedCubes;

        int countCubes = Random.Range(_minRandomValue, _maxRandomValue);

        List<Cube> cubes = new List<Cube>();

        Vector3 scale = newCube.transform.localScale / 2;
        float chanceToSplite = newCube.ChanceToSplit / _indexForDerciseChanceSpleet;

        Debug.Log(chanceToSplite);

        for (int i = 0; i < countCubes; i++)
        {
            newCube = CreateCube(newCube, newCube.Position);
            newCube.Init(newCube, scale, chanceToSplite);
            cubes.Add(newCube);
        }
    }
}
