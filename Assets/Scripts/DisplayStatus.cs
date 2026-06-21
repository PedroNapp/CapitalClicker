using UnityEngine;
using UnityEngine.UI;

public class DisplayStatus : MonoBehaviour
{

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;
    
    [Header("Elementos de Interface")]
    [SerializeField] private Text  dinheiroText; 
    [SerializeField] private Text rendaDiariaText;
    [SerializeField] private Text dividaTotalText;
    [SerializeField] private Text parcelaDividaText;
    [SerializeField] private Text tempoAteProximaParcelaText;
    [SerializeField] private Text sanidadeText;
    [SerializeField] private Slider sanidadeSlider;
    [SerializeField] private Slider saudeSlider;
    [SerializeField] private Text saudeText;
    [SerializeField] private Text dataText;
    [SerializeField] private Text diasText;
    [SerializeField] private GameObject btnPagarTudo;

    public void UpdateAll()
    {
        dinheiroText.text = "R$ " + gameManager.GetDinheiro().ToString("F2");
        rendaDiariaText.text = "R$ " + gameManager.GetRendaDiaria().ToString("F2");
        dividaTotalText.text = "R$ " + gameManager.GetDivida().ToString("F2");
        parcelaDividaText.text = "Parcela: R$" + gameManager.GetParcelaDivida().ToString("F2");
        tempoAteProximaParcelaText.text ="Tempo restante: " + gameManager.GetTempoAteProximaParcela().ToString("F0") + " dias";
        sanidadeText.text = "Sanidade: " + gameManager.GetSanidade().ToString("F1") + "%";
        saudeText.text = "Saúde: " + gameManager.GetSaude().ToString("F1") + "%";
        dataText.text = "Data: " + gameManager.GetData();
        diasText.text = "Dia: " + gameManager.GetDiasPassados().ToString();
        // =========================
        // Sliders
        // =========================

        sanidadeSlider.value = gameManager.GetSanidade();

        saudeSlider.value = gameManager.GetSaude();

        if(gameManager.GetDinheiro() >= gameManager.GetDivida()){
            btnPagarTudo.SetActive(true);
        } else{
            btnPagarTudo.SetActive(false);
        }

    }

    public void BtnPagarTudo()
    {   
        if (gameManager.GetDinheiro() >= gameManager.GetDivida())
        {
            float sobra = gameManager.GetDinheiro() - gameManager.GetDivida();

            gameManager.SetDinheiro(sobra);
            gameManager.SetDivida(0);
            UpdateAll();
        }
    }

}
