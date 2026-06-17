using UnityEngine;
using UnityEngine.UI;

public class ScreenVitoriaEDerotas : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private InterfaceControll interfaceControll;
    [SerializeField] private Text diasVitoria;
    [SerializeField] private Text diasSanidade;
    [SerializeField] private Text diasVida;
    
    
    private void OnEnable()
    {
        atualizardiasVitoria();
        atualizardiasSanidade();
        atualizardiasVida();
    }

    public void verificarCondicoesDeVitoriaEderrota()
    {
        Venceu();

        if(gameManager.GetSaude() <= 0)
        {   
            PerdeuVida();
            return;
        }
        
        if(gameManager.GetSanidade() <= 0)
        {
            PerdeuSanidade();
            return;
        }

        
    }
    
    public void atualizardiasVitoria()
    {
        diasVitoria.text = "Dias: " + gameManager.GetDiasPassados();
    }

    public void atualizardiasSanidade()
    {
        diasSanidade.text = "Dias: " + gameManager.GetDiasPassados();
    }

    public void atualizardiasVida()
    {
        diasVida.text = "Dias: " + gameManager.GetDiasPassados();
    }

    public void Venceu()
    {
        if(gameManager.GetDivida() <= 0)
        {
            gameManager.SetVenceu(true);
        }

        if(gameManager.GetVenceu() && !gameManager.GetQuerContinuar())
        {   
            gameManager.SetQuerContinuar(true);
            interfaceControll.SetIdTelaAtual(8);
            interfaceControll.TrocarTela();
        }
    }

    public void PerdeuSanidade()
    {
        gameManager.SetPerdeuSanidade(true);
        gameManager.GetPerdeuSanidade();
        interfaceControll.SetIdTelaAtual(9);
        interfaceControll.TrocarTela();
        
    }

    public void PerdeuVida()
    {   
        gameManager.SetPerdeuVida(true);
        interfaceControll.SetIdTelaAtual(10);
        interfaceControll.TrocarTela();
    }

    public void BtnContinuar()
    {
        gameManager.SetQuerContinuar(true);
        interfaceControll.SetIdTelaAtual(2);
        interfaceControll.TrocarTela();
    }
}
