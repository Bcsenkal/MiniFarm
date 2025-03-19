using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetKits.ParticleImage;

public class HarvestParticleHandler : MonoBehaviour
{
    [SerializeField]private ParticleImage particlePrefab;
    private List<ProductParticle> particles;
    private int poolCount = 4;
    void Awake()
    {
        CreatePool();
    }

    private void Start()
    {
        Managers.EventManager.Instance.OnShowParticle += ShowParticle;
    }

    private void CreatePool()
    {
        particles = new List<ProductParticle>();
        for (int i = 0; i < poolCount; i++)
        {
            ProductParticle particle = Instantiate(particlePrefab, transform).GetComponent<ProductParticle>();
            particle.CacheComponents();
            particle.gameObject.SetActive(false);
            particles.Add(particle);
        }
    }

    private ProductParticle GetParticle()
    {
        foreach (var particle in particles)
        {
            if (!particle.gameObject.activeInHierarchy)
            {
                return particle;
            }
        }

        ProductParticle newParticle = Instantiate(particlePrefab, transform).GetComponent<ProductParticle>();
        newParticle.CacheComponents();
        particles.Add(newParticle);
        return newParticle;
    }

    private void ShowParticle(Product product,Vector3 targetPos,int amount)
    {
        ProductParticle particle = GetParticle();
        particle.gameObject.SetActive(true);
        particle.SetParticleData(product, amount, Input.mousePosition, targetPos);
    }
}
