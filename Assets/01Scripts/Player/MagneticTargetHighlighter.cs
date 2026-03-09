using UnityEngine;

public class MagneticTargetHighlighter : MonoBehaviour
{
    public Color highlightColor = Color.cyan;
    public float highlightScale = 1.15f;

    private Transform currentTarget;
    private Vector3 originalScale;
    private SpriteRenderer targetSprite;


    public void UpdateHighlight(MagneticTargetDetector detector)
    {
        if(!detector.HasTarget)
        {
            ClearHighlight();
            return;
        }

        Transform target = detector.CurrentHit.collider.transform;

        if(currentTarget == target)
        {
            return;
        }

        ClearHighlight();

        currentTarget = target;

        targetSprite = target.GetComponentInChildren<SpriteRenderer>();

        originalScale = target.localScale;

        currentTarget.localScale = originalScale * highlightScale;
        targetSprite.color = highlightColor;
    }

    void ClearHighlight()
    {
        if(currentTarget == null)
        {
            return;
        }

        if(targetSprite != null)
        {
            targetSprite.color = Color.white;
        }

        currentTarget.localScale = originalScale;

        currentTarget = null;
        targetSprite = null;
    }

}
