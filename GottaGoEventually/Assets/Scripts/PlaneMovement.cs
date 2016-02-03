using UnityEngine;
using System.Collections;

public class PlaneMovement : MonoBehaviour
{
    public int widthMoveLimit;
    public int heightMoveLimit;

    public int speed;
    public float maxZRot;
    public float maxXRot;

    public float screen2Phone = 1.27f;

    protected Vector3 ScreenToWorld(Vector3 input)
    {
        Vector3 returnVec = new Vector3();
        //float zDist = Mathf.Abs((transform.position - cam.transform.position).z);  //get the z distance from the camera to the plane

        //clamp to the edges of the screen
        input.x = Mathf.Clamp(input.x, 270f / 1153f * Screen.width, 880f / 1153f * Screen.width);
        input.y = Mathf.Clamp(input.y, 150f / 648f * Screen.height, 480/648f * Screen.height);
        returnVec.x = (((input.x / Screen.width ) * widthMoveLimit * 2) - widthMoveLimit)/ .6764f;         //offset is so 0,0 is the center of the screen
        returnVec.y = (((input.y / Screen.height) * heightMoveLimit* 2) - heightMoveLimit)/ .6764f;      //same tbh
        
        return returnVec;
    }

    void Update()
    {
        //lerp position to the new location based on the mouse/finger position
        //only move if the phone is fully raised
        if (GameManager.instance.phoneActive)
        {
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
}
