using UnityEngine;

public class EnergyCore : MonoBehaviour
{
    [SerializeField] private int energyValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        Debug.Log("Player collected energy core!");
        EnergyInventory inventory = other.GetComponent<EnergyInventory>();

        if (inventory == null)
            return;

        inventory.AddEnergy(energyValue);

        Destroy(gameObject);
    }
}