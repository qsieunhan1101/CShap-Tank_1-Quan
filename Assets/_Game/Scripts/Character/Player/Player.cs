using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick moveJoystick;
    [SerializeField] private JoystickCheckInput joystickCheckInput;
    [SerializeField] private float speed;



    [SerializeField] private FixedJoystick joystickShooting;

    //
    [SerializeField] private TrajectotyLine trajectotyLine;


    [SerializeField] private Button btnFire;


    private void Start()
    {
        joystickCheckInput = moveJoystick.GetComponent<JoystickCheckInput>();

        btnFire.onClick.AddListener(Attack);
    }
    private void Update()
    {
        Move();

        trajectotyLine.ShowTrajectoryLine(muzzle.position, muzzle.forward * shotForce / cannonBallMass);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();

        }
        if (joystickShooting.GetComponent<JoystickCheckInput>().IsTouching == true)
        {
            RotationUpperBodyAndBarrel();
            if (joystickShooting.GetComponent<JoystickCheckInput>().IsTouching == false)
            {
                Attack();
            }
        }
    }



    public void Move()
    {

        rb.velocity = new Vector3(moveJoystick.Horizontal * speed, rb.velocity.y, moveJoystick.Vertical * speed);

        Rotation();
    }

    public void Rotation()
    {
        Vector3 dir = joystickCheckInput.Direction.normalized;
        if (dir != Vector3.zero)
        {
            lowerBody.rotation = Quaternion.LookRotation(dir);

        }
    }

    private void RotationUpperBodyAndBarrel()
    {
        float rotationVertical = 15;
        float rotationHorizontal = 60;
        upperBody.Rotate(new Vector3(0, joystickShooting.Horizontal * rotationHorizontal * Time.deltaTime, 0));

        barrel.Rotate(new Vector3(-joystickShooting.Vertical * rotationVertical * Time.deltaTime, 0, 0));

        Vector3 barrelEulerAngles = barrel.rotation.eulerAngles;

        barrelEulerAngles.x = (barrelEulerAngles.x > 180) ? barrelEulerAngles.x - 360 : barrelEulerAngles.x;
        barrelEulerAngles.x = Mathf.Clamp(barrelEulerAngles.x, -30, 0);

        barrel.rotation = Quaternion.Euler(barrelEulerAngles);
    }
}
