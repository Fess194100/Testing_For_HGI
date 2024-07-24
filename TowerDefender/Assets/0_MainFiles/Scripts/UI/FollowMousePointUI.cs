using UnityEditor.Rendering;
using UnityEngine;

public class FollowMousePointUI : MonoBehaviour
{
    public Vector2 offsetPosition;
    private Camera CameraUI;
    private RectTransform rectTransformUI;

    void Start()
    {
        CameraUI = FindObjectOfType<Canvas>().GetComponentInChildren<Camera>();
        rectTransformUI = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (rectTransformUI != null)
        {
            Vector2 cursorPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformUI.parent as RectTransform, Input.mousePosition, CameraUI, out cursorPos);
            rectTransformUI.localPosition = cursorPos + offsetPosition;
        }
    }
}
