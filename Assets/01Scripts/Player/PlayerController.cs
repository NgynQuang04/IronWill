using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    PlayerInput input;
    MagneticTargetDetector detector;
    MagneticMotor motor;
    PlayerRotation rotation;
    PlayerPhysicsLimiter limiter;
    MagneticTargetHighlighter highlighter;
    PlayerHealth health;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        input = GetComponent<PlayerInput>();
        detector = GetComponent<MagneticTargetDetector>();
        motor = GetComponent<MagneticMotor>();
        rotation = GetComponent<PlayerRotation>();
        limiter = GetComponent<PlayerPhysicsLimiter>();
        highlighter = GetComponent<MagneticTargetHighlighter>();
        health = GetComponent<PlayerHealth>();

    }

    void Update()
    {
        if (health != null && health.IsDead())
            return;

        input.UpdateInput();

        detector.UpdateTarget(rb.position, input.MouseDirection);

        highlighter.UpdateHighlight(detector, input);

        rotation.UpdateRotation(rb, detector, input);
    }

    void FixedUpdate()
    {
        motor.ApplyMagnetForce(
            rb,
            detector,
            input
        );

        limiter.Apply(rb);
    }
}