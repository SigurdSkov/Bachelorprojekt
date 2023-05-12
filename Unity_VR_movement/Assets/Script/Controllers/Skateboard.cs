using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skateboard : MonoBehaviour
{
    Transform CameraTransform;
    Rigidbody body;
    float speed = 0f;
    //float turnSpeed = 0;
    float leanForward;
    float leanSide;
    float turnDegrees;
    Vector3 moveDirection;
    GameObject skateBoard;
    bool inverted;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;
        body = GetComponent<Rigidbody>();
        skateBoard = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //skateBoard = Instantiate(cube);
        Transform cam = transform;//.GetChild(0);//.GetChild(0).GetChild(0); //The camera
        skateBoard.transform.parent = cam; //The camera
        //skateBoard.transform.localPosition = new Vector3(0,-1.5f,0);
        skateBoard.transform.position = new Vector3(cam.position.x, 0.1f, cam.position.z);
        skateBoard.transform.localScale = new Vector3(0.2f, 0.01f, 0.6f);
        skateBoard.transform.parent = transform;
        skateBoard.transform.rotation = new Quaternion (0,0,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leanForward = CameraTransform.localPosition.z;
        leanSide = CameraTransform.localPosition.x;

        if (leanForward > 0.1F || leanForward < -0.1F)
        {
            if (leanForward < 0)
                inverted = true;
            else
                inverted = false;
            speed = Mathf.Abs(leanForward) * 50;
        }
        else
            speed = 0;

        if (leanSide > 0.1F || leanSide < -0.1F) //Alternativet er jo at man bruger armene. Anden løsning krævet. Hvis man vender sig om er disse controls inverteret
        { 
            //turnSpeed = leanSide * 50;
            turnDegrees = leanSide * 5;
        }
        else
            turnDegrees = 0;

        //body.velocity += CameraTransform.forward * speed * Time.deltaTime;
        moveDirection = transform.forward;
        //moveDirection.y = 0;
        if (body.velocity.magnitude < 10)
            if (inverted)
                body.AddForce(-moveDirection * speed, ForceMode.Acceleration);
            else
                body.AddForce(moveDirection * speed, ForceMode.Acceleration);
        //body.AddForce(moveDirection * turnSpeed, ForceMode.Acceleration);
        if (inverted)
            transform.Rotate(new Vector3(0, -turnDegrees, 0));
        else
            transform.Rotate(new Vector3(0, turnDegrees, 0));
        Debug.Log("Forward: " + speed + "sides: " + leanSide);
    }

    private void OnDestroy()
    {
        Destroy(skateBoard);
    }
}
