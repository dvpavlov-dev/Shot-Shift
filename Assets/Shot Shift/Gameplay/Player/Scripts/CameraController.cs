using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly Vector3 _cameraPosition = new(0, 15, -6);
    private Transform _camera;

    private void Update()
    {
        if (_camera != null)
        {
            _camera.transform.position = transform.position + _cameraPosition;
        }
    }

    private void OnEnable()
    {
        _camera = Camera.main.transform;
    }
}
