using UnityEngine;

public class EnergyGate : MonoBehaviour
{
    [SerializeField] private int requiredEnergy = 3;

    [Header("Gate Visuals")]
    [SerializeField] private GameObject gateClosedVisual;
    [SerializeField] private GameObject gateOpenVisual;

    [SerializeField] private Collider2D gateCollider;

    private bool opened;

    private void Start()
    {
        // trạng thái ban đầu
        if (gateClosedVisual != null)
            gateClosedVisual.SetActive(true);

        if (gateOpenVisual != null)
            gateOpenVisual.SetActive(false);
    }

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

        // đổi visual
        if (gateClosedVisual != null)
            gateClosedVisual.SetActive(false);

        if (gateOpenVisual != null)
            gateOpenVisual.SetActive(true);

        // nếu muốn bỏ collider
        if (gateCollider != null)
            gateCollider.enabled = false;

        Debug.Log("Gate opened");

        GameEvents.OnPortalActivated?.Invoke();
    }
}