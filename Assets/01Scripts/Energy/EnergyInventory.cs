using UnityEngine;

public class EnergyInventory : MonoBehaviour
{
    [SerializeField] private int energy;

    public int Energy => energy;

    public void AddEnergy(int amount)
    {
        energy += amount;
        Debug.Log("Energy added. Current energy: " + energy);

        EnergyEvents.OnEnergyChanged?.Invoke(energy);
    }

    public bool HasEnough(int required)
    {
        return energy >= required;
    }

    public void SpendEnergy(int amount)
    {
        energy -= amount;

        if (energy < 0)
            energy = 0;

        EnergyEvents.OnEnergyChanged?.Invoke(energy);
    }
}