using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrenadeGenerator : MonoBehaviour
{

    [SerializeField]
    private Transform m_SpawnPoint;

    [SerializeField]
    private Grenade m_GrenadePrefab;

    [SerializeField]
    private float m_SpawnForce;

    //TODO : à appeler lors de l'appui du bouton
    public void Spawn()
    {
        Grenade g = Instantiate(m_GrenadePrefab, m_SpawnPoint.position, m_SpawnPoint.rotation);

        Rigidbody rigbod = g.GetComponent<Rigidbody>();
        rigbod.AddForce(m_SpawnPoint.forward * m_SpawnForce, ForceMode.Impulse);
        rigbod.AddRelativeTorque(Random.insideUnitSphere * m_SpawnForce * 2, ForceMode.Impulse);

        g.transform.localScale = Vector3.zero;
        g.transform.DOScale(1f, 0.75f).SetEase(Ease.OutExpo);
    }
}
