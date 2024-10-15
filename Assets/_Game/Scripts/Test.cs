using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private TrajectotyLine trajectotyLine;

    [SerializeField] private GameObject cannonBall;

    [SerializeField] private Transform muzzle;

    [SerializeField, Min(1)] private float cannonBallMass = 30;

    [SerializeField, Min(1)] private float shotForce = 30;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trajectotyLine.ShowTrajectoryLine(muzzle.position, muzzle.forward * shotForce/cannonBallMass);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ball = Instantiate(cannonBall);
            ball.transform.position = muzzle.position;
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.mass = cannonBallMass;
            rb.AddForce(muzzle.forward * shotForce, ForceMode.Impulse);

        }
    }
}
