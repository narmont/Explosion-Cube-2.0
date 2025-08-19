using System;
using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _inputReader.OnClicked += HandleClick;
    }

    private void OnDisable()
    {
        _inputReader.OnClicked -= HandleClick;
    }

    public void HandleClick(Vector2 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out Cube cube))
        {
            cube.HandleClick();
        }
    }
}
