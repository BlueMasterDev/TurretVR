using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private float m_SpawnDistance;

    [SerializeField]
    private float m_SpawnRate;

    [SerializeField]
    private Transform m_Target;

    private float _lastAsteroid;

    [SerializeField]
    private Asteroid m_AsteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _lastAsteroid = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _lastAsteroid >= (1f / m_SpawnRate))
        {
            _lastAsteroid = Time.time;
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        Asteroid aster = Instantiate(m_AsteroidPrefab, transform);

        aster.transform.position = m_Target.position + Random.insideUnitSphere.normalized * m_SpawnDistance;
        
        aster.Setup(m_Target);
    }
}
