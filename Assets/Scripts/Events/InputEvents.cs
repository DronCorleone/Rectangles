using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public static InputEvents Current;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Current = this;
    }

    public event Action<Vector3> OnLeftButtonClick;
    public void LeftButtonClick(Vector3 mousePosition)
    {
        OnLeftButtonClick?.Invoke(mousePosition);
    }

    public event Action<Vector3> OnLeftButtonDoubleClick;
    public void LeftButtonDoubleClick(Vector3 mousePosition)
    {
        OnLeftButtonDoubleClick?.Invoke(mousePosition);
    }

    public event Action<Vector3> OnLeftButtonDrag;
    public void LeftButtonDrag(Vector3 mousePosition)
    {
        OnLeftButtonDrag?.Invoke(mousePosition);
    }

    public event Action<Vector3> OnRightButtonClick;
    public void RightButtonClick(Vector3 mousePosition)
    {
        OnRightButtonClick?.Invoke(mousePosition);
    }
}