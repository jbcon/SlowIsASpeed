using UnityEngine;
using System.Collections;

public class PlaneMovement : MonoBehaviour
{
    public int widthMoveLimit;
    public int heightMoveLimit;

    public int speed;
    public float maxZRot;
    public float maxXRot;

    protected Vector3 ScreenToWorld(Vector3 input)
    {
        Vector3 returnVec = new Vector3();
        returnVec.x = ((input.x / Screen.width) * widthMoveLimit * 2) - widthMoveLimit;         //offset is so 0,0 is the center of the screen
        returnVec.y = ((input.y / Screen.height) * heightMoveLimit * 2) - heightMoveLimit;      //same tbh
        return returnVec;
    }

    void Update()
    {
        //lerp position to the new location based on the mouse/finger position
        Vector3 goal = ScreenToWorld(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * speed);

        //rotate the plane based on the speed and direction you're moving
        Vector3 dir = goal - transform.position;
        float newZ = Mathf.Clamp(-dir.x * maxZRot, -maxZRot, maxZRot);
        float newX = Mathf.Clamp(-dir.y * maxXRot, -maxXRot, maxXRot);
        Quaternion goalRot = Quaternion.Euler(newX, transform.rotation.y, newZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * 2);
    }
}
