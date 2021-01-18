using UnityEngine;

public class TowerRotate : MonoBehaviour
{
    //variables
    Vector3 currentRotation;
    public float newRotation = 45;

    public float speed = 10.0f;

    private GameObject thePlayer;

    void Start()
    {
        thePlayer = GameManager.Instance.GetPlayer();
    }

    void Update()
    {
        if (thePlayer == null)
        {
            thePlayer = GameManager.Instance.GetPlayer();
        }

        // Turn Right
        if (Input.GetKey(KeyCode.RightArrow) && thePlayer != null)
        {
            RotateLeftTower();
            EnableTowerMoving();
        }

        // Turn Left
        if (Input.GetKey(KeyCode.LeftArrow) && thePlayer != null)
        {
            RotateRightTower();
            EnableTowerMoving();
        }

        // Touch Controls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && thePlayer != null)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                RotateLeftTower();
                EnableTowerMoving();
            }
            if (touch.position.x < Screen.width / 2)
            {
                RotateRightTower();
                EnableTowerMoving();
            }
        }
    }

    void RotateRightTower()
    {
        currentRotation = transform.eulerAngles;
        currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - newRotation, Time.deltaTime * speed);
        transform.eulerAngles = currentRotation;
    }
    void RotateLeftTower()
    {
        currentRotation = transform.eulerAngles;
        currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y + newRotation, Time.deltaTime * speed);
        transform.eulerAngles = currentRotation;
    }

    void EnableTowerMoving()
    {
        Timer.isArrowPressed = true;
    }
}
