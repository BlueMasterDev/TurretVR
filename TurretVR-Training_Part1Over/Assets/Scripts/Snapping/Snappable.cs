using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Throwable))]
public class Snappable : MonoBehaviour
{

    [SerializeField]
    private List<Snapper> m_fittingSnappers;

    [SerializeField]
    private UnityEvent OnSnap;
    [SerializeField]
    private UnityEvent OnUnSnap;

    private bool m_isSnapped = false;

    private Snapper m_snapper = null;

    private bool m_isAttached = false;

    private Interactable _interactable;

    private Throwable _throwable;

    private Rigidbody _rigbod;

    // Start is called before the first frame update
    void Start()
    {
        _interactable = GetComponent<Interactable>();
        _rigbod = GetComponent<Rigidbody>();
        _throwable = GetComponent<Throwable>();

        _throwable.onDetachFromHand.AddListener(delegate
        {
            _rigbod.isKinematic = false;
        });
    }

    public bool FitsIn(Snapper snapper)
    {
        if (m_fittingSnappers == null || m_fittingSnappers.Count == 0)
            return true;

        return m_fittingSnappers.Contains(snapper);
    }

    public void ToggleSnappers(bool truth)
    {
        m_fittingSnappers.ForEach(x => {
            if (!x.m_isUsed)
                x.TogglePreview(truth);
        });
    }

    public void OnAttachedToHand()
    {
        m_isAttached = true;

        if (!m_isSnapped)
            ToggleSnappers(true);
        else
        {
            m_snapper?.UnSnap(this);
        }
    }


    private void OnDetachedFromHand()
    {
        m_isAttached = false;
        ToggleSnappers(false);
    }

    public void Snap(Snapper snapper)
    {
        _interactable.attachedToHand?.DetachObject(gameObject, false);

        m_isSnapped = true;
        m_snapper = snapper;

        _rigbod.isKinematic = true;

        OnSnap.Invoke();
    }

    public void UnSnap()
    {
        if (m_isAttached)
        {
            ToggleSnappers(true);
        }

        m_snapper = null;
        m_isSnapped = false;

        OnUnSnap.Invoke();
    }

    public bool GetAttached()
    {
        return m_isAttached;
    }

    public Snapper GetSnapper()
    {
        return m_snapper;
    }
}
