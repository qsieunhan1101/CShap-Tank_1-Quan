using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private IState<Enemy> currentState;
    [SerializeField] private string currentStateName;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private Transform mapCenterPoint;

    [SerializeField] private float attackRadius;
    [SerializeField] private float checkTargetRadius;
    [SerializeField] private float mapRadius;

    [SerializeField] private LayerMask layerPlayer;

    [SerializeField] private NavMeshAgent agent;



    private float rotateVelocity;
    public float rotateSpeedMovement = 0.05f;

    public Transform EnemyTarget => enemyTarget;

    private void Start()
    {
        ChangeState(new EnemyIdleState());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack();
        }

        CheckTargetEnemy();


        if (currentState != null)
        {
            currentState.OnExecute(this);
        }


    }


    public void Move(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void StopMove()
    {
        agent.SetDestination(transform.position);
        agent.ResetPath();
    }

    public void Rotation(Vector3 lookAtPosition)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - lowerBody.position);
        float rotationY = Mathf.SmoothDampAngle(lowerBody.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

        lowerBody.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public override void Attack()
    {
        if (enemyTarget != null)
        {
            StopMove();
            shotForce = Vector3.Distance(transform.position, enemyTarget.position) * 44f;
            upperBody.rotation = Quaternion.LookRotation((enemyTarget.position - upperBody.position));
            base.Attack();

        }
    }

    public void ChangeState(IState<Enemy> newState)
    {
        currentStateName = newState.ToString();
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentStateName != null)
        {
            currentState.OnEnter(this);
        }
    }

    private void CheckTargetEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkTargetRadius, layerPlayer);
        if (colliders.Length != 0)
        {
            enemyTarget = colliders[0].transform;
        }
        else
        {
            enemyTarget = null;
        }
    }
    public bool IsTargetInAttackRange()
    {
        if (enemyTarget != null)
        {

            if (Vector3.Distance(transform.position, enemyTarget.position) <= attackRadius)
            {
                return true;
            }
        }
        return false;
    }
    public Vector3 RandomPositionInMap()
    {
        Vector3 randomPosition = Random.insideUnitSphere * mapRadius + mapCenterPoint.position;
        NavMeshHit navHit;

        bool hasHit = NavMesh.SamplePosition(randomPosition, out navHit, 1.0f, NavMesh.AllAreas);
        if (hasHit == true)
        {
            return navHit.position;
        }
        return RandomPositionInMap();
    }

    public bool IsFinishMove()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, checkTargetRadius);
        Gizmos.DrawWireSphere(transform.position, mapRadius);
    }

}
