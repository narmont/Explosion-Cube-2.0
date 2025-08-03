using System;
using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void CastFromMainCamera()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out Cube cube))
        {
            cube.HandleClick();
        }
    }
}
