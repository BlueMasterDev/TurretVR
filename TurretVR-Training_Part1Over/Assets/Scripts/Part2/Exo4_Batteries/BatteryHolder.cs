using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHolder : MonoBehaviour
{
	[SerializeField]
	private float m_speedCharge = -0.01f;

	public Snapper m_BatteryOne;
	public Snapper m_BatteryTwo;

	
    // Update is called once per frame
    void Update()
    {
		//TODO : Voir en dessous
		//Si il y a une ou des batteries de placées dans le réceptacle, diminuer leur charge d'un taux fixe chaque seconde.
		//Sinon, ne rien faire.

		if (m_BatteryOne.GetSnappedObject() != null)
		{
			if (m_BatteryOne.GetSnappedObject().enabled)
			{				
				m_BatteryOne.GetSnappedObject().gameObject.GetComponent<Battery>().ChangeCharge(m_speedCharge * Time.deltaTime);				
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

    //TODO : Appeler cette méthode dans TurretController pour vérifier si le réceptacle possède des piles avec de l'énergie
    public bool HasEnergy()
    {
        //TODO : Vérifier si il y a des piles, et si oui, si il y reste de l'énergie.
        //Si il n'y a pas de piles ou qu'elles sont vides, renvoyer faux.
		if (m_BatteryOne || m_BatteryTwo)
		{
			if (m_BatteryOne.GetSnappedObject().gameObject.GetComponent<Battery>().GetCharge() > 0 || 
				m_BatteryTwo.GetSnappedObject().gameObject.GetComponent<Battery>().GetCharge() > 0)
				return true;
		}
		 
		return false;
    }
}

