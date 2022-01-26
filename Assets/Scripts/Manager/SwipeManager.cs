using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector2 swipeDelta, swipeDeltaTemp, startTouch;
    public Vector3 difference;
    private Touch touch;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position;
            }

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                swipeDeltaTemp.x = touch.position.x;
                swipeDelta.x = swipeDeltaTemp.x - startTouch.x;
                difference = swipeDelta / Screen.width;
            }
        }
    }
}
