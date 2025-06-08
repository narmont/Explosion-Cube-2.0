using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _pointer;
    [SerializeField] private Camera _mainCamera;

    public event Action<Cube> OnCubeClicked;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        HandleCubeClickInput();
    }

    private void HandleCubeClickInput()
    {
        int indexMouseButtonDown = 0;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 0.1f);

        if (Input.GetMouseButtonDown(indexMouseButtonDown))
        {
            if (Physics.Raycast(ray, out RaycastHit targetHit) && targetHit.collider.TryGetComponent(out Cube cube))
            {
                _pointer.position = targetHit.point;

                OnCubeClicked?.Invoke(cube);
            }
        }
    }
}
