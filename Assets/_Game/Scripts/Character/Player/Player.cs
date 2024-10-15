using UnityEngine;

public class Player : Character
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick moveJoystick;
    [SerializeField] private JoystickCheckInput joystickCheckInput;
    [SerializeField] private float speed;

    [SerializeField] private Transform lowerBody;



    [SerializeField] private VariableJoystick joystickUpperBody;
    [SerializeField] private VariableJoystick joystickBarrel;
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform barrel;


    //
    [SerializeField] private TrajectotyLine trajectotyLine;

    [SerializeField] private GameObject cannonBall;

    [SerializeField] private Transform muzzle;

    [SerializeField, Min(1)] private float cannonBallMass = 30;

    [SerializeField, Min(1)] private float shotForce = 30;




    private void Start()
    {
        joystickCheckInput = moveJoystick.GetComponent<JoystickCheckInput>();
    }
    private void Update()
    {
        Move();

        trajectotyLine.ShowTrajectoryLine(muzzle.position, muzzle.forward * shotForce / cannonBallMass);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ball = Instantiate(cannonBall);
            ball.transform.position = muzzle.position;
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.mass = cannonBallMass;
            rb.AddForce(muzzle.forward * shotForce, ForceMode.Impulse);

        }
        RotationUpperBody();
        RotationBarrel();
    }



    protected override void Move()
    {

        rb.velocity = new Vector3(moveJoystick.Horizontal * speed, rb.velocity.y, moveJoystick.Vertical * speed);

        Rotation();
    }

    protected override void Rotation()
    {
        Vector3 dir = joystickCheckInput.Direction.normalized;
        lowerBody.rotation = Quaternion.LookRotation(dir);
    }

    private void RotationUpperBody()
    {
        upperBody.Rotate(new Vector3(0, joystickUpperBody.Horizontal, 0));
    }

    private void RotationBarrel()
    {
        float ro = 15;

        barrel.Rotate(new Vector3(joystickBarrel.Vertical * ro * Time.deltaTime, 0, 0));
      
        Vector3 barrelEulerAngles = barrel.rotation.eulerAngles;

        barrelEulerAngles.x = (barrelEulerAngles.x > 180) ? barrelEulerAngles.x - 360 : barrelEulerAngles.x;
        barrelEulerAngles.x = Mathf.Clamp(barrelEulerAngles.x, 0, 30);

        barrel.rotation = Quaternion.Euler(barrelEulerAngles);
    }
}
