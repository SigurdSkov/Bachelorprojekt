using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;


//Nej... Måske denne?
//Forhold dig til:
//Måske rytme?
//Hvor langt har du bevæget dig siden sidste tjek (hastighed)?
//Er du under et threshold (er du faktisk ved at dukke dig, og ikke bare ved at kigge ned) - (Squatcheck)?
//Hvad er vinklen på kameraet?
//Hvis man er roteret underligt er man måske ved at bukke sig, og ikke ved at lave et squat?
//Man bevæger sig når man er ved at rejse sig op, ikke når man er ved at dukke sig.
//Når man dukker sig sættes issquatting til true, hvis man ikke rejser sig op (eller begynder oprejsning), så går issquatting til false.
//Så er rytmen ikke i orden.
//Nu giver det måske mening at have en runmode (hvor man bevæger sig når man går ned, forsat man lige er kommet ud af et squat)?
//Bevægelse skal helst være i den retning man kigger, men ikke rotationsmæssigt... Gå ud at man altid kigger ligeud ifht. det horisontale plan.


public class SquatjumpUp : MonoBehaviour
{
    private const int PositionsSize = 10; //Size of queue
    private Queue<Vector3> positions = new Queue<Vector3>(); //queue of saved positions

    private double timeCounter = 0; //Count up
    private double sendRate = 4000; // save position every 4000 milliseconds (4 seconds)
    private float highestHigh = float.MinValue;
    //private float lowestLow = float.MaxValue;
    private float SquatThreshold = 0.35f; // 0.2 units reduction in height is considered a squat
    //private float squatLength = 1f; // length of squat motion
    //private float squatDist = 0f; // distance moved during a squat
    //private bool IsSquatting = false; // flag to indicate if currently in a squat
    private float PreviousCameraPositionY = 0;
    //private bool MovingUp = false;
    private bool SquatStatus = false;
    Transform CameraTransform;

    [SerializeField]
    private float speed = 100;

    //MATHIAS HJÆLP
    Rigidbody body;

    private float CurrentPos;

    //float rotationX;

    private bool Grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        PreviousCameraPositionY = CameraTransform.transform.localPosition.y;
        body = GetComponent<Rigidbody>();
        body.drag = 0;
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Grounded)
        {
            UpdateHighestHigh();
        }
        //body.AddForce(Vector3.forward*speed, ForceMode.Force);
        SquatCheck(SquatStatus);
        //SavePosition();
        if (SquatStatus) //Føltes som om man kunne rykke disse kald ud, så de sker sjælnere. Ikke sikker på om jeg bør være bange for at de bliver kaldt for ofte
        {
            if (MoveUpCheck())
            {
                //Indsæt en counter her, gang addforce med counteren - lowest low
            }
            if (highestHigh - PreviousCameraPositionY < 0.1 && highestHigh - PreviousCameraPositionY > -0.1) //Check if ~max position is reached. Teknisk set muligt at skippe, hvis man bevæger sig 20cm på en frame... Derfor er "else" fjernet
            {
                body.AddForce((CameraTransform.up*2) * speed, ForceMode.Impulse);//(3000-timeCounter)
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
        CurrentPos = CameraTransform.localPosition.y;
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