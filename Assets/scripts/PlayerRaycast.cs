using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float interactRange = 3f;
    public GameObject uiPrompt;

    private Interactable currentTarget;
    void Start()
    {
            if (uiPrompt != null)
            {
                uiPrompt.SetActive(false);
            }
    }

    void Update()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 1f, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                currentTarget = interactable;
                if (uiPrompt != null)
                {
                    uiPrompt.SetActive(true);
                    // Optionally set prompt text here using currentTarget.GetPromptText()
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentTarget.Interact();
                }
            }
            else
            {
                ClearTarget();
            }
        }
        else
        {
            ClearTarget();
        }
    }

    void ClearTarget()
    {
        currentTarget = null;
        if (uiPrompt != null)
        {
            uiPrompt.SetActive(false);
        }
    }
}
