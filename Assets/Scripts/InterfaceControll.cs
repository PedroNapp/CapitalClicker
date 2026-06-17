using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceControll : MonoBehaviour
{
    public static InterfaceControll instancia {get; private set;}
    [Header("Telas do Jogo")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuDificuldade;
    [SerializeField] private GameObject cenaPrincipal;
    [SerializeField] private GameObject vidaPessoal;
    [SerializeField] private GameObject estudos;
    [SerializeField] private GameObject descanso;
    [SerializeField] private GameObject trabalho;
    [SerializeField] private GameObject carreira;
    [SerializeField] private GameObject popUpInfo;
    [SerializeField] private GameObject holderTelasDeFinalizacao;
    [SerializeField] private GameObject vitoria;
    [SerializeField] private GameObject derrotaSanidade;
    [SerializeField] private GameObject derrotaVida;
    [SerializeField] private GameObject popUpConfirmarSaida;

    [Header("Elementos de Interface e Sons")]
    [SerializeField] private GameObject displayStatus;
    [SerializeField] private SoundControler soundControler;

    private int id_tela_atual = 0; 
    // 0 -> Menu Principal | 1 -> Menu Seleção de Dificuldade
    // 2 -> Cena principal, Apartir daqui o display de status e ativo
    // | 3 -> vidaPessoal | 4 -> estudos | 5 -> descanso 
    // | 6 -> trabalho | 7 -> carreira | 8 -> vitoria | 9 -> derrotaSanidade 
    // 10 -> derrotaVida | 11 -> popUpConfirmarSaida

    void Awake()
    {
        if(instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        menuPrincipal.SetActive(true);
        menuDificuldade.SetActive(false);
        cenaPrincipal.SetActive(false);
        displayStatus.SetActive(false);
        vidaPessoal.SetActive(false);
        estudos.SetActive(false);
        descanso.SetActive(false);
        trabalho.SetActive(false);
        carreira.SetActive(false);
        popUpInfo.SetActive(false);
        vitoria.SetActive(false);
        derrotaSanidade.SetActive(false);
        derrotaVida.SetActive(false);
        popUpConfirmarSaida.SetActive(false);
        holderTelasDeFinalizacao.SetActive(false);
    }

    public void TrocarTela()
    {
        if(id_tela_atual == 0)
        {
            menuPrincipal.SetActive(true);
            menuDificuldade.SetActive(false);
            displayStatus.SetActive(false);
            vitoria.SetActive(false);
            derrotaSanidade.SetActive(false);
            cenaPrincipal.SetActive(false);
            derrotaVida.SetActive(false);
            holderTelasDeFinalizacao.SetActive(false);
            popUpConfirmarSaida.SetActive(false);
        } 
        
        else if(id_tela_atual == 1)
        {
            menuPrincipal.SetActive(false);
            menuDificuldade.SetActive(true);
            cenaPrincipal.SetActive(false);
            displayStatus.SetActive(false);
        } 
        
        else if(id_tela_atual == 2)
        {
            menuPrincipal.SetActive(false);
            menuDificuldade.SetActive(false);
            cenaPrincipal.SetActive(true);
            displayStatus.SetActive(true);
            vidaPessoal.SetActive(false);
            trabalho.SetActive(false);
            vitoria.SetActive(false);
            derrotaSanidade.SetActive(false);
            derrotaVida.SetActive(false);
            holderTelasDeFinalizacao.SetActive(false);
            popUpConfirmarSaida.SetActive(false);
        } 
        
        else if(id_tela_atual == 3)
        {
            cenaPrincipal.SetActive(false);
            vidaPessoal.SetActive(true);
            estudos.SetActive(false);
            descanso.SetActive(false);
        }

        else if(id_tela_atual == 4)
        {
            cenaPrincipal.SetActive(false);
            vidaPessoal.SetActive(false);
            estudos.SetActive(true);
            displayStatus.SetActive(true);
        }

        else if(id_tela_atual == 5)
        {
            cenaPrincipal.SetActive(false);
            vidaPessoal.SetActive(false);
            descanso.SetActive(true);
            displayStatus.SetActive(true);
        }

        else if(id_tela_atual == 6)
        {
            cenaPrincipal.SetActive(false);
            trabalho.SetActive(true);
            carreira.SetActive(false);
        }

        else if(id_tela_atual == 7)
        {
            cenaPrincipal.SetActive(false);
            trabalho.SetActive(false);
            carreira.SetActive(true);
        }

        else if(id_tela_atual == 8)
        {
            cenaPrincipal.SetActive(false);
            displayStatus.SetActive(false);
            holderTelasDeFinalizacao.SetActive(true);
            vitoria.SetActive(true);
        }

        else if(id_tela_atual == 9)
        {
            cenaPrincipal.SetActive(false);
            displayStatus.SetActive(false);
            holderTelasDeFinalizacao.SetActive(true);
            derrotaSanidade.SetActive(true);
        }

        else if(id_tela_atual == 10)
        {
            cenaPrincipal.SetActive(false);
            displayStatus.SetActive(false);
            holderTelasDeFinalizacao.SetActive(true);
            derrotaVida.SetActive(true);
        }

        else if(id_tela_atual == 11)
        {
            popUpConfirmarSaida.SetActive(true);
        }
    }

    public void btnMenuPrincipal()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 0;
        TrocarTela();
    }

    public void btnStartGame()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 1;
        TrocarTela();
    }

    public void btnVoltar(){
        soundControler.TocarSomBotao();
        id_tela_atual --;
        TrocarTela();
    }

    public void btnCarregarCenaPrincipal()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 2;
        TrocarTela();
    }


    public void btnSair()
    {
        soundControler.TocarSomBotao();
        Debug.Log("Jogo Encerrado!");
    }


    public void btnVidaPessoal()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 3;
        TrocarTela();
    }

    public void btnEstudos()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 4;
        TrocarTela();
    }

    public void btnDescanso()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 5;
        TrocarTela();
    }

    public void btnTrabalho()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 6;
        TrocarTela();
    }

    public void btnCarreira()
    {
        soundControler.TocarSomBotao();
        id_tela_atual = 7;
        TrocarTela();
    }

    public void btnPopUpConfirmarSaida()
    {
        soundControler.TocarSomBotao();
        popUpConfirmarSaida.SetActive(true);
    }

    public void SetIdTelaAtual(int id)
    {
        id_tela_atual = id;
    }
}