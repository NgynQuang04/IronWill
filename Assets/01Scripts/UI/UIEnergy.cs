using UnityEngine;
using TMPro;

public class UIEnergy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyText;

    void Start()
    {
        GameEvents.OnEnergyChanged += UpdateUI;
    }

    void OnDestroy()
    {
        GameEvents.OnEnergyChanged -= UpdateUI;
    }

    void UpdateUI(int current, int max)
    {
        energyText.text = current + " / " + max;
    }
}