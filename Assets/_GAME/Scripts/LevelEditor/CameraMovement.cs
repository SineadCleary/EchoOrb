using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCamera;
    Vector3 origin;
    Vector3 newPosDifference;
    bool isDragging;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Code based on code seen in YouTube video: https://www.youtube.com/watch?v=H7pjj1K91HE
    private Vector3 GetMousePosition => mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        if (context.started)
            origin = GetMousePosition;
        isDragging = context.started || context.performed;
    }

    private void LateUpdate()
    {
        if (!isDragging) return;
        newPosDifference = GetMousePosition - transform.position;
        transform.position = origin - newPosDifference;
    }

    public void zoom(int direction = 1)
    {
        float zoomSize = mainCamera.orthographicSize;
        mainCamera.orthographicSize = Mathf.Clamp(zoomSize - direction * 2, 2f, 14f);
    }
}
