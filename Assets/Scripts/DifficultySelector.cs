using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    private string selectedDifficulty = "Fácil";

    [SerializeField] private Image selected;
    [SerializeField] private GameManager gameManager;

    [Header("Cards")]
    [SerializeField] private Transform cardFacil;
    [SerializeField] private Transform cardMedio;
    [SerializeField] private Transform cardDificil;

    // força a atulização de dados ao iniciar a cena, evita que nunca exista uma dificuldade selecionada e os dados sejam nulos.
    // O padrao é a dificuldade fácil.
    void OnEnable()
    {
        UpdateSelectedDifficulty();
    }

    // Método do botão de dificuldade Fácil.
    public void btnFacil()
    {
        selectedDifficulty = "Fácil";
        UpdateSelectedDifficulty();
    }

    // Método do botão de dificuldade Medio.
    public void btnMedio()
    {
        selectedDifficulty = "Médio";
        UpdateSelectedDifficulty();
    }

    // Método do botão de dificuldade difícil.
    public void btnDificil()
    {
        selectedDifficulty = "Difícil";
        UpdateSelectedDifficulty();
    }

    // Método para atualizar a dificuldade selecionada e aplicar as configurações correspondentes.
    public void UpdateSelectedDifficulty(){
        if (selectedDifficulty == "Fácil")
        {   
            gameManager.GameReset();
            SetDificuldade( 
                dinheiro: 1000, divida: 20000, multiplicadorDivida: 0.05f,
                recuperacaoSanidade: 1f, custoDeUpgrade: 0.10f, 
                eventosNegativos: 10f,multiplicadorJuros: 0.05f
            );

            selected.color = Color.green;
            MoveSelectedToCard(cardFacil);
        }

        else if (selectedDifficulty == "Médio")
        {   
            gameManager.GameReset();
            SetDificuldade(
                dinheiro: 300, divida: 35000, multiplicadorDivida: 0.10f,
                recuperacaoSanidade: 0.5f, custoDeUpgrade: 0.25f,
                eventosNegativos: 25f, multiplicadorJuros: 0.15f
            );
            selected.color = Color.orange;
            MoveSelectedToCard(cardMedio);
        }

        else if (selectedDifficulty == "Difícil")
        {   
            gameManager.GameReset();
            SetDificuldade(
                dinheiro: 100, divida: 50000, multiplicadorDivida: 0.20f,
                recuperacaoSanidade: 0.35f, custoDeUpgrade: 0.35f,
                eventosNegativos: 50f, multiplicadorJuros: 0.20f
            );
            selected.color = Color.red;
            MoveSelectedToCard(cardDificil);
        }
    }


    
    // Método para mover a imagem de seleção para o card correspondente.
    public void MoveSelectedToCard(Transform card)
    {
        RectTransform cardRect = card.GetComponent<RectTransform>();

        float offset = cardRect.rect.width * card.lossyScale.x / 2.9f;

        selected.rectTransform.position = new Vector3(
            card.position.x - offset,
            selected.rectTransform.position.y,
            selected.rectTransform.position.z
        );
    }

    // Método para setar as variáveis de dificuldade no GameManager
    public void SetDificuldade(
        float dinheiro, float divida, float multiplicadorDivida,
        float recuperacaoSanidade, float custoDeUpgrade,
        float eventosNegativos, float multiplicadorJuros = 0.05f
    ) { 
        // Todos o multiplicadores tem que ser passados primeiro, para que ele possa calcular os valores corretamente.
        gameManager.SetMultiplicadorJuros(multiplicadorJuros);
        gameManager.SetMultiplicadorDivida(multiplicadorDivida);

        gameManager.SetDinheiro(dinheiro);
        gameManager.SetDivida(divida);

        gameManager.SetRecuperacaoSanidade(recuperacaoSanidade);

        gameManager.SetCustoDeUpgrade(custoDeUpgrade);
        gameManager.SetEventosNegativos(eventosNegativos);
    }
}