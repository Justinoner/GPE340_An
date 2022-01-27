using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn pawn;
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        //make sure cam is loaded
        if (playerCamera == null) Debug.LogWarning("Error: No cam set!");
    }

    // Update is called once per frame
    void Update()
    {
        //send move command to pawn
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Limit the vector magnitude to 1
        moveVector = Vector3.ClampMagnitude(moveVector, 1.0f);
        // Tell the pawn to move
        pawn.Move(moveVector);
        //Rotate player to face mouse
        RotateToMouse();

    }
    void RotateToMouse(){
        //Create a plane object(mathematical reperesentaion of all points in 2d)
        Plane groundPlane;

        //set plane so it is the X,Z plane the player is standing on
        groundPlane = new Plane(Vector3.up, pawn.transform.position);
        //cast ray from our camera toward the plane. through our mouse cursor
        float distance;
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        groundPlane.Raycast(cameraRay, out distance);
        //find where that ray hits the plane
        Vector3 raycastPoint = cameraRay.GetPoint(distance);
        //rotate towards that point
        pawn.RotateTowards(raycastPoint);
    }
}
