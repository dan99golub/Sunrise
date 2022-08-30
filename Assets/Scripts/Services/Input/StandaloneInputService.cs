using UnityEngine;

public class StandaloneInputService : InputService
{
    public override Vector2 Axis
    {
        get
        {
            Vector2 axis = SimpeInputAxis();

            if (axis == Vector2.zero)
                axis = GetUnityAxis();
            return axis;
        }
    }

    private static Vector2 GetUnityAxis()
    {
        return new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}
