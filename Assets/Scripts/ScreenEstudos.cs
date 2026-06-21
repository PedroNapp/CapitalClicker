using UnityEngine;
using UnityEngine.UI;

public class ScreenEstudos : MonoBehaviour
{   
    [Header("Game Manager e Status")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DisplayStatus displayStatus;

    [Header("Habilitação")]
    [SerializeField] private Text textHabilitacao;
    [SerializeField] private Button btnHabilitacao;

    [Header("Curso Básico")]
    [SerializeField] private Text textCursoBasico;
    [SerializeField] private Button btnCursoBasico;
    
    [Header("Faculdade de Tecnologia")]
    [SerializeField] private Text textFaculdadeTecnologia;
    [SerializeField] private Button btnFaculdadeTecnologia;

    [Header("Curso de Administração")]
    [SerializeField] private Text textCursoAdm;
    [SerializeField] private Button btnCursoAdm;

    [Header("Sprites para popups")]
    [SerializeField] private Sprite[] spritesCursos;
    [SerializeField] private PopUpInfo popup;

    // variaveis para verificar se ja foi mostrado o popup de conclusão do curso
    private bool possuiCursoBasico;
    private bool possuiHabilitacao;
    private bool possuiFaculdade;
    private bool possuiAdministracao;

    private float custoHabilitação;
    private float custoCursoBasico;
    private float custoFaculdadeTecnologia;
    private float custoCursoAdministracao;
    private float custoUpgrade;

    private void Start()
    {
        possuiHabilitacao = gameManager.GetPossuiHabilitacao();
        possuiCursoBasico = gameManager.GetPossuiCursoBasico();
        possuiFaculdade = gameManager.GetPossuiFaculdadeTecnologia();
        possuiAdministracao = gameManager.GetPossuiCursoAdministracao();
    }

    private void OnEnable()
    {
        AtualizarCustos();
        AtualizarBotoes();
    }

    public void BtnHabilitacao()
    {
        if(gameManager.GetDinheiro() >= custoHabilitação)
        {
            gameManager.RemoverDinheiro(custoHabilitação);
            gameManager.AdicionarCurso(0, 90);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    public void BtnCursoBasico()
    {
        if(gameManager.GetDinheiro() >= custoCursoBasico)
        {
            gameManager.RemoverDinheiro(custoCursoBasico);
            gameManager.AdicionarCurso(1, 180);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    public void BtnFaculdadeTecnologia()
    {
        if(gameManager.GetDinheiro() >= custoFaculdadeTecnologia)
        {
            gameManager.RemoverDinheiro(custoFaculdadeTecnologia);
            gameManager.AdicionarCurso(2, 365);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    public void BtnCursoAdministracao()
    {
        if(gameManager.GetDinheiro() >= custoCursoAdministracao)
        {
            gameManager.RemoverDinheiro(custoCursoAdministracao);
            gameManager.AdicionarCurso(3, 730);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    private void AtualizarBotoes(){
        // ------------------- Habilitação ------------------
        if(gameManager.GetPossuiHabilitacao())
        {
            textHabilitacao.text = "Feito";
            textHabilitacao.color = Color.yellow;
            btnHabilitacao.interactable = false;
        }
        
        else if(gameManager.CursoEmAndamento(0))
        {
            textHabilitacao.text = "Em andamento";
            textHabilitacao.color = Color.cyan;
            btnHabilitacao.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoHabilitação)
        {
            textHabilitacao.text = "R$ " + custoHabilitação.ToString("F2");
            textHabilitacao.color = Color.red;
            btnHabilitacao.interactable = false;
        }

        else
        {
            textHabilitacao.text = "R$ " + custoHabilitação.ToString("F2");
            textHabilitacao.color = Color.green;
            btnHabilitacao.interactable = true;
        }
        // -------------------------------------------------------------

        // ------------------- Curso Básico ------------------
        if(gameManager.GetPossuiCursoBasico())
        {
            textCursoBasico.text = "Feito";
            textCursoBasico.color = Color.yellow;
            btnCursoBasico.interactable = false;
        }
        
        else if(gameManager.CursoEmAndamento(1))
        {
            textCursoBasico.text = "Em andamento";
            textCursoBasico.color = Color.cyan;
            btnCursoBasico.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoCursoBasico)
        {
            textCursoBasico.text = "R$ " + custoCursoBasico.ToString("F2");
            textCursoBasico.color = Color.red;
            btnCursoBasico.interactable = false;
        }

        else
        {
            textCursoBasico.text = "R$ " + custoCursoBasico.ToString("F2");
            textCursoBasico.color = Color.green;
            btnCursoBasico.interactable = true;
        }
        // -------------------------------------------------------------

        // ------------------- Faculdade de Tecnologia ------------------
        if(gameManager.GetPossuiFaculdadeTecnologia())
        {
            textFaculdadeTecnologia.text = "Feito";
            textFaculdadeTecnologia.color = Color.yellow;
            btnFaculdadeTecnologia.interactable = false;
        }
        
        else if(gameManager.CursoEmAndamento(2))
        {
            textFaculdadeTecnologia.text = "Em andamento";
            textFaculdadeTecnologia.color = Color.cyan;
            btnFaculdadeTecnologia.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoFaculdadeTecnologia)
        {
            textFaculdadeTecnologia.text = "R$ " + custoFaculdadeTecnologia.ToString("F2");
            textFaculdadeTecnologia.color = Color.red;
            btnFaculdadeTecnologia.interactable = false;
        }

        else
        {
            textFaculdadeTecnologia.text = "R$ " + custoFaculdadeTecnologia.ToString("F2");
            textFaculdadeTecnologia.color = Color.green;
            btnFaculdadeTecnologia.interactable = true;
        }
        // -------------------------------------------------------------

        // ------------------- Curso de Administração ------------------
        if(gameManager.GetPossuiCursoAdministracao())
        {
            textCursoAdm.text = "Feito";
            textCursoAdm.color = Color.yellow;
            btnCursoAdm.interactable = false;
        }
        
        else if(gameManager.CursoEmAndamento(3))
        {
            textCursoAdm.text = "Em andamento";
            textCursoAdm.color = Color.cyan;
            btnCursoAdm.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoCursoAdministracao)
        {
            textCursoAdm.text = "R$ " + custoCursoAdministracao.ToString("F2");
            textCursoAdm.color = Color.red;
            btnCursoAdm.interactable = false;
        }

        else
        {
            textCursoAdm.text = "R$ " + custoCursoAdministracao.ToString("F2");
            textCursoAdm.color = Color.green;
            btnCursoAdm.interactable = true;
        }
    }

    private void AtualizarCustos(){
        custoUpgrade = gameManager.GetCustoDeUpgrade();
        custoHabilitação = 1000 * (1 + custoUpgrade);
        custoCursoBasico = 2000 * (1 + custoUpgrade);
        custoFaculdadeTecnologia = 5000 * (1 + custoUpgrade);
        custoCursoAdministracao = 10000 * (1 + custoUpgrade);
    }

    // =========== Verificar se algum curso foi concluído para mostrar popup =========== //
    public void VerificarEstudos()
    {
        if(gameManager.GetPossuiHabilitacao() && !possuiHabilitacao)
        {
            popup.Mostrar(
                "Curso Concluído",
                "Você concluiu a Habilitação.\n+1% de renda diária.",
                spritesCursos[0]
            );

            possuiHabilitacao = true;
        }

        if(gameManager.GetPossuiCursoBasico() && !possuiCursoBasico)
        {
            popup.Mostrar(
                "Curso Concluído",
                "Você concluiu o Curso Básico.\n+15% de renda diária.",
                spritesCursos[1]
            );

            possuiCursoBasico = true;
        }

        if(gameManager.GetPossuiFaculdadeTecnologia() && !possuiFaculdade)
        {
            popup.Mostrar(
                "Curso Concluído",
                "Você concluiu a Faculdade de Tecnologia.\n+10% de renda diária.",
                spritesCursos[2]
            );

            possuiFaculdade = true;
        }

        if(gameManager.GetPossuiCursoAdministracao() && !possuiAdministracao)
        {
            popup.Mostrar(
                "Curso Concluído",
                "Você concluiu o Curso de Administração.\n+25% de renda diária.",
                spritesCursos[3]
            );

            possuiAdministracao = true;
        }
    }
}
