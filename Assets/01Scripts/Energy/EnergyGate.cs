using UnityEngine;

public class EnergyGate : MonoBehaviour
{
    [SerializeField] private int requiredEnergy = 3;

    [SerializeField] private GameObject gateVisual;
    [SerializeField] private Collider2D gateCollider;

    private bool opened;

    private void Start()
    {
        EnergyEvents.OnEnergyChanged += CheckGate;
    }

    private void OnDestroy()
    {
        EnergyEvents.OnEnergyChanged -= CheckGate;
    }

    private void CheckGate(int currentEnergy)
    {
        if (opened)
            return;

        if (currentEnergy >= requiredEnergy)
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        opened = true;

        if (gateVisual != null)
            gateVisual.SetActive(false);

        if (gateCollider != null)
            gateCollider.enabled = false;
    }
}