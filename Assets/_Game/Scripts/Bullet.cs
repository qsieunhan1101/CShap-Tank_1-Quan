using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform oner;
    [SerializeField] private ParticleSystem explodedParticle;
    [SerializeField] private Transform bulletBody;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float timeDestroy;
    [SerializeField] private float explededRadius;

    public Transform Oner { get => oner; set => oner = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }

    void Start()
    {

        Destroy(gameObject, timeDestroy);
    }

    void Update()
    {
        if (Rb.velocity != Vector3.zero)
        {
            Quaternion rota = Quaternion.LookRotation(Rb.velocity);
            bulletBody.rotation = rota;

        }
    }

    private void ButtletExploded()
    {
        Rb.velocity = Vector3.zero;
        Rb.useGravity = false;
        bulletBody.gameObject.SetActive(false);
        explodedParticle.gameObject.SetActive(true);
        Destroy(gameObject, 0.5f);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") == false)
        {
            if (other.transform != oner)
            {
                ButtletExploded();
                Debug.Log(other.name);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explededRadius);
    }
}
