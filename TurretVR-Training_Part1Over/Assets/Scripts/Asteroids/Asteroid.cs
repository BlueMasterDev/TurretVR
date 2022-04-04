using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float m_TargetDistanceDestroyTreshold;

    [SerializeField]
    private float m_TargetAvoidanceDistance;

    [SerializeField]
    private Vector2 m_AsteroidSpeedBounds;

    [SerializeField]
    private Mesh[] m_Meshes;

    [SerializeField]
    private MeshFilter m_MeshFilter;

    [SerializeField]
    private MeshCollider m_MeshCollider;

    [SerializeField]
    private Vector2 m_ScaleBounds;

    [SerializeField]
    private float m_HPPerScale;

    [SerializeField]
    private GameObject m_AsteroidExplosion;

    [SerializeField, ColorUsage(true, true)]
    private Color m_LowHPColor;

    private Color _startColor;

    private float _maxHP;
    private float _hp;
    private Material _mat;

    private Transform _target;
    private Vector3 _moveDirection;
    private float _speed;

    private bool _canMove = false;

    public void Setup(Transform target)
    {
        _target = target;
        _speed = Random.Range(m_AsteroidSpeedBounds.x, m_AsteroidSpeedBounds.y);

        transform.forward = (_target.position - transform.position).normalized;

        Vector2 distanceFromTarget = Random.insideUnitCircle.normalized * m_TargetAvoidanceDistance;

        Vector3 targetPosition = target.position + (distanceFromTarget.x * transform.right) + (distanceFromTarget.y * transform.up);

        _moveDirection = (targetPosition - transform.position).normalized;

        transform.eulerAngles = new Vector3(Random.Range(-360, 360),
                                            Random.Range(-360, 360),
                                            Random.Range(-360, 360));

        m_MeshFilter.mesh = m_Meshes[Random.Range(0, m_Meshes.Length)];
        m_MeshCollider.sharedMesh = m_MeshFilter.mesh;

        transform.localScale = Vector3.zero;

        float scale = Random.Range(m_ScaleBounds.x, m_ScaleBounds.y);

        _hp = m_HPPerScale * scale;
        _maxHP = _hp;

        transform.DOScale(scale, 0.75f).SetEase(Ease.InOutCubic);

        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = Instantiate(mr.material);
        _mat = mr.material;

        _startColor = _mat.GetColor("_EmissionColor");

        _canMove = true;
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.position += _moveDirection * _speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _target.position) >= m_TargetDistanceDestroyTreshold)
            {
                Kill();
            }
        }
    }

    public bool TakeDamage(float damages)
    {
        _hp -= damages;

        _mat.SetColor("_EmissionColor", Color.Lerp(m_LowHPColor, _startColor, _hp / _maxHP));

        if (_hp <= 0)
        {
            Kill(true);
            return true;
        } else
        {
            return false;
        }
    }

    public void Kill(bool fromDamage = false)
    {
        _canMove = false;

        if (fromDamage)
        {
            Instantiate(m_AsteroidExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else
        {
            transform.DOScale(0, 1.5f).SetEase(Ease.InOutCubic).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
        
    }
}
