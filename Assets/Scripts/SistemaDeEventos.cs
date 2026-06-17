using UnityEngine;

public class SistemaDeEventos : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PopUpInfo popUpInfo;
    [SerializeField] private Sprite[] spritesEventos;

    private int ultimoEventoPositivo = -1;
    private float chanceNegativa;

    
    // Método para verificar se um evento aleatório deve ocorrer, e qual tipo de evento será.
    public void VerificarEventos()
    {  
        float chanceEvento = Random.Range(0f, 100f);

        if(chanceEvento > 5f)
            return;

        float tipoEvento = Random.Range(0f, 100f);
        chanceNegativa = 10 + gameManager.GetEventosNegativos();

        if(tipoEvento < 1f)
        {
            EventoCatastrofe();
        }
        else if(tipoEvento < chanceNegativa)
        {
            EventoNegativo();
        }
        else
        {
            EventoPositivo();
        }
    }

    // =========================
    // Eventos Relacionados ao Trabalho.
    // =========================

    public void EventoElogioChefe(){   
        if(gameManager.GetTrabalhoAtual() == 0)
            return;

        if(gameManager.GetDiasParaProximoCargo() <= 0)
            return;

        gameManager.AdicionarDiasTrabalhados(
            gameManager.GetTrabalhoAtual(),
            10
        );

        popUpInfo.Mostrar(
            "Elogio do Chefe",
            "Seu trabalho foi reconhecido.\n+10 dias de experiência",
            spritesEventos[6]
        );
    }

    public void EventoPromocao()
    {
        if(gameManager.GetTrabalhoAtual() == 0)
            return;

        if(gameManager.GetDiasParaProximoCargo() <= 0)
            return;

        int bonusDias = Random.Range(15, 31);

        gameManager.AdicionarDiasTrabalhados(
            gameManager.GetTrabalhoAtual(),
            bonusDias
        );

        popUpInfo.Mostrar(
            "Evento de carreira!",
            "+" + bonusDias + " dias de experiência.",
            spritesEventos[0]
        );
    }
    
    public void EventoDemissao()
    {   
        if(gameManager.GetTrabalhoAtual() == 0)
            return;

        gameManager.SetTrabalhoAtual(0);

        popUpInfo.Mostrar(
            "Demissão",
            "A empresa realizou cortes e você perdeu o emprego.",
            spritesEventos[10]
        );
    }

    // ========================= 
    // Eventos Relacionados a Vida e Sanidade.
    // =========================

    public void EventoDiaBom()
    {
        gameManager.SetSanidade(
            Mathf.Min(100, gameManager.GetSanidade() + 5)
        );

        popUpInfo.Mostrar(
            "Dia Bom",
            "Hoje tudo pareceu dar certo.\n+5 Sanidade",
            spritesEventos[2]
        );
    }

    public void EventoDiaEstressante()
    {
        gameManager.SetSanidade(
            Mathf.Max(0, gameManager.GetSanidade() - 5)
        );

        popUpInfo.Mostrar(
            "Dia Estressante",
            "Nada deu certo hoje.\n-5 Sanidade",
            spritesEventos[5]
        );
    }

    public void EventoCriseEmocional()
    {
        gameManager.SetSanidade(
            Mathf.Max(0, gameManager.GetSanidade() - 10)
        );

        popUpInfo.Mostrar(
            "Crise Emocional",
            "Uma situação difícil abalou seu emocional.\n-10 Sanidade",
            spritesEventos[12]
        );
    }

    public void EventoAmigos()
    {
        gameManager.SetSanidade(
            Mathf.Min(100, gameManager.GetSanidade() + 8)
        );

        popUpInfo.Mostrar(
            "Encontro com Amigos",
            "Você passou um tempo agradável com amigos.\n+8 Sanidade",
            spritesEventos[3]
        );
    }

    public void EventoProblemaSaudeLeve()
    {
        gameManager.SetSaude(
            Mathf.Max(0, gameManager.GetSaude() - 2)
        );

        popUpInfo.Mostrar(
            "Problema de Saúde Leve",
            "Você pegou um resfriado.\n-2 Saúde",
            spritesEventos[11]
        );
    }

    public void EventoProblemaSaudeGrave()
    {
        gameManager.SetSaude(
            Mathf.Max(0, gameManager.GetSaude() - 10)
        );

        popUpInfo.Mostrar(
            "Problema de Saúde Grave",
            "Você enfrentou um problema de saúde grave.\n-10 Saúde",
            spritesEventos[11]
        );
    }

    // =========================
    // Eventos Relacionados a Finanças.
    // =========================
    public void EventoAchouDinheiro()
    {
        int valor = Random.Range(0, 100);

        gameManager.AdicionarDinheiro(valor);

        popUpInfo.Mostrar(
            "Achou Dinheiro",
            "você encontrou dinheiro na rua.\n+R$ " + valor.ToString("F2"),
            spritesEventos[1]
        );
    }

    public void EventoPresente()
    {
        int valor = Random.Range(50, 300);

        gameManager.AdicionarDinheiro(valor);
        gameManager.SetSanidade(Mathf.Min(100, gameManager.GetSanidade() + 8));

        popUpInfo.Mostrar(
            "Presente Inesperado",
            "Um familiar lembrou do seu aniversário.\n+R$ " +
            valor.ToString("F2"),
            spritesEventos[8]
        );
    }

    public void EventoCelularQuebrou()
    {
        float valor = Random.Range(300f, 1001f);

        if(valor > gameManager.GetDinheiro())
        {
            float restante = valor - gameManager.GetDinheiro();
            gameManager.RemoverDinheiro(gameManager.GetDinheiro());
            gameManager.SetDivida(gameManager.GetDivida() + restante);
        }

        else
        {
            gameManager.RemoverDinheiro(valor);
        }
        
        popUpInfo.Mostrar(
            "Celular Quebrou",
            "Seu celular apresentou defeito.\n-R$ " + valor.ToString("F2"),
            spritesEventos[4]
        );
    }

    public void EventoMulta()
    {
        float valor = Random.Range(100f, 1501f);

         if(valor > gameManager.GetDinheiro())
        {
            float restante = valor - gameManager.GetDinheiro();
            gameManager.RemoverDinheiro(gameManager.GetDinheiro());
            gameManager.SetDivida(gameManager.GetDivida() + restante);
        }
        else
        {
            gameManager.RemoverDinheiro(valor);
        }

        popUpInfo.Mostrar(
            "Multa Inesperada",
            "Você recebeu uma multa.\n-R$ " + valor.ToString("F2"),
            spritesEventos[1]
        );
    }

    public void EventoAssalto()
    {
        float perda = Random.Range(200f, 1501f);

        perda = Mathf.Min(perda, gameManager.GetDinheiro());

        gameManager.RemoverDinheiro(perda);

        gameManager.SetSanidade(
            Mathf.Max(0, gameManager.GetSanidade() - 10)
        );

        popUpInfo.Mostrar(
            "Assalto",
            "Você foi assaltado.\n-R$ " +
            perda.ToString("F2") +
            " -10 Sanidade",
            spritesEventos[7]
        );
    }

    public void EventoAumentoJuros()
    {
        gameManager.SetMultiplicadorJuros(gameManager.GetMultiplicadorJuros() + 0.01f);
        gameManager.AplicarJuros();
        gameManager.AtualizarValorParcela();

        popUpInfo.Mostrar(
            "Aumento dos Juros",
            "As taxas de juros aumentaram.\n + 1%.",
            spritesEventos[9]
        );
    }

    public void EventoHerdouDivida()
    {   
        if (gameManager.GetQuantidadeHerdouDivida() >= 2)
            return;

        gameManager.SetQuantidadeHerdouDivida(gameManager.GetQuantidadeHerdouDivida() + 1);
        float valor = Random.Range(5000f, 15000f);

        gameManager.SetDivida(
            gameManager.GetDivida() + valor
        );

        popUpInfo.Mostrar(
            "Herdou uma Dívida",
            "Uma pendência financeira caiu sobre você.\n+R$ "
            + valor.ToString("F0") + " de dívida.",
            spritesEventos[9]
        );
    }

    // =========================
    // Métodos para eventos de dificuldade.
    // =========================

    private void EventoPositivo()
    {   
        if(gameManager.GetdiasDesdeUltimoEventoPositivo() < 8) return;

        int evento = Random.Range(0, 6);

        if(evento == ultimoEventoPositivo)
        {
            evento = Random.Range(0, 6);
        }

        ultimoEventoPositivo = evento;

        switch(evento)
        {
            case 0: EventoPromocao(); break;
            case 1: EventoAchouDinheiro(); break;
            case 2: EventoDiaBom(); break;
            case 3: EventoAmigos(); break;
            case 4: EventoElogioChefe(); break;
            case 5: EventoPresente(); break;
        }
        gameManager.SetdiasDesdeUltimoEventoPositivo(0);
    }

    private void EventoNegativo()
    {   
        if (gameManager.GetdiasDesdeUltimoEventoNegativo() < 30){
            return;
        } 

        int evento = Random.Range(0, 4);

        switch(evento)
        {
            case 0: EventoCelularQuebrou(); break;
            case 1: EventoMulta(); break;
            case 2: 
                if (gameManager.GetSanidade() < 50)
                    EventoCriseEmocional();
                else
                    EventoDiaEstressante(); break;

            case 3: EventoAssalto(); break;
            case 4: 
                if (gameManager.GetSanidade() < 50)
                    EventoProblemaSaudeGrave();
                else
                    EventoProblemaSaudeLeve();
                break;
        }

        gameManager.SetdiasDesdeUltimoEventoNegativo(0);
    }

    private void EventoCatastrofe()
    {
        if (gameManager.GetdiasDesdeUltimaCatastrofe() <= 120)
            return;

        int evento = Random.Range(0, 3);
        switch(evento)
        {
            case 0: EventoAumentoJuros(); break;
            case 1: EventoHerdouDivida(); break;
            case 2: EventoDemissao(); break;
        }

        gameManager.SetdiasDesdeUltimaCatastrofe(0);
    }
}