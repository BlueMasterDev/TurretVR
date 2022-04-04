using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneObject : MonoBehaviour
{
    private Vector3 _startPosition;

    private Quaternion _startRotation;

    private Rigidbody _rigidBody;
    
    [SerializeField]
    private float m_MaxDistance;
    
    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, _startPosition) >= m_MaxDistance)
        {
            //Reset position

            if(_rigidBody != null)
            {
                _rigidBody.velocity = Vector3.zero;
                _rigidBody.angularVelocity = Vector3.zero;
            }

            transform.position = _startPosition;

            transform.rotation = _startRotation;
        }
    }
}
