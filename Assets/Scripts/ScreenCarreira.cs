using UnityEngine;
using UnityEngine.UI;

public class ScreenCarreira : MonoBehaviour
{
    [Header("Texto de display de profissoões")]
    [SerializeField] private Text textFaxineiro;
    [SerializeField] private Text textEntregador;
    [SerializeField] private Text textVendedor;
    [SerializeField] private Text textProgramador;
    [SerializeField] private Text textGerente;
    [SerializeField] private Text textCEO;

    [Header("Botões de candidatura")]
    [SerializeField] private Button interacaoFaxineiro;
    [SerializeField] private Button interacaoEntregador;
    [SerializeField] private Button interacaoVendedor;
    [SerializeField] private Button interacaoProgramador;
    [SerializeField] private Button interacaoGerente;
    [SerializeField] private Button interacaoCEO;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        AtualizarBotoes();
    }

    // ------------------- BOTÃO FAXINEIRO ------------------
    public void btnFaxineiro(){
        gameManager.SetProfissaoPendente(1);
        gameManager.SetDiasParaProfissao(7);
        AtualizarBotoes();
    }

    // ------------------- BOTÃO ENTREGADOR ------------------
    public void btnEntregador(){
        
        if(gameManager.GetPossuiHabilitacao())
        {
            gameManager.SetProfissaoPendente(2);
            gameManager.SetDiasParaProfissao(10);
            AtualizarBotoes();
        }
    }

    // ------------------- BOTÃO VENDEDOR ------------------
    public void btnVendedor(){
        if(gameManager.GetNivelComunicacao() >= 3)
        {
            gameManager.SetProfissaoPendente(3);
            gameManager.SetDiasParaProfissao(10);
            AtualizarBotoes();
        }
    }

    // ------------------- BOTÃO PROGRAMADOR ------------------
    public void btnProgramador(){
       
        if(gameManager.GetPossuiFaculdadeTecnologia())
        {
            gameManager.SetProfissaoPendente(4);
            gameManager.SetDiasParaProfissao(15);
            AtualizarBotoes();
        }
    }

    // ------------------- BOTÃO GERENTE DE PROJETO ------------------
    public void btnGerenteDeProjeto(){
        
        if(gameManager.GetPossuiCursoAdministracao())
        {
            gameManager.SetProfissaoPendente(5);
            gameManager.SetDiasParaProfissao(25);
            AtualizarBotoes();
        }
    }

    // ------------------- BOTÃO CEO ------------------
    public void btnCEO(){
        if(gameManager.GetDiasTrabalhados(5) >= 1000)
        {
            gameManager.SetProfissaoPendente(6);
            gameManager.SetDiasParaProfissao(60);
            AtualizarBotoes();
        }
    }


    // ------------ Metodo para atualizar os botões da tela ------------
    // Este método verifica o status de cada profissão e atualiza o texto,  
    // cor e interatividadedos dos botões de acordo com as condições do jogo.
    public void AtualizarBotoes()
    {   
        // ------------------- FAXINEIRO ------------------
        if(gameManager.GetTrabalhoAtual() == 1)
        {
            textFaxineiro.text = "Atual";
            textFaxineiro.color = Color.yellow;
            interacaoFaxineiro.interactable = false;
        }
        
        else if(gameManager.GetProfissaoPendente() == 1)
        {
            textFaxineiro.text = "Aguardando: " + gameManager.GetDiasParaProfissao() + " dias";
            textFaxineiro.color = Color.cyan;
            interacaoFaxineiro.interactable = false;
        }

        else
        {
            textFaxineiro.color = Color.green;
            textFaxineiro.text = "Candidatar-se";
            interacaoFaxineiro.interactable = true;
        }

        // ------------------- ENTREGADOR ------------------
        if(gameManager.GetTrabalhoAtual() == 2)
        {
            textEntregador.text = "Atual";
            textEntregador.color = Color.yellow;
            interacaoEntregador.interactable = false;
        }
        
        else if(gameManager.GetProfissaoPendente() == 2)
        {
            textEntregador.text = "Aguardando: " + gameManager.GetDiasParaProfissao() + " dias";
            textEntregador.color = Color.cyan;
            interacaoEntregador.interactable = false;
        }

        else if(gameManager.GetPossuiHabilitacao())
        {
            textEntregador.color = Color.green;
            textEntregador.text = "Candidatar-se";
            interacaoEntregador.interactable = true;
        }
        else
        {
            textEntregador.color = Color.red;
            textEntregador.text = "Não Qualificado";
            interacaoEntregador.interactable = false;
        }

        // ------------------- VENDEDOR ------------------
        if(gameManager.GetTrabalhoAtual() == 3)
        {
            textVendedor.text = "Atual";
            textVendedor.color = Color.yellow;
            interacaoVendedor.interactable = false;
        }
        
        else if(gameManager.GetProfissaoPendente() == 3)
        {
            textVendedor.text = "Aguardando: " + gameManager.GetDiasParaProfissao() + " dias";
            textVendedor.color = Color.cyan;
            interacaoVendedor.interactable = false;
        }
        
        else if(gameManager.GetNivelComunicacao() >= 3)
        {
            textVendedor.color = Color.green;
            textVendedor.text = "Candidatar-se";
            interacaoVendedor.interactable = true;
        }

        else
        {
            textVendedor.color = Color.red;
            textVendedor.text = "Não Qualificado";
            interacaoVendedor.interactable = false;
        }

        // ------------------- PROGRAMADOR ------------------
        if(gameManager.GetTrabalhoAtual() == 4)
        {
            textProgramador.text = "Atual";
            textProgramador.color = Color.yellow;
            interacaoProgramador.interactable = false;
        }
        
        else if(gameManager.GetProfissaoPendente() == 4)
        {
            textProgramador.text = "Aguardando: " + gameManager.GetDiasParaProfissao() + " dias";
            textProgramador.color = Color.cyan;
            interacaoProgramador.interactable = false;
        }

        else if(gameManager.GetPossuiFaculdadeTecnologia())
        {
            textProgramador.color = Color.green;
            textProgramador.text = "Candidatar-se";
            interacaoProgramador.interactable = true;
        }

        else
        {
            textProgramador.color = Color.red;
            textProgramador.text = "Não Qualificado";
            interacaoProgramador.interactable = false;
        }


        // ------------------- GERENTE DE PROJETO ------------------
        if(gameManager.GetTrabalhoAtual() == 5)
        {
            textGerente.text = "Atual";
            textGerente.color = Color.yellow;
            interacaoGerente.interactable = false;
        }
        
        else if(gameManager.GetProfissaoPendente() == 5)
        {
            textGerente.text = "Aguardando: " + gameManager.GetDiasParaProfissao() + " dias";
            textGerente.color = Color.cyan;
            interacaoGerente.interactable = false;
        }

        else if(gameManager.GetPossuiCursoAdministracao())
        {
            textGerente.color = Color.green;
            textGerente.text = "Candidatar-se";
            interacaoGerente.interactable = true;
        }
        else
        {
            textGerente.color = Color.red;
            textGerente.text = "Não Qualificado";
            interacaoGerente.interactable = false;
        }


        // ------------------- CEO ------------------
        if(gameManager.GetTrabalhoAtual() == 6)
        {
            textCEO.text = "Atual";
            textCEO.color = Color.yellow;
            interacaoCEO.interactable = false;
        }
        
        else if(gameManager.GetProfissaoPendente() == 6)
        {
            textCEO.text = "Aguardando: " + gameManager.GetDiasParaProfissao() + " dias";
            textCEO.color = Color.cyan;
            interacaoCEO.interactable = false;
        }

        else if(gameManager.GetDiasTrabalhados(5) >= 1000)
        {
            textCEO.color = Color.green;
            textCEO.text = "Candidatar-se";
            interacaoCEO.interactable = true;
        }
        else
        {
            textCEO.color = Color.red;
            textCEO.text = "Não Qualificado";
            interacaoCEO.interactable = false;
        }
    }
}
