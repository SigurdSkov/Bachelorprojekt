using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;


public class Squatjump : MonoBehaviour
{ 
    private double timeCounter = 0;
    private double sendRate = 4000;
    private float highestHigh = float.MinValue;
    private float SquatThreshold = 0.25f;
    private float PreviousCameraPositionY = 0;
    private bool SquatStatus = false;
    Transform CameraTransform;


    private float speed = 10;


    Rigidbody body;

    private float CurrentPos;

    private bool Grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        PreviousCameraPositionY = CameraTransform.transform.position.y;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grounded)
        {
            UpdateHighestHigh();
        }
        SquatCheck(SquatStatus);
        if (SquatStatus) //Føltes som om man kunne rykke disse kald ud, så de sker sjælnere. Ikke sikker på om jeg bør være bange for at de bliver kaldt for ofte
        {
            if (MoveUpCheck())
            {
                //Indsæt en counter her, gang addforce med counteren - lowest low
            }
            if (highestHigh - PreviousCameraPositionY < 0.1 && highestHigh - PreviousCameraPositionY > -0.1) //Check if ~max position is reached. Teknisk set muligt at skippe, hvis man bevæger sig 20cm på en frame... Derfor er "else" fjernet
            {
                body.AddForce((CameraTransform.up + CameraTransform.forward) * speed, ForceMode.Impulse);
                Debug.Log(this.GetType().ToString() + ": Force added");
                SquatStatus = false;
                highestHigh = float.MinValue;
            }
        }

        PreviousCameraPositionY = CameraTransform.transform.position.y; //Gemmes her, efter alle tjekkene

        //Timer
        if (SquatStatus)
        {
            //Timer skal sættes markant ned
            timeCounter += Time.deltaTime * 1000;
            if (timeCounter > sendRate)
            {
                SquatStatus = false;
                highestHigh = float.MinValue;
                timeCounter = 0;
            }
        }
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Ground") //Ad det er en hacky løsning
        {
            Debug.Log("Grounded");
            Grounded = true;
        }
    }
    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Ground") //Ad det er en hacky løsning
        {
            Grounded = false;
        }
    }

    void UpdateHighestHigh()
    {
        CurrentPos = CameraTransform.position.y;
        if (CurrentPos > highestHigh)
            highestHigh = CurrentPos;
    }

    bool MoveUpCheck() //Checks if position is more than the previous position. Tilføjede 2 centimeter, bare så man ikke flyver 1 meter, fordi man bevægede sig 3 millimeter op. That allows MoveUp. 
    {
        bool MovingUp;
        if (CameraTransform.transform.position.y > /*Overvej om dette skal være en double*/ PreviousCameraPositionY + 0.0005 && IsLookingProper()) //+0.02 er en mere elegant løsning, end at lave et lowest low check, til at se om er på bunden... Faktisk... Begge kunne virke... En større deadzone ved minimumhøjden
            MovingUp = true;
        else
            MovingUp = false;
        return MovingUp; //Kan laves om til en float, hvilket bliver en modifier for hvor hurtigt man squatter
    }

    void SquatCheck(bool AlreadySquatting) //Checks if you're doing a squat
    {
        if(!AlreadySquatting)
        {
            if (CurrentPos < highestHigh - SquatThreshold && IsLookingProper()) //Tjek om den er indenfor thresholdet, og rotationen er indenfor 90 grader (45 op, 45 ned)
            {
                SquatStatus = true;
            }
        }
    }

    bool IsLookingProper() //Checks where you're looking
    {
        float CurrentRotation = CameraTransform.eulerAngles.x;
        if ((CurrentRotation > 0 && CurrentRotation < 45) || (CurrentRotation < 360 && CurrentRotation > 315))
        { return true; }
        else return false;
    }
}