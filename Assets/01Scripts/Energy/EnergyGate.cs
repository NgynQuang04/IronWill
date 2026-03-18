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
        if (gateClosedVisual != null)
            gateClosedVisual.SetActive(true);

        if (gateOpenVisual != null)
            gateOpenVisual.SetActive(false);

        if (gateCollider != null)
            gateCollider.enabled = false;

        Debug.Log("bắt đầu game");
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

        if (gateClosedVisual != null)
            gateClosedVisual.SetActive(false);

        if (gateOpenVisual != null)
            gateOpenVisual.SetActive(true);

        if (gateCollider != null)
            gateCollider.enabled = true;

        Debug.Log("Gate opened");

        GameEvents.OnPortalActivated?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!opened)
            return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.Win(); // 👈 gọi animation win
            }
        }
    }

}