using UnityEngine;
using UnityEngine.UI;

public class ScreenVidaPessoal : MonoBehaviour
{
    [Header("Sprites das Carinhas")]
    [SerializeField] private Sprite[] spritesCarinhas;
    
    [Header("Situação Sanidade")]
    [SerializeField] private Image displayCarinha;
    [SerializeField] private Text displaySituacaoSanidade;
    [SerializeField] private Text displayDescricaoSituacao;
    [SerializeField] private Image Borda;

    [Header("Atividades e Dicas")]
    [SerializeField] private Text displayAtividades;
    [SerializeField] private Text displayStrees;
    [SerializeField] private Text displayDicas;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        atualizarDisplaySituacao();
        atualizarDisplayAtividades();
        atualizarDicas();
    }

    private void atualizarDisplaySituacao(){
        float sanidade = gameManager.GetSanidade();

        if (sanidade < 25)
        {
            displayCarinha.sprite = spritesCarinhas[2];
            displaySituacaoSanidade.text = "Crítica";
            displayDescricaoSituacao.text = "Sua sanidade está em um estado crítico. Cuidado nessesario Para evitar morte ou Insanidade.";
            Borda.color = Color.red;
        }
        else if (sanidade < 50)
        {
            displayCarinha.sprite = spritesCarinhas[1];
            displaySituacaoSanidade.text = "Neutro";
            displayDescricaoSituacao.text = "Sua sanidade está em um estado neutro. Cuidado nessesario Para evitar consequências graves.";
            Borda.color = Color.yellow;
        }
        else
        {
            displayCarinha.sprite = spritesCarinhas[0];
            displaySituacaoSanidade.text = "Estável";
            displayDescricaoSituacao.text = "Sua sanidade está em um estado bom. Continue assim para manter sua saúde.";
            Borda.color = Color.green;
        }
    }

    private void atualizarDisplayAtividades(){
        float variacaoDiaria = gameManager.GetRecuperacaoSanidade() + gameManager.GetBonusDescansoSanidade() - (gameManager.CalcularpressaoFinanceira() / 250) - gameManager.GetDesgasteEstudos();
        displayStrees.text = "Variação Diária: " + (variacaoDiaria >= 0 ? "+" : "") +variacaoDiaria.ToString("F2") + "%" 
                             + "\nRecuperação Diaria: " + (gameManager.GetRecuperacaoSanidade() + gameManager.GetBonusDescansoSanidade()) + "%";
        displayAtividades.text = gameManager.GetCursosEmAndamento() +"\n" + gameManager.GetDescansoEmAndamento();
    }

    private void atualizarDicas(){
        string[] dicas = {
            "Dica: Evite chegar a um estado crítico de sanidade, pois isso pode levar a consequências graves.",
            "Dica: Faça atividades para reduzir o estresse, como exercícios Academia, Terrapia ou Tire um dia de folga.",
            "Dica: Evite tirar muitos dias de folga, pois isso pode afetar sua renda diária.",
            "Dica: Academia recupera não so sua sanidade, mas também sua saúde física.",
            "Dica: Aumente sua renda pois evita estresse por conta da divida.",
            "Dica: Melhore suas tecnicas de trabalho para evitar desgaste."
        };
    
        int indiceDica = Random.Range(0, dicas.Length);
        displayDicas.text = dicas[indiceDica];
    }
}
