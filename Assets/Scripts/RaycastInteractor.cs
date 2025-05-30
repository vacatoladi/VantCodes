using System;
using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    public Action action;

    public GameObject Interagir;

    public Camera playerCamera;

    Transform newTransform;
    Transform oldTransform;

    Outline outline;
    Interactable interactable;

    public float range = 0f;

    public bool thatUiInteract = true;
    bool abredo;
    bool noMoreFirstTime;

    void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range) && hit.collider.CompareTag("Interagir"))
        {
            newTransform = hit.collider.gameObject.transform;
            if (newTransform != oldTransform && noMoreFirstTime)
            {
                outline.OutlineColor = new Color(1f, 1f, 1f, 0f);
                oldTransform = newTransform;
            }
            else
            {
                noMoreFirstTime = true;
            }
            abredo = true;
            outline = hit.collider.gameObject.GetComponent<Outline>();
            interactable = hit.collider.gameObject.GetComponent<Interactable>();
            outline.OutlineColor = new Color(1f,1f,1f,1f);
            //outline.OutlineWidth = 5;
            //outline.enabled = true;
            interactable.Selection();
            thatUiInteract = interactable.uiInteract;
            Interagir.SetActive(thatUiInteract);
            if (Input.GetKeyDown(KeyCode.E))
            {
                action?.Invoke();
            }
        }
        else if (abredo)
        {
            Reseting();
        }
    }

    void Reseting()
    {
        abredo = false;
        outline.OutlineColor = new Color(1f, 1f, 1f, 0f);
        //outline.OutlineWidth = 0;
        //outline.enabled = false;
        //thatUiInteract = false;
        Interagir.SetActive(false);
        action = null;
        interactable = null;
        outline = null;
    }

}