using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    public RaycastInteractor interactor;

    public DialogueSystem dialogueSystem;

    public DataController dataController;

    public PlayerController playerC;

    public GameObject InteragirUI;

    bool secretariaFirstDialogue = true;

    public bool uiInteract = true;
    bool alreadyChating;
    
    public enum Object
    {
        Campainha,
        Elevador,
        WrongNumElevator,
    }


    public Object actualObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Tem();
        }
    }

    public void Selection()
    {
        Debug.Log("abriste a Selecao");
        switch (actualObject)
        {
            case Object.Campainha:
                interactor.action = Campainha;
                break;
            case Object.Elevador:
                interactor.action = Elevator;
                break;
            case Object.WrongNumElevator:
                interactor.action = WrongNumElevator;
                break;
        }
    }

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
            InteragirUI.SetActive(uiInteract);
        }
        else if (!alreadyChating)
        {
            alreadyChating = true;
            dataController.WhichData(2);
            dialogueSystem.Next();
            uiInteract = false;
            InteragirUI.SetActive(uiInteract);
        }
    }

    void Elevator()
    {
        Debug.Log("abriste o Elevator");
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
                InteragirUI.SetActive(uiInteract);
            }
            else
            {
                alreadyChating = true;
                dataController.WhichData(3);
                dialogueSystem.Next();
                uiInteract = false;
                InteragirUI.SetActive(uiInteract);
            }
        }

    }

    public void ContinueChat()
    {
        alreadyChating = false;
        uiInteract = true;
    }

    public void Tem()
    {
        
        switch (actualObject)
        {
            case Object.Campainha:
                Debug.Log("campainha está " + uiInteract);
                break;
            case Object.WrongNumElevator:
                Debug.Log("wrong esta " + uiInteract);
                break;
        }
    }

}
