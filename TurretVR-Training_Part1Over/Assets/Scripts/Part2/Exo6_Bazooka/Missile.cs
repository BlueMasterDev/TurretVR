using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO : Faire exploser le missile après quelques secondes de vol si il ne touche rien.
//TODO : Faire exploser le missile à l'impact d'une surface quelconque
public class Missile : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private GameObject m_ExplosionPrefab;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * m_Speed, ForceMode.Impulse);
    }

    private void Update()
    {

    }

    //TODO : Ajouter la détection d'astéroïdes dans la zone d'effet pour les détruire
    public void Kaboom()
    {
        Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
