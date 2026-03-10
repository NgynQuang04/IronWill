using UnityEngine;

public class MagneticTargetHighlighter : MonoBehaviour
{
    public Color pullColor = Color.cyan;
    public Color pushColor = Color.red;
    public float highlightScale = 1.15f;

    private Transform currentTarget;
    private Vector3 originalScale;
    private SpriteRenderer targetSprite;
    private Color originalColor;


    public void UpdateHighlight(MagneticTargetDetector detector, PlayerInput playerInput)
    {
        if(!detector.HasTarget)
        {
            ClearHighlight();
            return;
        }

        Transform target = detector.CurrentHit.collider.transform;

        // nếu target mới
        if(currentTarget != target )
        {
            ClearHighlight();

            currentTarget = target;
            targetSprite = target.GetComponentInChildren<SpriteRenderer>();

            if (targetSprite == null) return;

            originalColor = targetSprite.color;
            originalScale = targetSprite.transform.localScale;

            targetSprite.transform.localScale = originalScale * highlightScale;
        }

        // cập nhật màu theo input
        if (playerInput.Pull)
        {
            targetSprite.color = pullColor;
        }
        else if (playerInput.Push)
        {
            targetSprite.color = pushColor;
        }
        else
        {
            targetSprite.color = originalColor;
        }

    }

    void ClearHighlight()
    {
        if (currentTarget == null)
            return;

        if (targetSprite != null)
        {
            targetSprite.color = originalColor;
            targetSprite.transform.localScale = originalScale;
        }

        currentTarget = null;
        targetSprite = null;
    }

}
