using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 _rotator;

    [SerializeField]
    private Vector2 m_RotationSpeedRange;

    // Start is called before the first frame update
    void Start()
    {
        _rotator = new Vector3(Random.Range(m_RotationSpeedRange.x, m_RotationSpeedRange.y),
                                                Random.Range(m_RotationSpeedRange.x, m_RotationSpeedRange.y),
                                                Random.Range(m_RotationSpeedRange.x, m_RotationSpeedRange.y));

        transform.eulerAngles = new Vector3(Random.Range(-360, 360),
                                            Random.Range(-360, 360),
                                            Random.Range(-360, 360));
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(_rotator * Time.deltaTime);
    }
}
