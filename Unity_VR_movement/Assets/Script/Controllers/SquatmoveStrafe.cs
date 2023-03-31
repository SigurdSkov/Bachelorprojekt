using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;


//Nej... M�ske denne?
//Forhold dig til:
//M�ske rytme?
//Hvor langt har du bev�get dig siden sidste tjek (hastighed)?
//Er du under et threshold (er du faktisk ved at dukke dig, og ikke bare ved at kigge ned) - (Squatcheck)?
//Hvad er vinklen p� kameraet?
//Hvis man er roteret underligt er man m�ske ved at bukke sig, og ikke ved at lave et squat?
//Man bev�ger sig n�r man er ved at rejse sig op, ikke n�r man er ved at dukke sig.
//N�r man dukker sig s�ttes issquatting til true, hvis man ikke rejser sig op (eller begynder oprejsning), s� g�r issquatting til false.
//S� er rytmen ikke i orden.
//Nu giver det m�ske mening at have en runmode (hvor man bev�ger sig n�r man g�r ned, forsat man lige er kommet ud af et squat)?
//Bev�gelse skal helst v�re i den retning man kigger, men ikke rotationsm�ssigt... G� ud at man altid kigger ligeud ifht. det horisontale plan.


public class SquatmoveStrafe : MonoBehaviour
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

    Transform CameraTransform;

    [SerializeField]
    private float speed = 10;

    //MATHIAS HJ�LP
    Rigidbody body;

    private float CurrentPos;

    private bool right = true;

    //float rotationX;

    // Start is called before the first frame update
    void Start()
    {
        //while (positions.Count != PositionsSize)
        //{
        //    Vector3 TempVector = Vector3.zero; //this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).localPosition; //new Vector3(0, 0, 0);
        //    positions.Enqueue(TempVector);
        //}
        
        CameraTransform = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
        PreviousCameraPositionY = CameraTransform.transform.localPosition.y;
        //Reference til rotationX = CameraTransform.eulerAngles.x;

        //MATHIAS! HJ�LP!
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighestHigh();
        //body.AddForce(Vector3.forward*speed, ForceMode.Force);
        bool SquatStatus = SquatCheck();
        //SavePosition();
        if (SquatStatus) //F�ltes som om man kunne rykke disse kald ud, s� de sker sj�lnere. Ikke sikker p� om jeg b�r v�re bange for at de bliver kaldt for ofte
        {
            if (MoveUpCheck())
            {
                Debug.Log("Moving");
                //transform.position = Vector3.Lerp(transform.localPosition, transform.localPosition + CameraTransform.forward, 0.5f); //Find ud af hvor langt, og find ogs� ud af hvor l�nge. Det skal v�re mere smooth end det er lige nu
                //GetComponent<Rigidbody>().velocity = Vector3.forward*Time.deltaTime;
                Vector3 forward = CameraTransform.right;
                if (!right)
                {
                    forward = forward * -1;
                    right = true;
                }
                else
                    right = false;
                forward.y = 0;
                body.AddForce(forward*speed, ForceMode.Force);
            }
            //BLIVER ALDRIG KALDT. Fordi squatstatus stopper, og s� bliver dette aldrig tjekket
            if (highestHigh - PreviousCameraPositionY < 0.1 && highestHigh - PreviousCameraPositionY > -0.1) //Check if ~max position is reached. Teknisk set muligt at skippe, hvis man bev�ger sig 20cm p� en frame... Derfor er "else" fjernet
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

    bool MoveUpCheck() //Checks if position is more than the previous position. Tilf�jede 2 centimeter, bare s� man ikke flyver 1 meter, fordi man bev�gede sig 3 millimeter op. That allows MoveUp. 
    {
        bool MovingUp;
        if (CameraTransform.transform.position.y > /*Overvej om dette skal v�re en double*/ PreviousCameraPositionY+0.0005 && IsLookingProper()) //+0.02 er en mere elegant l�sning, end at lave et lowest low check, til at se om er p� bunden... Faktisk... Begge kunne virke... En st�rre deadzone ved minimumh�jden
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