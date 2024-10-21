using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] public GameObject target;
    [SerializeField] private float smoothTime;
    private Vector3 current = Vector3.zero;

    private Transform tf;

    public Transform Tf
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position + offset;
            Tf.position = Vector3.SmoothDamp(Tf.position, targetPosition, ref current, smoothTime);
            Tf.LookAt(target.transform);

        }
    }
}
