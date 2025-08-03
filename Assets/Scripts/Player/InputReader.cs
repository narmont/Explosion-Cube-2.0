using UnityEngine;

[RequireComponent(typeof(RaycastInteractor))]
public class InputReader : MonoBehaviour
{
    private RaycastInteractor _raycastHandler;

    private void Awake()
    {
        _raycastHandler = GetComponent<RaycastInteractor>();
    }

    private void Update()
    {
        HandleCubeClickInput();
    }

    private void HandleCubeClickInput()
    {
        int leftMouseButton = 0;

        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            _raycastHandler.CastFromMainCamera();
        }
    }
}
