using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private List<BaseController> _controllers = new List<BaseController>();
    private InputController _inputController;
    private RectangleController _rectangleController;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //Controllers
        _inputController = new InputController(this);
        _rectangleController = new RectangleController(this);
    }

    private void Start()
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i] is IInitialize)
            {
                _controllers[i].Initialize();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i] is IExecute)
            {
                (_controllers[i] as IExecute).Execute();
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i] is IFixedExecute)
            {
                (_controllers[i] as IFixedExecute).FixedExecute();
            }
        }
    }


    public void AddController(BaseController controller)
    {
        if (!_controllers.Contains(controller))
        {
            _controllers.Add(controller);
        }
    }

    public void RemoveController(BaseController controller)
    {
        if (_controllers.Contains(controller))
        {
            _controllers.Remove(controller);
        }
    }

    public T GetController<T>() where T : BaseController
    {
        foreach (BaseController obj in _controllers)
        {
            if (obj.GetType() == typeof(T))
            {
                return (T)obj;
            }
        }
        return null;
    }
}