using TMPro;
using UnityEngine;
using System.Collections;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;

    int currentEnergy;
    int maxEnergy;

    Vector3 originalScale;

    Coroutine bounceRoutine;

    void Awake()
    {
        originalScale = Vector3.one; // luôn dùng scale chuẩn
    }

    void OnEnable()
    {
        GameEvents.OnEnergyChanged += UpdateEnergy;
    }

    void OnDisable()
    {
        GameEvents.OnEnergyChanged -= UpdateEnergy;
    }

    void UpdateEnergy(int current, int max)
    {
        currentEnergy = current;
        maxEnergy = max;

        UpdateUI();

        if (bounceRoutine != null)
            StopCoroutine(bounceRoutine);

        bounceRoutine = StartCoroutine(PlayBounce());
    }

    void UpdateUI()
    {
        energyText.text = currentEnergy + " / " + maxEnergy;
    }

    IEnumerator PlayBounce()
    {
        energyText.transform.localScale = originalScale * 1.2f;

        yield return new WaitForSeconds(0.1f);

        energyText.transform.localScale = originalScale;
    }
}