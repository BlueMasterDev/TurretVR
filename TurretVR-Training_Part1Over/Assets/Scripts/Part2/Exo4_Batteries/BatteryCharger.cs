using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCharger : MonoBehaviour
{
	[SerializeField]
	private float m_speedCharge = 0.01f;

	public Snapper m_BatteryOne;
	public Snapper m_BatteryTwo;
	public bool isActive = false;

	// Update is called once per frame
	void Update()
    {
		//TODO : Voir en dessous
		//Si il y a une ou des batteries de placées dans le chargeur, augmenter leur charge d'un taux fixe chaque seconde.
		//Sinon, ne rien faire.

		if (m_BatteryOne.GetSnappedObject() != null)
		{
			if (m_BatteryOne.GetSnappedObject().enabled)
			{
				
				m_BatteryOne.GetSnappedObject().gameObject.GetComponent<Battery>().ChangeCharge(m_speedCharge * Time.deltaTime);
				isActive = m_BatteryOne.GetSnappedObject().enabled;
			}
			else
				return;
		}
		

		if (m_BatteryTwo.GetSnappedObject() != null)
		{
			if (m_BatteryTwo.GetSnappedObject().enabled)
			{
				
				m_BatteryTwo.GetSnappedObject().gameObject.GetComponent<Battery>().ChangeCharge(m_speedCharge * Time.deltaTime);
				
			}
			else
				return;
		}
		
	}
}
