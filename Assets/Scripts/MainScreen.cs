using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{   
    [Header("Elementos de Interface")]
    [SerializeField] private Text dicasText;
    [SerializeField] private DisplayStatus displayStatus;
    [SerializeField] private SistemaDeEventos sistemaDeEventos;
    [SerializeField] private ScreenTrabalho screenTrabalho;
    [SerializeField] private ScreenEstudos screenEstudos;
    [SerializeField] private ScreenVitoriaEDerotas screenVitoriaEDerotas;
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        displayStatus.UpdateAll();
    }

    public void OnEnable()
    {
        dicasText.text = Dicas();
        displayStatus.UpdateAll();
    }

    public void btnAvançarDia()
    {
        gameManager.AvancarDia();
        screenVitoriaEDerotas.verificarCondicoesDeVitoriaEderrota();
        sistemaDeEventos.VerificarEventos();
        displayStatus.UpdateAll();
        dicasText.text = Dicas();
        screenTrabalho.PopUpEmprego();
        screenTrabalho.PopUpPromocao();
        screenEstudos.VerificarEstudos();
    }

    private string Dicas()
    {
        string[] dicas = {
            "Dica: Tente economizar dinheiro para pagar suas dívidas!",
            "Dica: Mantenha sua sanidade alta para evitar problemas de saúde.",
            "Dica: Invista em atividades que aumentem sua renda diária.",
            "Dica: Fique atento às parcelas da dívida para não perder o controle financeiro.",
            "Dica: Lembre-se de cuidar da sua saúde para evitar doenças e despesas médicas.",
            "Dica: Mantenha dinheiro reservado para emergências."
        };
    
        int indiceDica = Random.Range(0, dicas.Length);
        return dicas[indiceDica];
    }
}
