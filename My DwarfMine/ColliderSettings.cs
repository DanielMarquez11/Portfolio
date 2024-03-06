using UnityEngine;

public class ColliderSettings : MonoBehaviour
{
    [Header("Front Wheels")] public WheelCollider[] frontWheels;
    public float frontWheelMass;
    public float frontWheelRadius;

    [Header("Back Wheels")] public WheelCollider[] backWheels;
    public float backWheelMass;
    public float backWheelRadius;

    void Start()
    {
        ApplyNormalSettings();
    }

    void SetWheelSettings(WheelCollider[] wheels, float mass, float radius /*, other parameters*/)
    {
        foreach (var wheel in wheels)
        {
            wheel.mass = mass;
            wheel.radius = radius;
            // set other common wheel parameters
            
            // Suspension Spring
            JointSpring suspensionSpring = wheel.suspensionSpring;
            suspensionSpring.spring = 17500f; // Adjust the spring stiffness as needed
            suspensionSpring.damper = 2250f; // Adjust the damper as needed
            suspensionSpring.targetPosition = 0.5f; // Adjust the target position as needed
            wheel.suspensionSpring = suspensionSpring;
            // Set forward friction
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.extremumSlip = 0.4f;
            forwardFriction.extremumValue = 1f;
            forwardFriction.asymptoteSlip = 0.8f;
            forwardFriction.asymptoteValue = 0.5f;
            forwardFriction.stiffness = 1f;
            wheel.forwardFriction = forwardFriction;

            // Set sideways friction
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.extremumSlip = 0.2f;
            sidewaysFriction.extremumValue = 1f;
            sidewaysFriction.asymptoteSlip = 0.5f;
            sidewaysFriction.asymptoteValue = 0.75f;
            sidewaysFriction.stiffness = 1f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }
    public void ApplyNormalSettings()
    {
        SetWheelSettings(frontWheels, frontWheelMass, frontWheelRadius /*, other front wheel parameters*/);
        SetWheelSettings(backWheels, backWheelMass, backWheelRadius /*, other back wheel parameters*/);
        // Additional settings for normal driving
    }

    public void ApplyDriftSettings()
    {
        SetWheelSettings(frontWheels, frontWheelMass, frontWheelRadius /*, other front wheel parameters*/);
        SetWheelSettings(backWheels, backWheelMass, backWheelRadius /*, other back wheel parameters*/);

        // Adjust settings for drift mode
        foreach (var wheel in frontWheels)
        {
            // set other common wheel parameters
            
            // Suspension Spring
            JointSpring suspensionSpring = wheel.suspensionSpring;
            suspensionSpring.spring = 17500.0f; // Adjust the spring stiffness as needed
            suspensionSpring.damper = 1500.0f; // Adjust the damper as needed
            suspensionSpring.targetPosition = 0.5f; // Adjust the target position as needed
            wheel.suspensionSpring = suspensionSpring;
            // Set forward friction
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.extremumSlip = 1.4f;
            forwardFriction.extremumValue = 1f;
            forwardFriction.asymptoteSlip = 2.8f;
            forwardFriction.asymptoteValue = 2.5f;
            forwardFriction.stiffness = 1f;
            wheel.forwardFriction = forwardFriction;

            // Set sideways friction
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.extremumSlip = 2.2f;
            sidewaysFriction.extremumValue = 1f;
            sidewaysFriction.asymptoteSlip = 1.8f;
            sidewaysFriction.asymptoteValue = 0.75f;
            sidewaysFriction.stiffness = 1f;
            wheel.sidewaysFriction = sidewaysFriction;
        }

        foreach (var wheel in backWheels)
        {
            // set other common wheel parameters
            
            // Suspension Spring
            JointSpring suspensionSpring = wheel.suspensionSpring;
            suspensionSpring.spring = 17500.0f; // Adjust the spring stiffness as needed
            suspensionSpring.damper = 1500.0f; // Adjust the damper as needed
            suspensionSpring.targetPosition = 0.5f; // Adjust the target position as needed
            wheel.suspensionSpring = suspensionSpring;
            // Set forward friction
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.extremumSlip = 1.4f;
            forwardFriction.extremumValue = 1f;
            forwardFriction.asymptoteSlip = 2.8f;
            forwardFriction.asymptoteValue = 2.5f;
            forwardFriction.stiffness = 1f;
            wheel.forwardFriction = forwardFriction;

            // Set sideways friction
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.extremumSlip = 2.2f;
            sidewaysFriction.extremumValue = 1f;
            sidewaysFriction.asymptoteSlip = 1.8f;
            sidewaysFriction.asymptoteValue = 0.75f;
            sidewaysFriction.stiffness = 1f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }
}