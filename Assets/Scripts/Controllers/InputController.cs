using UnityEngine;

public class InputController : BaseController, IExecute
{
    private float _clickTime;
    private float _doubleClickDelta = 0.25f;

    public InputController(MainController main) : base(main)
    {

    }

    public void Execute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if ((Time.time - _clickTime) <= _doubleClickDelta)
            {
                InputEvents.Current.LeftButtonDoubleClick(Input.mousePosition);
            }
            else
            {
                InputEvents.Current.LeftButtonClick(Input.mousePosition);
            }

            _clickTime = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            InputEvents.Current.LeftButtonDrag(Input.mousePosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InputEvents.Current.RightButtonClick(Input.mousePosition);
        }
    }
}