using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _pointer;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Guide();
    }

    private void Guide()
    {
        RaycastHit targetHit;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_mainCamera.transform.position, mousePos, Color.yellow);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out targetHit))
            {
                _pointer.position = targetHit.point;

                DestroyTargetCube(targetHit);
            }
        }
    }

    private void DestroyTargetCube(RaycastHit targetHit)
    {
        var cube = targetHit.collider.gameObject.GetComponent<Cube>();      

        if (cube)
        {
            Destroy(targetHit.collider.gameObject);

            cube.Explode();
        }
    }
}
