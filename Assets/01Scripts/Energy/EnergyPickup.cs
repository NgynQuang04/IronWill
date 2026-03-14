using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [SerializeField] private int energyAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        EnergyInventory inventory = other.GetComponent<EnergyInventory>();

        if (inventory != null)
        {
            inventory.AddEnergy(energyAmount);
        }

        Destroy(gameObject);
    }
}