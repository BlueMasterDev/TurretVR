using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHolder : MonoBehaviour
{
	[SerializeField]
	private TurretController m_TurretController;

	public Battery m_BatteryOne;
	public Battery m_BatteryTwo;

	
    // Update is called once per frame
    void Update()
    {
        //TODO : Voir en dessous
        //Si il y a une ou des batteries de placées dans le réceptacle, diminuer leur charge d'un taux fixe chaque seconde.
        //Sinon, ne rien faire.
    }

    //TODO : Appeler cette méthode dans TurretController pour vérifier si le réceptacle possède des piles avec de l'énergie
    public bool HasEnergy()
    {
        //TODO : Vérifier si il y a des piles, et si oui, si il y reste de l'énergie.
        //Si il n'y a pas de piles ou qu'elles sont vides, renvoyer faux.
        return false;
    }
}
