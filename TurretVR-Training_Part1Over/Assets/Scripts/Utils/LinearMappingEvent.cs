using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class LinearMappingEvent : MonoBehaviour
{
    [SerializeField]
    private LinearMapping m_LinearMapping;

    [SerializeField]
    private UnityEvent m_OnMin;

    [SerializeField]
    private UnityEvent m_OnMax;

    private float _lastValue;

    // Start is called before the first frame update
    void Start()
    {
        _lastValue = m_LinearMapping.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(_lastValue > 0.05f && _lastValue < 0.95f)
        {
            if(m_LinearMapping.value < 0.05f)
            {
                m_OnMin.Invoke();
            } else if(m_LinearMapping.value > 0.95f)
            {
                m_OnMax.Invoke();
            }
        }

        _lastValue = m_LinearMapping.value;
    }
}
