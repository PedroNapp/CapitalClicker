using UnityEngine;
using UnityEngine.UI;


public class ScreenTrabalho : MonoBehaviour
{
    [Header("Sistema de display do trabalho")]
    [SerializeField] private Text displayNomeTrabalho;
    [SerializeField] private Text displayTempoTrabalhado;

    [SerializeField] private GameObject holderCargoTrabalho;
    [SerializeField] private Text displayCargoTrabalho;
    [SerializeField] private Image displayImagemTrabalho;

    [Header("Tecnicas")]
    [SerializeField] private Text displayNivelTecnicas;
    [SerializeField] private Text displayBtnPrecoTecnicas;

    [Header("Produtividade Melhorada")]
    [SerializeField] private Text displayNivelProdutividade;
    [SerializeField] private Text displayBtnPrecoProdutividade;

    [Header("Boa comunicacao")]
    [SerializeField] private Text displayNivelComunicacao;
    [SerializeField] private Text displayBtnPrecoComunicacao;

    [Header("Game Manager, Status e PopUp")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DisplayStatus displayStatus;
    [SerializeField] private PopUpInfo popup;

    [Header("Sprites dos trabalhos")]
    [SerializeField] private Sprite[] spritesTrabalho;
    
    private float precoTecnica;
    private float precoProdutividade;
    private float precoComunicacao;

    private int ultimoTrabalho = 0;
    private string ultimoCargo = "";

    // =========== Atualiza ao ser iniciada =========== //
    private void Start()
    {
        ultimoTrabalho = gameManager.GetTrabalhoAtual();
        ultimoCargo = gameManager.GetCargoAtual();
    }

    private void OnEnable()
    {
        CarregaUpgrades();
        AtualizarTrabalho();
    }

    // =========== Atualiza tudo =========== //
    public void updateAll(){
        CarregaUpgrades();
        displayStatus.UpdateAll();
        AtualizarTrabalho();
    }

    // =========== Carrega os preços e níveis dos upgrades =========== //
    private void CarregaUpgrades(){
        AtualizarPrecos();

        displayNivelTecnicas.text = "Nível: " + gameManager.GetNivelTecnicas() + "/5";
        displayNivelProdutividade.text = "Nível: " + gameManager.GetNivelProdutividade() + "/5";
        displayNivelComunicacao.text = "Nível: " + gameManager.GetNivelComunicacao() + "/5";

        if (gameManager.GetNivelTecnicas() < 5)
        {
            displayBtnPrecoTecnicas.text = "R$ " + precoTecnica.ToString("F2");
        }
        if (gameManager.GetNivelProdutividade() < 5)
        {
            displayBtnPrecoProdutividade.text = "R$ " + precoProdutividade.ToString("F2");
        }
        if (gameManager.GetNivelComunicacao() < 5 )
        {
            displayBtnPrecoComunicacao.text = "R$ " + precoComunicacao.ToString("F2");
        }
    }

    private void AtualizarPrecos()
    {
        precoTecnica = 100 + (gameManager.GetNivelTecnicas() * 100) * (1 + gameManager.GetCustoDeUpgrade());
        precoProdutividade = 250 + (gameManager.GetNivelProdutividade() * 250) * (1 + gameManager.GetCustoDeUpgrade());
        precoComunicacao = 400 + (gameManager.GetNivelComunicacao() * 400) * (1 + gameManager.GetCustoDeUpgrade());
    }

    // =========== Funções dos botões de upgrade =========== //
    public void BtnUpgradeTecnicas()
    {
        if (gameManager.GetNivelTecnicas() >= 5)
        {
            return;
        }

        else if (gameManager.GetDinheiro() >= precoTecnica)
        {
            gameManager.RemoverDinheiro(precoTecnica);
            gameManager.SetNivelTecnicas(gameManager.GetNivelTecnicas() + 1);

            gameManager.SetRecuperacaoSanidade(
                gameManager.GetRecuperacaoSanidade() + 0.1f
            );

            if (gameManager.GetNivelTecnicas() >= 5)
            {
                displayBtnPrecoTecnicas.text = "Max";
            }

            updateAll();
        }
    }

    public void BtnUpgradeProdutividade()
    {

        if (gameManager.GetNivelProdutividade() >= 5)
        {
            return;
        }

        else if (gameManager.GetDinheiro() >= precoProdutividade)
        {
            gameManager.RemoverDinheiro(precoProdutividade);
            gameManager.SetNivelProdutividade(gameManager.GetNivelProdutividade() + 1);

            gameManager.SetMultiplicadorRenda(
                gameManager.GetMultiplicadorRenda() + 0.05f
            );

            if (gameManager.GetNivelProdutividade() >= 5)
            {
                displayBtnPrecoProdutividade.text = "Max";
            }

            updateAll();
        }
    }

    public void BtnUpgradeComunicacao()
    {
        if (gameManager.GetNivelComunicacao() >= 5)
        {
            return;
        }

        else if (gameManager.GetDinheiro() >= precoComunicacao)
        {
            gameManager.RemoverDinheiro(precoComunicacao);
            gameManager.SetNivelComunicacao(gameManager.GetNivelComunicacao() + 1);
            gameManager.SetEventosNegativos(gameManager.GetEventosNegativos() - 1f);

            if (gameManager.GetNivelComunicacao() >= 5)
            {
                displayBtnPrecoComunicacao.text = "Max";
            }
            
            updateAll();
        }
    }

    // =========== Atualiza o display do trabalho atual =========== //
    public void AtualizarTrabalho()
    {
        switch (gameManager.GetTrabalhoAtual())
        {
            case 0:
                displayNomeTrabalho.text = "Desempregado";
                displayTempoTrabalhado.text = "Dias não trabalhados: " + gameManager.GetDiasTrabalhados(0) + " dias";
                holderCargoTrabalho.SetActive(false);
                displayImagemTrabalho.sprite = spritesTrabalho[0];
                break;
            case 1:
                displayNomeTrabalho.text = "Faxineiro";
                displayTempoTrabalhado.text = "Dias trabalhados: " + gameManager.GetDiasTrabalhados(1) + " dias";
                holderCargoTrabalho.SetActive(true);
                displayCargoTrabalho.text = "Cargo Atual: " + gameManager.GetCargoAtual() + 
                    "\nPróximo Cargo em: " + gameManager.GetDiasParaProximoCargo() + " dias";
                displayImagemTrabalho.sprite = spritesTrabalho[1];
                break;
            case 2:
                displayNomeTrabalho.text = "Entregador";
                displayTempoTrabalhado.text = "Dias trabalhados: " + gameManager.GetDiasTrabalhados(2) + " dias";
                holderCargoTrabalho.SetActive(true);
                displayCargoTrabalho.text = "Cargo Atual: " + gameManager.GetCargoAtual() + 
                    "\nPróximo Cargo em: " + gameManager.GetDiasParaProximoCargo() + " dias";
                displayImagemTrabalho.sprite = spritesTrabalho[2];
                break;
            case 3:
                displayNomeTrabalho.text = "Vendedor";
                displayTempoTrabalhado.text = "Dias trabalhados: " + gameManager.GetDiasTrabalhados(3) + " dias";
                holderCargoTrabalho.SetActive(true);
                displayCargoTrabalho.text = "Cargo Atual: " + gameManager.GetCargoAtual() + 
                    "\nPróximo Cargo em: " + gameManager.GetDiasParaProximoCargo() + " dias";
                displayImagemTrabalho.sprite = spritesTrabalho[3];
                break;
            case 4:
                displayNomeTrabalho.text = "Programador";
                displayTempoTrabalhado.text = "Dias trabalhados: " + gameManager.GetDiasTrabalhados(4) + " dias";
                holderCargoTrabalho.SetActive(true);
                displayCargoTrabalho.text = "Cargo Atual: " + gameManager.GetCargoAtual() + 
                    "\nPróximo Cargo em: " + gameManager.GetDiasParaProximoCargo() + " dias" ;
                displayImagemTrabalho.sprite = spritesTrabalho[4];
                break;
            case 5:
                displayNomeTrabalho.text = "Gerente";
                displayTempoTrabalhado.text = "Dias trabalhados: " + gameManager.GetDiasTrabalhados(5) + " dias";
                holderCargoTrabalho.SetActive(true);
                displayCargoTrabalho.text = "Cargo Atual: " + gameManager.GetCargoAtual() + 
                    "\nPróximo Cargo em: " + gameManager.GetDiasParaProximoCargo() + " dias";
                displayImagemTrabalho.sprite = spritesTrabalho[5];
                break;
            case 6:
                displayNomeTrabalho.text = "CEO";
                displayTempoTrabalhado.text = "Dias trabalhados: " + gameManager.GetDiasTrabalhados(6) + " dias";
                holderCargoTrabalho.SetActive(true);
                displayCargoTrabalho.text = "Cargo Atual: " + gameManager.GetCargoAtual() + 
                    "\nPróximo Cargo em: " + gameManager.GetDiasParaProximoCargo() + " dias";
                displayImagemTrabalho.sprite = spritesTrabalho[6];
                break;
        }
    }

    // =========== Pop-ups para contratação e promoção ========== //
    public void PopUpEmprego()
    {
        if(gameManager.GetTrabalhoAtual() != ultimoTrabalho && gameManager.GetTrabalhoAtual() != 0)
        {
            popup.Mostrar(
                "Contratação",
                "Você conseguiu um novo emprego!",
                spritesTrabalho[gameManager.GetTrabalhoAtual()]
            );

            ultimoTrabalho = gameManager.GetTrabalhoAtual();
            ultimoCargo = gameManager.GetCargoAtual();
        }
    }

    public void PopUpPromocao()
    {
        if(gameManager.GetCargoAtual() != ultimoCargo && gameManager.GetTrabalhoAtual() != 0)
        {
            popup.Mostrar(
                "Promoção",
                "Parabéns! Você foi promovido para " + gameManager.GetCargoAtual() + "!",
                spritesTrabalho[gameManager.GetTrabalhoAtual()]
            );

            ultimoCargo = gameManager.GetCargoAtual();
        }
    }
}
