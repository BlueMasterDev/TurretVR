using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    [SerializeField]
    private Missile m_MissilePrefab;

    [SerializeField]
    private Transform m_SpawnPoint;

    //TODO : A appeler lorsqu'on on enclenche entièrement le levier de tir
    public void Fire()
    {
        Instantiate(m_MissilePrefab, m_SpawnPoint.position, m_SpawnPoint.rotation); //PEW PEW
    }
}
