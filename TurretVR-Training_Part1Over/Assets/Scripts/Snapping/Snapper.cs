using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class Snapper : MonoBehaviour
{
    [SerializeField]
    private GameObject m_basePreview = null;

    private Collider m_collider = null;
    private Rigidbody m_rigidbody = null;

    public bool m_isUsed = false;

    [SerializeField]
    private UnityEvent OnSnap = null;
    [SerializeField]
    private UnityEvent OnUnSnap = null;

    [SerializeField] private bool _DeactivateOnStart = true;

    private Snappable _currentSnappable = null;

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;

        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.isKinematic = true;
        m_rigidbody.useGravity = false;

        if (_DeactivateOnStart)
            TogglePreview(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSnappable != null)
        {
            if (m_isUsed)
            {
                if (this != _currentSnappable.GetSnapper())
                {
                    m_isUsed = false;
                    _currentSnappable = null;
                }
            }
            else
            {
                if (!_currentSnappable.GetAttached())
                {
                    Snap(_currentSnappable);
                }
            }
        }
        else
        {
            m_isUsed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_isUsed)
            return;

        Snappable snappable = other.GetComponent<Snappable>();

        if (snappable != null)
        {
            if (snappable.FitsIn(this))
            {
                _currentSnappable = snappable;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_isUsed)
            return;

        Snappable snappable = other.GetComponent<Snappable>();

        if (snappable != null)
        {
            if (_currentSnappable == snappable)
            {
                _currentSnappable = null;
            }
        }
    }

    public void Snap(Snappable snappable)
    {
        _currentSnappable = snappable;
        snappable.Snap(this);
        m_isUsed = true;

        snappable.transform.SetParent(transform);
        snappable.transform.DOLocalMove(Vector3.zero, 1f);
        snappable.transform.DOLocalRotate(Vector3.zero, 1f);

        OnSnap.Invoke();
    }

    public void UnSnap(Snappable snappable)
    {
        m_isUsed = false;
        snappable.UnSnap();
        _currentSnappable = null;
        snappable = null;
        OnUnSnap.Invoke();
    }

    public void TogglePreview(bool truth)
    {
        if (m_basePreview == null)
            return;
        m_basePreview.SetActive(truth);
    }

    public Snappable GetSnappedObject()
    {
        return _currentSnappable;
    }
}
 