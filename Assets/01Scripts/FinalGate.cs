using UnityEngine;

public class FinalGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameEvents.OnGameWin?.Invoke();
        Debug.Log(" player đã qua cổng");
    }
}