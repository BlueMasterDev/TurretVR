using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverchargeTurret : MonoBehaviour
{
	[SerializeField]
	private TurretController m_TurretController;

	private float m_MinTurretDamage;
	private float m_MaxTurretDamage = 150f;

    // Start is called before the first frame update
    void Start()
    {
		m_MinTurretDamage = m_TurretController.DamagesPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
		m_TurretController.DamagesPerSecond = Mathf.Clamp(m_TurretController.DamagesPerSecond, m_MinTurretDamage, m_MaxTurretDamage);
    }

	public void SetOnMaximum()
	{
		m_TurretController.DamagesPerSecond = m_MaxTurretDamage;
	}

	public void SetOnMinimum()
	{
		m_TurretController.DamagesPerSecond = m_MinTurretDamage;
	}
}
