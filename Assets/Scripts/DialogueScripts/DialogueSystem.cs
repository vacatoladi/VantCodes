// using NUnit.Framework;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Enum para criar 3 estados do dialogo, (Digitando, Desativado e Esperando)
public enum STATE
{
    DISABLED,
    WAITING,
    TYIPING
}

public class DialogueSystem : MonoBehaviour
{
    // Importanto o Enum
    STATE state;

    // Importando outros scripts para usar algumas fun��es aqui
    public DialogueData dataActual;
    public DataController dataController;
    public DialogueUI dialogueUI;
    public TypeAnimation typetext;

    //Variaveis para confirmar se o texto acabou ou n�o
    int currentText = 0;
    bool finished = false;

    // Objetos que ficam dentro da UI
    public GameObject enter;
    public GameObject interact;


    // Variaveis para ativa��o do dialogo coisas com collider trigger ou outro meio

    public RaycastInteractor raycastInteractor;

    // Variaveis para completar miss�o, deve ser usado na fun��o Next()

    //public bool crystalReward = false;
    //public GameObject crystal;

    
    void Awake()
    {
        //Aqui adicionamos a fun��o OnTypeFinished() a variavel Action TypeFinished do script TypeAnimation
        typetext.TypeFinished = OnTypeFinished;
    }

    void Start()
    {
        // Aqui setamos o estado como desativado logo ao iniciar o jogo
        state = STATE.DISABLED;
    }

    void Update()
    {
        // Aqui caso o estado seja Desativado ele cancela todos os scripts abaixo
        if (state == STATE.DISABLED) return;

        // aqui faz um Switch case para cada um dos estados
        switch (state)
        {
            //Caso ja tenha terminado a anima��o de digita��o ele inicia a fun��o Waiting();
            case STATE.WAITING:
                Waiting();
                break;
            //Caso ainda esteja digitando ele inicia a fun��o Typing()
            case STATE.TYIPING:
                Typing();
                break;
        }

    }

    // Fun��o que ser� ativada para iniciar ou ir para a proxima linha do dialogo
    public void Next()
    {

        enter.SetActive(true);

        if (currentText == 0)
        {
            dialogueUI.Enable();
        }

        dialogueUI.SetName(dataActual.talkScript[currentText].name);

        typetext.fullText = dataActual.talkScript[currentText].text;

        if (currentText == dataActual.talkScript.Count - 1)
        {
            finished = true;
        }

        //if (currentText == 5 && crystalReward)
        //{
        //    crystal.SetActive(true);
        //    crystalReward = false;
        //}

        currentText++;
        state = STATE.TYIPING;
        typetext.StartAnimation();

    }

    // Fun��o que transforma o estado do dialogo em Esperando
    void OnTypeFinished()
    {
        state = STATE.WAITING;
    }

    //Fun~�ao que � ativada a todo momento em que o dialogo esta em estado de Espera
    void Waiting()
    {
        // Aqui verificamos se o bot�o Enter foi pressionado
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Aqui checamos se o dialogo ja acabou (se essa ultima linha de dialogo, foi a ultima da Data), caso ainda tenha texto para ser exibido aqui iniciar� o proximo 
            if (!finished)
            {
                Next();
                state = STATE.TYIPING;
            }
            // Caso ja tenha acabado o dialogo, aqui tudo voltar� ao estado que estava antes do dialogo ser iniciado, preparando para vir o proximo ou para o player voltar a jogar tranquilamente
            else
            {
                enter.SetActive(false);
                dialogueUI.Disable();
                state = STATE.DISABLED;
                currentText = 0;
                finished = true; // SLA EIN-----------------------------------------------------------
                interact.SetActive(false);

                // EXEMPLOS DO QUE TER� QUE SER FEITO!!!

                // Isso serve para enviar para o player que o dialogo atual ja acabou e la dentro ele vai selecionar um dialogo novo

                //player.NextDialog();

                // Isso serve para poder avisar que o dialogo acabou, fazendo reativar algumas func�es nesses scripts

                raycastInteractor.ContinueChat();
                
            }
        }
    }

    //fun��o que � ativada a todo momento em que estamos esperando a anima��o de digita��o acabar
    void Typing()
    {
        //Verificamos se o bot�o Enter foi pressionado
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Ativa a fun��o que pula a anima��o de digita��o e mostra o texto completo de uma s� vez
            typetext.Skip();

            //Transforma o estado do dialogo em Esperando
            state = STATE.WAITING;
        }
    }


    // Fun��o que ser� ativada caso o player saia do range do dialogo, cancelando tudo e reiniciando para caso o player decida voltar e falar denovo
    public void CancelDialogue()
    {
        enter.SetActive(false);
        dialogueUI.Disable();
        state = STATE.DISABLED;
        currentText = 0;

        // EXEMPLOS DO QUE TER� QUE SER FEITO!!!

        // Isso serve para poder avisar que o dialogo acabou, fazendo reativar algumas func�es nesses scripts

        //player.ContinueChat();
        //portal.ReEleger();
    }

    //Fun��o que ser� ativada em DataController, ela receber� a proxima Data que ser� utilizada no ChatBox
    public void Which(DialogueData data)
    {
        dataActual = data;
    }

    //Fun��o que eu tinha feito para reativar o codigo de uma maneira externa, mas deixei de usar por algum motivo, um dia irei lembrar, MANTENHA ELA AQUI MESMO SEM TER REFERECIA NENHUMA
    public void Reiniciate()
    {
        finished = false;
    }

}
