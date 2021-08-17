using UnityEngine;

public abstract class BaseController : IInitialize
{
    protected bool _isActive = true;
    protected MainController _main;

    public MainController Main => _main;
    public bool IsActive => _isActive;


    public BaseController(MainController main)
    {
        main.AddController(this);
        _main = main;
        Debug.Log($"{this.GetType()} added in controller list");
    }

    protected virtual void SetState(bool state)
    {
        _isActive = state;
    }

    public virtual void Initialize()
    {

    }

    public virtual void Enable()
    {
        SetState(true);
    }
    public virtual void Disable()
    {
        SetState(false);
    }
}