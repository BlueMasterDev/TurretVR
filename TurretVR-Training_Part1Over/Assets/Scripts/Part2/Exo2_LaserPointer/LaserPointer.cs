using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_Laser;
	[SerializeField]
	private TurretController m_turretController;

    private Asteroid _currentAsteroid;
    
    void Update()
    {
		//TODO : les commentaires en dessous
		//Utiliser un raycast pour détecter ce qui se trouve devant le pointeur
		//Il faudra alors que le laser pointe sur ce qu'on vise (qu'il ne traverse pas un mur !)
		//Puis notifier éventuellement la tourelle pour lui dire de tirer sur la cible
		//Attention, ne pas appeler plusieurs fois de suite le changement de cible pour une même cible, sinon la tourelle ne tirera pas.
		int layerMask = 1 << 9;
		RaycastHit hit;
		if (Physics.Raycast(m_Laser.transform.position, m_Laser.transform.forward, out hit, layerMask))
		{
			//Debug.Log(hit.transform.name);
			if (_currentAsteroid != hit.transform.GetComponent<Asteroid>())
			{
				Debug.Log("Asteroid found !");
				_currentAsteroid = hit.transform.GetComponent<Asteroid>();
				m_turretController.SetTarget(_currentAsteroid);
			}			
		}
	}

    //Si on active le pointeur, on active le visuel du laser aussi
    public void OnEnable()
    {
        m_Laser.gameObject.SetActive(true);
    }

    //Si on désactive le pointeur, on désactive le visuel du laser aussi
    public void OnDisable()
    {
        m_Laser.gameObject.SetActive(false);
    }
}
