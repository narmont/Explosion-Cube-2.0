using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<Vector2> OnClicked;

    private void Update()
    {
        HandleCubeClickInput();
    }

    private void HandleCubeClickInput()
    {
        int leftMouseButton = 0;

        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            OnClicked?.Invoke(Input.mousePosition);
        }
    }
}
