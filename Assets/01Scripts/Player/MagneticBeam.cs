using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MagneticBeam : MonoBehaviour
{
    [Header("Beam Origin")]
    [SerializeField] Transform beamOrigin;

    MagneticTargetDetector detector;
    PlayerInput playerInput;

    LineRenderer line;

    [Header("Colors")]
    [SerializeField] Color pullColor = Color.cyan;
    [SerializeField] Color pushColor = Color.red;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        detector = GetComponentInParent<MagneticTargetDetector>();
        playerInput = GetComponentInParent<PlayerInput>();

        line.enabled = false;

    }

    void Update()
    {
        if (!detector.HasTarget)
        {   
            line.enabled = false;
            return;
        }

        line.enabled = true;

        line.SetPosition(0, beamOrigin.position);
        line.SetPosition(1, detector.CurrentHit.point);

        UpdateColor();
    }

    void UpdateColor()
    {
        if (playerInput.Pull)
        {
            SetColor(pullColor);
        }
        else if (playerInput.Push)
        {
            SetColor(pushColor);
        }
        else
        {
            SetColor(Color.white);
        }
    }

    void SetColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }
}