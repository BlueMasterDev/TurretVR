using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField]
    private bool m_AutoStart = false;

    [SerializeField]
    private Vector2 m_LocalHeightBounds;

    [SerializeField]
    private float m_HoverSpeed;

    private float _progression;

    private bool _playing = false;

    private float _startTime;

    // Start is called before the first frame update
    void Start()
    {
        if (m_AutoStart)
        {
            Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playing)
        {
            _progression = (Mathf.Sin((Time.time - _startTime) * m_HoverSpeed) + 1) * 0.5f; //0 => 1 => 0 => 1 and so on
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(m_LocalHeightBounds.x, m_LocalHeightBounds.y, _progression), transform.localPosition.z);
        }
    }

    public void Toggle()
    {
        if (_playing)
        {
            Stop();
        } else
        {
            Play();
        }
    }

    public void Play()
    {
        _startTime = Time.time;
        _playing = true;
    }

    public void Stop()
    {
        _playing = false;
        transform.localPosition = new Vector3(transform.localPosition.x, m_LocalHeightBounds.x, transform.localPosition.z);
    }
}
