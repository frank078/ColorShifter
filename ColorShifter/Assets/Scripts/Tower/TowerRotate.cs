using UnityEngine;

public class TowerRotate : MonoBehaviour
{
    //variables
    Vector3 currentRotation;
    public float newRotation = 45;

    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // Turn Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation = transform.eulerAngles;
            currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y + newRotation, Time.deltaTime * speed);
            transform.eulerAngles = currentRotation;
        }

        // Turn Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation = transform.eulerAngles;
            currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - newRotation, Time.deltaTime * speed);
            transform.eulerAngles = currentRotation;
        }
    }
}
