using UnityEngine;

public class DraggableNode : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.z = gameObject.transform.position.z;
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(newPos);
            viewportPos.x = Mathf.Clamp01(viewportPos.x);
            viewportPos.y = Mathf.Clamp01(viewportPos.y);
            newPos = mainCamera.ViewportToWorldPoint(viewportPos);
            gameObject.transform.position = newPos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
