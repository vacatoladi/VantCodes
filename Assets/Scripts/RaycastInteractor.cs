using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RaycastInteractor : MonoBehaviour
{
    public Action action;

    public GameObject Interagir;

    public Camera playerCamera;

    public DialogueSystem dialogueSystem;

    public DataController dataController;

    public PlayerController playerC;

    Outline outline;
    Interactable interactable;

    float range = 5f;

    [SerializeField] bool uiInteract;
    [SerializeField] bool alreadyChating;
    [SerializeField] bool abredo;

    bool secretariaFirstDialogue = true;

    void Start()
    {
        uiInteract = true;
    }

    void Update()
    {
        Selector();

        if (Input.GetKeyDown(KeyCode.E) && action != null)
        {
            action.Invoke();
        }
    }

    void Selector()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Campainha"))
            {
                Enable(hit);
                action = Campainha;
            }
            else if (hit.collider.CompareTag("Elevator"))
            {
                Enable(hit);
                action = Elevator;
            }
            else if (hit.collider.CompareTag("WrongElevatorNum"))
            {
                Enable(hit);
                action = WrongNumElevator;
            }
            else if (abredo)
            {
                Reseting();
            }
        }
        else if (abredo)
        {
            Reseting();
        }

    }

    void Enable(RaycastHit hit)
    {
        abredo = true;
        outline = hit.collider.gameObject.GetComponent<Outline>();
        outline.OutlineColor = new Color(1f, 1f, 1f, 1f);
        Interagir.SetActive(uiInteract);
    }

    void Reseting()
    {
        abredo = false;
        outline.OutlineColor = new Color(1f, 1f, 1f, 0f);
        Interagir.SetActive(false);
        action = null;
        interactable = null;
        outline = null;
    }

    //------------------------------------------------
    //Funções de click

    void Campainha()
    {
        Debug.Log("abriste a campainha");
        if (secretariaFirstDialogue && !alreadyChating)
        {
            alreadyChating = true;
            secretariaFirstDialogue = false;
            dataController.WhichData(1);
            dialogueSystem.Next();
            uiInteract = false;
            Interagir.SetActive(uiInteract);
            return;
        }
        else if (!alreadyChating)
        {
            alreadyChating = true;
            dataController.WhichData(2);
            dialogueSystem.Next();
            uiInteract = false;
            Interagir.SetActive(uiInteract);
        }
    }

    void Elevator()
    {

    }

    void WrongNumElevator()
    {
        if (!alreadyChating)
        {
            if (playerC.inElevator)
            {
                alreadyChating = true;
                dataController.WhichData(4);
                dialogueSystem.Next();
                uiInteract = false;
                Interagir.SetActive(uiInteract);
            }
            else
            {
                alreadyChating = true;
                dataController.WhichData(3);
                dialogueSystem.Next();
                uiInteract = false;
                Interagir.SetActive(uiInteract);
            }
        }
    }

    public void ContinueChat()
    {
        alreadyChating = false;
        uiInteract = true;
        Debug.Log("passou aqui e deixou o Chating "+alreadyChating+" e deixou o Interact "+uiInteract);
    }

}