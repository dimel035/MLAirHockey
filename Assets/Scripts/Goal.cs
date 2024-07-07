using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Vector3 resetPosition;
    [SerializeField] private GameObject puck;

    private Rigidbody puckRigidbody;

    private void Awake()
    {
        puckRigidbody = puck.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puck"))
        {
            if (gameObject.name == "BlueGoal")
            {
                ScoreManager.Instance.AddRedPoint();
            }
            else if (gameObject.name == "RedGoal")
            {
                ScoreManager.Instance.AddBluePoint();
            }
            ResetPuck();
        }
    }

    private void ResetPuck()
    {
        puck.transform.position = resetPosition;
        puckRigidbody.velocity = Vector3.zero;
        puckRigidbody.angularVelocity = Vector3.zero;
    }
}
