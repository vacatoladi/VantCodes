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

    public void Selection()
    {
         
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

}
