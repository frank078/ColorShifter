using UnityEngine;

public class TowerRotate : MonoBehaviour
{
    //variables
    Vector3 currentRotation;
    public float newRotation = 45;

    public float speed = 10.0f;

    // GET PLAYER HEALTH
    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        // Turn Right
        if (Input.GetKey(KeyCode.RightArrow) && playerHealth.health > 0)
        {
            currentRotation = transform.eulerAngles;
            currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y + newRotation, Time.deltaTime * speed);
            transform.eulerAngles = currentRotation;
        }

        // Turn Left
        if (Input.GetKey(KeyCode.LeftArrow) && playerHealth.health > 0)
        {
            currentRotation = transform.eulerAngles;
            currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - newRotation, Time.deltaTime * speed);
            transform.eulerAngles = currentRotation;
        }

        // Touch Controls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && playerHealth.health > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                currentRotation = transform.eulerAngles;
                currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y + newRotation, Time.deltaTime * speed);
                transform.eulerAngles = currentRotation;
            }
            if (touch.position.x < Screen.width / 2)
            {
                currentRotation = transform.eulerAngles;
                currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - newRotation, Time.deltaTime * speed);
                transform.eulerAngles = currentRotation;
            }
        }
    }
}
