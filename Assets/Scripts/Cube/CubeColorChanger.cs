using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeColorChanger : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}