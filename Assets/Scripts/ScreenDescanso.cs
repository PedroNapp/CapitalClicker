using UnityEngine;
using UnityEngine.UI;

public class ScreenDescanso : MonoBehaviour
{
    [Header("Dormir Melhor")]
    [SerializeField] private Text textDormir;
    [SerializeField] private Button btnDormir;

    [Header("Terrapia")]
    [SerializeField] private Text textTerapia;
    [SerializeField] private Button btnTerapia;
    
    [Header("Ir ao Parque")]
    [SerializeField] private Text textParque;
    [SerializeField] private Button btnParque;

    [Header("Academia")]
    [SerializeField] private Text textAcademia;
    [SerializeField] private Button btnAcademia;

    [Header("Game Manager e Status")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DisplayStatus displayStatus;

    private float custoDormir;
    private float custoTerrapia;
    private float custoParque;
    private float custoAcademia;
    private float custoUpgrade;

    private void OnEnable()
    {
        AtualizarCustos();
        AtualizarBotoes();
    }

    public void BtnParque()
    {
        if(gameManager.GetDinheiro() >= custoParque)
        {
            gameManager.RemoverDinheiro(custoParque);
            gameManager.AdicionarDescanso(0, 1);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    public void BtnDormir()
    {
        if(gameManager.GetDinheiro() >= custoDormir)
        {
            gameManager.RemoverDinheiro(custoDormir);
            gameManager.AdicionarDescanso(1, 7);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    public void BtnTerrapia()
    {
        if(gameManager.GetDinheiro() >= custoTerrapia)
        {
            gameManager.RemoverDinheiro(custoTerrapia);
            gameManager.AdicionarDescanso(2, 90);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }

    public void BtnAcademia()
    {
        if(gameManager.GetDinheiro() >= custoAcademia)
        {
            gameManager.RemoverDinheiro(custoAcademia);
            gameManager.AdicionarDescanso(3, 30);
            displayStatus.UpdateAll();
            AtualizarBotoes();
        }
    }


    private void AtualizarBotoes()
    {   
        // ------------------- Ir ao Parque ------------------
        if(gameManager.DescansoEmAndamento(0))
        {
            textParque.text = "Em andamento";
            textParque.color = Color.cyan;
            btnParque.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoParque)
        {
            textParque.text = "R$ " + custoParque.ToString("F2");
            textParque.color = Color.red;
            btnParque.interactable = false;
        }

        else
        {
            textParque.text = "R$ " + custoParque.ToString("F2");
            textParque.color = Color.green;
            btnParque.interactable = true;
        }

        // ------------------- Sono Revigorante ------------------
        if(gameManager.DescansoEmAndamento(1))
        {
            textDormir.text = "Em andamento";
            textDormir.color = Color.cyan;
            btnDormir.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoDormir)
        {
            textDormir.text = "R$ " + custoDormir.ToString("F2");
            textDormir.color = Color.red;
            btnDormir.interactable = false;
        }

        else
        {
            textDormir.text = "R$ " + custoDormir.ToString("F2");
            textDormir.color = Color.green;
            btnDormir.interactable = true;
        }

        // ------------------- Terapia ------------------
        if(gameManager.DescansoEmAndamento(2))
        {
            textTerapia.text = "Em andamento";
            textTerapia.color = Color.cyan;
            btnTerapia.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoTerrapia)
        {
            textTerapia.text = "R$ " + custoTerrapia.ToString("F2");
            textTerapia.color = Color.red;
            btnTerapia.interactable = false;
        }

        else
        {
            textTerapia.text = "R$ " + custoTerrapia.ToString("F2");
            textTerapia.color = Color.green;
            btnTerapia.interactable = true;
        }

        // ------------------- Academia ------------------
        if(gameManager.DescansoEmAndamento(3))
        {
            textAcademia.text = "Em andamento";
            textAcademia.color = Color.cyan;
            btnAcademia.interactable = false;
        }

        else if(gameManager.GetDinheiro() < custoAcademia)
        {
            textAcademia.text = "R$ " + custoAcademia.ToString("F2");
            textAcademia.color = Color.red;
            btnAcademia.interactable = false;
        }

        else
        {
            textAcademia.text = "R$ " + custoAcademia.ToString("F2");
            textAcademia.color = Color.green;
            btnAcademia.interactable = true;
        }
    }

    private void AtualizarCustos(){
        custoUpgrade = gameManager.GetCustoDeUpgrade();
        custoParque = 50 * (1 + custoUpgrade);
        custoDormir = 225 * (1 + custoUpgrade);
        custoTerrapia = 500 * (1 + custoUpgrade);
        custoAcademia = 1200 * (1 + custoUpgrade);
    }
}
