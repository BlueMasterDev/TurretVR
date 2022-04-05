using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Valve.VR.InteractionSystem;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ExplosionPrefab;
	[SerializeField]
	private float m_Radius;
	[SerializeField]
	private float m_Timer = 2f;
	[SerializeField]
	private LayerMask asteroidLayer;

    private bool _degoupilled = false;

    public void Update()
    {
		//TODO : Si dégoupillée, explosion auto après X seconde
		if (_degoupilled)
			StartCoroutine(WaitBeforeBurst());	
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO : Si dégoupillée, explosion à l'impact
		if (_degoupilled && collision.gameObject.layer == asteroidLayer)
		{
			Boom();
		}
    }

	IEnumerator WaitBeforeBurst()
	{
		yield return new WaitForSeconds(m_Timer);
		Boom();
	}

    public void Degoupille()
    {
        _degoupilled = true;

		//Animation de clignotement lors du dégoupillage
		MeshRenderer mr = GetComponent<MeshRenderer>();
        Material[] mats = mr.materials;
        mats[1] = Instantiate(mats[1]);
        mats[1].DOColor(mats[1].GetColor("_EmissionColor") * 3f, 0.75f).SetEase(Ease.InOutBounce);
        mr.materials = mats;
    }

    public void Boom()
    {
		//TODO : Recherche des astéroïdes à portée et destruction de ces derniers
		Collider[] hitAsteroids = Physics.OverlapSphere(transform.position, m_Radius, asteroidLayer);

		foreach(Collider asteroid in hitAsteroids)
		{
			asteroid.gameObject.GetComponent<Asteroid>().Kill(true);
		}

        Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
