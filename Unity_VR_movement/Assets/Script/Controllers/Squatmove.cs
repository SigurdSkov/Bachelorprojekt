using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Squatmove : MonoBehaviour
{
    private const int PositionsSize = 10; //Size of queue
    private Queue<Vector3> positions = new Queue<Vector3>(); //queue of saved positions

    private double timeCounter = 0; //Count up
    private double sendRate = 4000; // save position every 4000 milliseconds (4 seconds)
    private float highestHigh = float.MinValue;
    private float SquatThreshold = 0.35f; // 0.2 units reduction in height is considered a squat
    private float PreviousCameraPositionY = 0;
    Transform CameraTransform;

    [SerializeField]
    private float speed = 10;
    Rigidbody body;

    private float CurrentPos;
    void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        PreviousCameraPositionY = CameraTransform.transform.localPosition.y;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighestHigh();
        bool SquatStatus = SquatCheck();
        if (SquatStatus) //Føltes som om man kunne rykke disse kald ud, så de sker sjælnere. Ikke sikker på om jeg bør være bange for at de bliver kaldt for ofte
        {
            if (MoveUpCheck())
            {
                Vector3 forward = CameraTransform.forward;
                forward.y = 0;
                body.AddForce(forward * speed, ForceMode.Force);
                //Husk at sætte et hegn rundt om der hvor man burde kunne bevæge sig

            }
            //BLIVER ALDRIG KALDT
            if (highestHigh - PreviousCameraPositionY < 0.1 && highestHigh - PreviousCameraPositionY > -0.1) //Check if ~max position is reached. Teknisk set muligt at skippe, hvis man bevæger sig 20cm på en frame... Derfor er "else" fjernet
            {
                SquatStatus = false;
                highestHigh = float.MinValue;
            }
        }

        PreviousCameraPositionY = CameraTransform.transform.position.y; //Gemmes her, efter alle tjekkene

        //Timer
        if (SquatStatus)
        {
            timeCounter += Time.deltaTime * 1000;
            if (timeCounter > sendRate)
            {
                SquatStatus = false;
                highestHigh = float.MinValue;
                timeCounter = 0;
            }
        }
    }

    void UpdateHighestHigh()
    {
        CurrentPos = CameraTransform.localPosition.y;
        if (CurrentPos > highestHigh)
            highestHigh = CurrentPos;
    }

    bool MoveUpCheck() //Checks if position is more than the previous position. Tilføjede 2 centimeter, bare så man ikke flyver 1 meter, fordi man bevægede sig 3 millimeter op. That allows MoveUp. 
    {
        bool MovingUp;
        if (CameraTransform.transform.position.y > /*Overvej om dette skal være en double*/ PreviousCameraPositionY + 0.001 && IsLookingProper()) //+0.02 er en mere elegant løsning, end at lave et lowest low check, til at se om er på bunden... Faktisk... Begge kunne virke... En større deadzone ved minimumhøjden
            MovingUp = true;
        else
            MovingUp = false;
        return MovingUp; //Kan laves om til en float, hvilket bliver en modifier for hvor hurtigt man squatter
    }

    bool SquatCheck() //Checks if you're doing a squat
    {
        if (CurrentPos < highestHigh - SquatThreshold && IsLookingProper()) //Tjek om den er indenfor thresholdet, og rotationen er indenfor 90 grader (45 op, 45 ned)
        {
            return true;
        }
        return false;
    }

    bool IsLookingProper() //Checks where you're looking
    {
        float CurrentRotation = CameraTransform.eulerAngles.x;
        if ((CurrentRotation > 0 && CurrentRotation < 45) || (CurrentRotation < 360 && CurrentRotation > 315))
        { return true; }
        else return false;
    }
}