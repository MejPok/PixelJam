using UnityEngine;

public class FootPositioner : MonoBehaviour
{
    // reference to player character object
    public GameObject playerObj;

    // reference to IK target
    public Transform target;

    // reference to the other foot
    public FootPositioner otherFoot;

    public bool isBalanced;

    // used to lerp the foot from its current position to target position
    public float lerp;

    // the start and end position of a step
    private Vector3 startPos;
    private Vector3 endPos;

    // how far should we anticipate a step
    public float overShootFactor = 0.8f;

    // how fast the foot moves
    public float stepSpeed = 3f;

    // the foot's displacement from body center on the X axis
    public float footDisplacementOnX = 0.25f;

    private void Start()
    {
        startPos = endPos = target.position;
    }

    private void Update()
    {
        UpdateBalance();

        // if the body is not balanced AND this foot has finished its previous step (we don't want to calculate new steps in the process of moving a foot)
        if (!isBalanced && lerp > 1)
        {
            CalculateNewStep();
        }

        // using ease in/ease out value will make the animation look more natural
        float easedLerp = EaseInOutCubic(lerp);

        target.position = Vector3.Lerp(startPos, endPos, easedLerp);
        lerp += Time.deltaTime * stepSpeed;

        // this foot can only move when: (1) the other foot finishes moving, (2) the other foot made the last step
        bool thisFootCanMove = otherFoot.lerp > 1 && lerp > otherFoot.lerp;

        // if the body is not balanced AND this foot has finished its previous step (we don't want to calculate new steps in the process of moving a foot)
        if (!isBalanced && lerp > 1 && thisFootCanMove)
        {
            CalculateNewStep();
        }

    }
    private float EaseInOutCubic(float x)
    {
        return 1f / (1 + Mathf.Exp(-10 * (x - 0.5f)));
    }

    private void CalculateNewStep()
    {
        // set starting position
        startPos = target.position;

        // this will make the foot start moving to its target position starting from next frame
        lerp = 0;

        // find where the foot should land without considering overshoot
        RaycastHit2D ray = Physics2D.Raycast(playerObj.transform.position + new Vector3(footDisplacementOnX, 0, 0), Vector2.down, 10);

        // consider the overshoot factor
        Vector3 posDiff = ((Vector3)ray.point - target.position) * (1 + overShootFactor);

        // find end target position
        endPos = target.position + posDiff;
    }

 

    private void UpdateBalance()
    {
        // get center of mass in world position
        float centerOfMass = playerObj.transform.position.x;
        // if center of mass is between two feet, the body is balanced
        isBalanced = IsFloatInRange(centerOfMass, target.position.x, otherFoot.target.position.x);
    }

    /// <summary>
    /// returns true if "value" is between "bound1" and "bound2"
    /// </summary>
    bool IsFloatInRange(float value, float bound1, float bound2)
    {
        float minValue = Mathf.Min(bound1, bound2);
        float maxValue = Mathf.Max(bound1, bound2);
        return value > minValue && value < maxValue;
    }

}
