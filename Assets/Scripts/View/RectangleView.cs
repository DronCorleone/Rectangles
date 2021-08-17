using UnityEngine;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LineRenderer))]
public class RectangleView : BaseObjectView
{
    private RectangleController _controller;
    private BaseObjectView _objectToConnect;
    private LineRenderer _lineRenderer;
    private bool _isConnected;

    public bool IsConnected => _isConnected;


    private void Awake()
    {
        FindMyController();
        _lineRenderer = GetComponent<LineRenderer>();
    }


    private void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<RectangleController>();
        }

        _controller.AddView(this);
    }

    public void SetColor()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f);
    }

    public void Move(Vector2 delta)
    {
        Vector2 newPosition = (Vector2)transform.position + delta;

        if (Physics2D.BoxCastAll(newPosition, transform.localScale, 0.0f, Vector2.zero).Length <= 1)
        {
            transform.position += new Vector3(delta.x, delta.y, 0.0f);
        }
    }

    public void SetBuddy(BaseObjectView buddy)
    {
        if (buddy != null)
        {
            _objectToConnect = buddy;
            _isConnected = true;
        }
    }

    public void DrawConnection()
    {
        if (!_isConnected)
        {
            return;
        }

        if (_objectToConnect == null)
        {
            _isConnected = false;
            _lineRenderer.SetPosition(1, transform.position);
            return;
        }

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _objectToConnect.Position);
    }
}