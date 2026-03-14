using UnityEngine;

public class EnergyInventory : MonoBehaviour
{
    [SerializeField] private int maxEnergy = 3;

    private int currentEnergy;

    public int CurrentEnergy => currentEnergy;
    public int MaxEnergy => maxEnergy;

    private void Start()
    {
        NotifyEnergyChanged();
    }

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;

        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;

        NotifyEnergyChanged();
    }

    public bool HasEnough(int required)
    {
        return currentEnergy >= required;
    }

    public void SpendEnergy(int amount)
    {
        currentEnergy -= amount;

        if (currentEnergy < 0)
            currentEnergy = 0;

        NotifyEnergyChanged();
    }

    private void NotifyEnergyChanged()
    {
        GameEvents.OnEnergyChanged?.Invoke(currentEnergy, maxEnergy);
    }
}