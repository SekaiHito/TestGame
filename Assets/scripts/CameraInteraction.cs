using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    public float interactRange = 10f; 
    public GameObject uiPrompt;

    void Start()
    {
        if (uiPrompt != null) uiPrompt.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.green);

        if (Physics.Raycast(ray, out hit, interactRange))
        {

            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                
                if (uiPrompt != null) uiPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
   
                    interactable.Interact();
                }
            }
            else
            {

                if (uiPrompt != null) uiPrompt.SetActive(false);
            }
        }
        else
        {
            if (uiPrompt != null) uiPrompt.SetActive(false);
        }
    }
}