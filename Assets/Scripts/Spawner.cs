using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _pointPosition;
    [SerializeField] private Cube _prefabCube;

    private void Start()
    {
        CreateCube(_prefabCube, _pointPosition.position);
        CreateCube(_prefabCube, _pointPosition.position + new Vector3(5, 0, 0));
        CreateCube(_prefabCube, _pointPosition.position + new Vector3(1, 0, -6));
    }

    private Cube CreateCube(Cube cube, Vector3 position)
    {
        Cube newCube = Instantiate(cube, position, Quaternion.identity);

        return newCube;
    }

    public void CreateRedusedCubes(Cube cube)
    {
        int minRandomValue = 2;
        int maxRandomValue = 6;
        int countCubes = Random.Range(minRandomValue, maxRandomValue + 1);

        for (int i = 0; i < countCubes; i++)
        {
            cube = CreateCube(cube, cube.Position);
        }
    }
}
