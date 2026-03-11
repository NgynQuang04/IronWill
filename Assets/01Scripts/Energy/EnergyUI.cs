using UnityEngine;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;

    private void Start()
    {
        EnergyEvents.OnEnergyChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        EnergyEvents.OnEnergyChanged -= UpdateUI;
    }

    void UpdateUI(int energy)
    {
        energyText.text = "Energy: " + energy;
    }
}