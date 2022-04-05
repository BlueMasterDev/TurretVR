using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretController : MonoBehaviour
{
    #region General Behaviour
    private Asteroid _current;

    [SerializeField]
    private float m_Cooldown;
    private float _LastFire;

    [SerializeField]
    private float m_DamagesPerSecond;

    [SerializeField]
    private Transform m_TurretHead;

    [SerializeField]
    private Transform m_TurretLasers;

    [SerializeField]
    private float m_MaxFiringDistance;

    [SerializeField, Range(0f, 1f)]
    private float m_TurrretSmoothing = 0.9f;

    [SerializeField]
    private Light m_HoveringLight;

    [SerializeField]
    private Vector2 m_HoveringLightIntensityBounds;

    [SerializeField]
    private Vector2 m_TurretHeadHeightBounds;

    [SerializeField]
    private Transform m_TurretHeadContainer;

    private bool _isHovering = false;

    private bool _isOn = false;

    private void Start()
    {
        _LastFire = Time.time - m_Cooldown;
        m_TurretLasers.gameObject.SetActive(false);

        SetHovering(false);
        SetOn(false);
    }

    public void ToggleHovering()
    {
        SetHovering(!_isHovering);
    }

    public void SetHovering(bool hovering)
    {
        if (_isHovering && _isOn) //Prevent sensors toggling while on
            return;

        _isHovering = hovering;

        //Move head
        m_TurretHeadContainer.DOKill();
        m_TurretHeadContainer.DOLocalMoveY(hovering ? m_TurretHeadHeightBounds.y : m_TurretHeadHeightBounds.x, 1.25f).SetEase(Ease.InOutQuad);

        //Set light intensity
        m_HoveringLight.DOKill();
        m_HoveringLight.DOIntensity(hovering ? m_HoveringLightIntensityBounds.y : m_HoveringLightIntensityBounds.x, 1.25f).SetEase(Ease.InOutBounce);
    }

    public void SetOn(bool on)
    {
        _isOn = on;

        if (!_isOn)
        {
            //Resting position
            m_TurretHead.DOKill();
            m_TurretHead.DOLookAt(m_TurretHead.position + transform.forward, m_Cooldown * 0.5f);
        }

        //Ensure turret is reset
        StopFiring();
    }

    private void Update()
    {
        if (!CanFire())
        {
            m_TurretLasers.gameObject.SetActive(false);
            return;
        }

        if(_current == null) //If no target has been acquired
        {
            if (Time.time - _LastFire >= m_Cooldown) //If cooldown is over
            {
                Fire(); //Seek for target
            }
        } else
        {
            if (Vector3.Dot(Vector3.up, (_current.transform.position - transform.position).normalized) < 0)
            {
                StopFiring();
                return;
            }

            m_TurretHead.rotation = Quaternion.Lerp(m_TurretHead.rotation, Quaternion.LookRotation((_current.transform.position - m_TurretHead.transform.position).normalized), 1 - m_TurrretSmoothing);

            m_TurretLasers.localScale = new Vector3(m_TurretLasers.localScale.x, m_TurretLasers.localScale.y, Vector3.Distance(m_TurretLasers.position, _current.transform.position) * 0.5f);

            if(Vector3.Angle(m_TurretHead.forward, (_current.transform.position - m_TurretHead.position).normalized) <= 5f)
            {
                if (_current.TakeDamage(m_DamagesPerSecond * Time.deltaTime))
                {
                    StopFiring();
                }
            }
        }
    }

    //Seek for target
    public void Fire()
    {
        Asteroid bestAsteroid = null;
        float bestDistance = Mathf.Infinity;

        foreach(Asteroid a in FindObjectsOfType<Asteroid>()) //Going through every asteroid (not best performance, but does the trick for this game) and search for the best target
        {
            float distance = Vector3.Distance(transform.position, a.transform.position);
            if (distance <= m_MaxFiringDistance) //Is the asteroid in range ?
            {
                if(Vector3.Dot(Vector3.up, (a.transform.position - transform.position).normalized) > 0) //Is the asteroid visible by the turret ? (A simple Y position check could have done the trick, but the Dot product is more flexible). 
                                                                                                        //With the dot product, if we wanted to put the turret upside down under the platform, it would still work.
                {
                    if (distance < bestDistance) //A better asteroid than the previous best has been found
                    {
                        bestAsteroid = a;
                        bestDistance = distance;
                    }
                }
            }
        }

        _current = bestAsteroid;

        if(_current != null) //If an asteroid has been found
        {
            m_TurretHead.DOKill(); //Reset turret head movement
            m_TurretLasers.gameObject.SetActive(true); //Activate lasers
        }
    }

    public void StopFiring() //Stop firing, for whatever reason
    {
        _current = null;
        _LastFire = Time.time;
        m_TurretLasers.gameObject.SetActive(false);
        m_TurretHead.DOKill();
        m_TurretHead.DOLookAt(m_TurretHead.position + transform.forward, m_Cooldown * 0.5f);
    }

    public void SetTarget(Asteroid target)
    {
        _current = target;

        if (_current != null) //If an asteroid has been found
        {
            m_TurretHead.DOKill(); //Reset turret head movement
            m_TurretLasers.gameObject.SetActive(true); //Activate lasers
        }
    }

    #endregion

    #region TO COMPLETE
    //TODO : Add missing fields
    [SerializeField]
    private Snapper[] m_Snappers;

	[SerializeField]
	private BatteryHolder m_batteryHolder;

    //TODO : Complete method
    public bool CanFire() //Check if everything is ok for firing
    {
        foreach(Snapper snapper in m_Snappers)
        {
            if (!snapper.m_isUsed || snapper.GetSnappedObject().gameObject.CompareTag("Defective") || m_batteryHolder.HasEnergy() == false)
            {
                return false;
            }
        }

        return _isHovering && _isOn;
    }
    #endregion

}
