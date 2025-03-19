using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetKits.ParticleImage;

public class ProductParticle : MonoBehaviour
{
    private ProductType currentProductType;
    private ParticleImage particleImage;

    public void CacheComponents()
    {
        particleImage = GetComponent<ParticleImage>();
        particleImage.onParticleStop.AddListener(DisableParticle);
        particleImage.onAnyParticleFinished.AddListener(AddProduct);
    }
    public void SetParticleData(Product product, int amount, Vector3 startPos, Vector3 targetPos)
    {
        currentProductType = product.type;
        transform.position = startPos;
        transform.GetChild(0).position = targetPos;
        particleImage.sprite = product.sprite;
        particleImage.SetBurst(0, 0, amount);
        particleImage.Play();
    }

    private void AddProduct()
    {
        ResourceManager.Instance.AddProductAmount(currentProductType, 1);
    }

    private void DisableParticle()
    {
        gameObject.SetActive(false);
    }
}
