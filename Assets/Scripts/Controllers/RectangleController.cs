using System.Collections.Generic;
using UnityEngine;

public class RectangleController : BaseController, IFixedExecute
{
    private List<RectangleView> _views;
    private RectangleView _tempView;
    private Vector2 _rectangleSize = new Vector2(2.0f, 1.0f);
    private Vector2 _startPos;
    private Vector2 _currentPos;

    public RectangleController(MainController main) : base(main)
    {
        _views = new List<RectangleView>();
    }

    public override void Initialize()
    {
        //Events
        InputEvents.Current.OnLeftButtonClick += CreateRectangle;
        InputEvents.Current.OnLeftButtonDoubleClick += DeleteRectangle;
        InputEvents.Current.OnLeftButtonDrag += MoveRectangle;
        InputEvents.Current.OnRightButtonClick += SetConnection;
    }

    public void FixedExecute()
    {
        DrawLines();
    }

    private void DrawLines()
    {
        if (_views.Count < 2)
        {
            return;
        }

        for (int i = 0; i < _views.Count; i++)
        {
            if (_views[i].IsConnected)
            {
                _views[i].DrawConnection();
            }
        }
    }

    public void AddView(RectangleView view)
    {
        _views.Add(view);
    }

    private void DestroyView(RectangleView view)
    {
        if (_views.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _views.Count; i++)
        {
            if (_views[i] == view)
            {
                _views.RemoveAt(i);
                GameObject.Destroy(view.transform.gameObject);
            }
        }
    }

    private void CreateRectangle(Vector3 mousePosition)
    {
        _startPos = Camera.main.ScreenToWorldPoint(mousePosition);

        if (!Physics2D.BoxCast(_startPos, _rectangleSize, 0.0f, Vector2.zero, 0.0f))
        {
            RectangleView rectangle = GameObject.Instantiate(Resources.Load<GameObject>("Rectangle"),
                new Vector3(_startPos.x, _startPos.y, 0.0f),
                Quaternion.identity)
                .GetComponent<RectangleView>();
            rectangle.SetColor();
        }
    }

    private void DeleteRectangle(Vector3 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out RectangleView view))
            {
                DestroyView(view);
            }
        }
    }

    private void MoveRectangle(Vector3 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out RectangleView view))
            {
                _currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                view.Move(_currentPos - _startPos);
                _startPos = _currentPos;
            }
        }
    }

    private void SetConnection(Vector3 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

        if (hit)
        {
            if (_tempView == null)
            {
                if (hit.transform.TryGetComponent(out RectangleView view))
                {
                    _tempView = view;
                }
            }
            else
            {
                if (hit.transform.TryGetComponent(out RectangleView view))
                {
                    if (view != _tempView)
                    {
                        _tempView.SetBuddy(view);
                        _tempView = null;
                    }
                    else
                    {
                        _tempView = null;
                    }
                }
                else
                {
                    _tempView = null;
                }
            }
        }
        else
        {
            _tempView = null;
        }
    }
}