using UnityEngine;

public class EnergyGate : MonoBehaviour
{
    [SerializeField] private int requiredEnergy = 3;

    [SerializeField] private GameObject gateVisual;
    [SerializeField] private Collider2D gateCollider;

    private bool opened;

    private void OnEnable()
    {
        GameEvents.OnEnergyChanged += CheckGate;
    }

    private void OnDisable()
    {
        GameEvents.OnEnergyChanged -= CheckGate;
    }

    private void CheckGate(int currentEnergy, int maxEnergy)
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

        /*if (gateCollider != null)
            gateCollider.enabled = false;*/

        Debug.Log("Gate opened");

        GameEvents.OnPortalActivated?.Invoke();
    }
}