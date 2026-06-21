using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia { get; private set; }
    [Header("Interface Control")]
    // =========================
    // Economia
    // =========================

    private float dinheiro;
    private float rendaBase;
    private float divida;
    private float parcelaDivida;
    private float parcelaMinima = 400;
    private int tempoAteProximaParcela = 30;

    // =========================
    // Player
    // =========================

    private float sanidade = 100;
    private float saude = 100;
    private float desgasteEstudos;
    // =========================
    // Tempo
    // =========================

    private int dia = 1;
    private int mes = 1;
    private int ano = 2026;

    private int diasPassados = 0;
    // =========================
    // Multiplicadores
    // =========================

    private float multiplicadorDivida;
    private float multiplicadorJuros;
    private float recuperacaoSanidade;
    private float custoDeUpgrade;
    private float multiplicadorRenda;

    // =========================
    // Eventos
    // =========================

    private float eventosNegativos;
    private bool venceu = false; 
    private bool perdeuSanidade = false;
    private bool perdeuVida = false;
    private bool querContinuar = false; 

    private int diasDesdeUltimoEventoPositivo = 7;
    private int diasDesdeUltimoEventoNegativo = 0;
    private int diasDesdeUltimaCatastrofe = 0;
    private int quantidadeHerdouDivida = 0;
    // =========================
    // Trabalho e Upgrades
    // =========================
    
    private int nivelTecnicas = 0;
    private int nivelProdutividade = 0;
    private int nivelComunicacao = 0;

    private int trabalhoAtual = 0; 
    // 0 = Sem trabalho, 1 = faxineiro, 2 = Entregado, 3 = Vendedor, 4 = Programador , 5 = Gerente, 6 = CEO
    
    private List<int> Diastrabalhado = new List<int>();
    
    private int profissaoPendente = 0;
    private int diasParaProfissao = 0;

    private string cargoAtual = "";

    // =========================
    // Estudos e Descanso
    // =========================

    private bool possuiHabilitacao;
    private bool possuiCursoBasico;
    private bool possuiFaculdadeTecnologia;
    private bool possuiCursoAdministracao;

    private List<int> cursosEmAndamento = new List<int>();
    private List<int> diasRestantesCurso = new List<int>();

    private List<int> descansosEmAndamento = new List<int>();
    private List<int> diasRestantesDescanso = new List<int>();

    private float bonusDescansoSanidade;
    private float bonusDescansoVida;
    // =========================
    // Singleton
    // =========================

    private void Awake()
    {   
        for(int i = 0; i < 7; i++)
        {
            Diastrabalhado.Add(0);
        }

        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // =========================
    // Dinheiro
    // =========================

    public float GetDinheiro()
    {
        return dinheiro;
    }

    public void SetDinheiro(float valor)
    {
        dinheiro = valor;
    }

    public void AdicionarDinheiro(float valor)
    {
        dinheiro += valor;
    }

    public void RemoverDinheiro(float valor)
    {
        dinheiro -= valor;
    }

    // =========================
    // Renda Diária
    // =========================

    public float GetRendaDiaria()
    {
        return rendaBase * GetBonusCargo() * (1 + multiplicadorRenda);
    }

    public void SetRendaDiaria(float valor)
    {
        rendaBase = valor;
    }

    // =========================
    // Dívida / Finanças
    // =========================

    public float GetDivida()
    {
        return divida;
    }

    public void SetDivida(float valor)
    {
        divida = valor;
        
        AtualizarValorParcela();
    }

    public float GetParcelaDivida()
    {
        return parcelaDivida;
    }

    public void SetParcelaDivida(float valor)
    {
        parcelaDivida = valor;
    }

    public int GetTempoAteProximaParcela()
    {
        return tempoAteProximaParcela;
    }

    public void SetTempoAteProximaParcela(int valor)
    {
        tempoAteProximaParcela = valor;
    }


    // =========================
    // Sanidade
    // =========================

    public float GetSanidade()
    {
        return sanidade;
    }

    public void SetSanidade(float valor)
    {
        sanidade = valor;
    }

    // =========================
    // Saúde
    // =========================

    public float GetSaude()
    {
        return saude;
    }

    public void SetSaude(float valor)
    {
        saude = valor;
    }

    // =========================
    // Data
    // =========================

    public string GetData()
    {
        return dia.ToString("00") + "/" +mes.ToString("00") + "/" +ano;
    }

    public int GetDiasPassados()
    {
        return diasPassados;
    }


    // =========================
    // Multiplicador Dívida
    // =========================

    public float GetMultiplicadorDivida()
    {
        return multiplicadorDivida;
    }

    public void SetMultiplicadorDivida(float valor)
    {
        multiplicadorDivida = valor;
    }

    // =========================
    // Multiplicador Juros
    // =========================

    public float GetMultiplicadorJuros()
    {
        return multiplicadorJuros;
    }

    public void SetMultiplicadorJuros(float valor)
    {
        multiplicadorJuros = valor;
    }

    // =========================
    // Recuperação Sanidade
    // =========================

    public float GetRecuperacaoSanidade()
    {
        return recuperacaoSanidade;
    }

    public void SetRecuperacaoSanidade(float valor)
    {
        recuperacaoSanidade = valor;
    }

    // =========================
    // Custo Upgrade
    // =========================

    public float GetCustoDeUpgrade()
    {
        return custoDeUpgrade;
    }

    public void SetCustoDeUpgrade(float valor)
    {
        custoDeUpgrade = valor;
    }

    // =========================
    // Eventos Negativos
    // =========================

    public float GetEventosNegativos()
    {
        return eventosNegativos;
    }

    public void SetEventosNegativos(float valor)
    {
        eventosNegativos = valor;
    }

    // =========================
    // Sistema de Tempo
    // =========================

    public void AvancarDia()
    {
        dia++;
        diasPassados++;

        if (dia > 30)
        {
            dia = 1;

            mes++;
        }

        if (mes > 12)
        {
            mes = 1;

            ano++;
        }

        AdicionarDinheiro(GetRendaDiaria());
        AtualizarParcela();
        AtualizarSanidade();
        AtualizarSaude();
        AdicionarDiasTrabalhados(trabalhoAtual, 1);
        AtualizarProcessoSeletivo();
        AtualizarCursos();
        AtualizarDescansos();
        AddDiasDesdeUltimosEventos();
    }

    // =========================
    // Sistema de Parcelas
    // =========================

    private void AtualizarParcela()
    {
        tempoAteProximaParcela--;

        if (tempoAteProximaParcela <= 0)
        {
            CobrarParcela();

            tempoAteProximaParcela = 30;
        }
    }

    private void CobrarParcela()
    {
        
        // Não conseguiu pagar tudo
        if (dinheiro < parcelaDivida)
        {
            divida -= dinheiro;
            dinheiro = 0;
            
            divida += parcelaDivida * 0.05f; // Juros de 5% se falhar pagamento
            AplicarJuros();
            AtualizarValorParcela();
        }

        else
        {
            dinheiro -= parcelaDivida;

            divida -= parcelaDivida;

            
            if (divida < 0)
            {
                divida = 0;
            }

            AplicarJuros();
            AtualizarValorParcela();
        }

    }

    public void AtualizarValorParcela()
    {
        parcelaDivida = divida * multiplicadorDivida;

        if(parcelaDivida < parcelaMinima && divida > 0)
        {
            parcelaDivida = parcelaMinima;
        }

        if(parcelaDivida > divida)
        {
            parcelaDivida = divida;
        }
    }

    public void AplicarJuros()
    {
        float juros = divida * (multiplicadorJuros / 12f);
        divida += juros;
    }

    // ========================= 
    // Sistema de Sanidade e Saúde
    // =========================
    public float CalcularpressaoFinanceira()
    {
        float renda = Mathf.Max(GetRendaDiaria(), 1);
        float pressaoFinanceira = divida / renda ;
        pressaoFinanceira = Mathf.Min(pressaoFinanceira, 1000);
        return pressaoFinanceira;
    }
    
    private void AtualizarSanidade()
    {
        // Recuperação
        if (sanidade < 100)
        {
            sanidade += recuperacaoSanidade + bonusDescansoSanidade;
        }   

        if (diasPassados >= 30)
        {
            sanidade -= (CalcularpressaoFinanceira()/250) + desgasteEstudos;
        }

        // Limites
        sanidade = Mathf.Clamp(sanidade, 0, 100);
    }

    private void AtualizarSaude()
    {
        // =========================
        // Recuperação natural
        // =========================

        if (saude < 100 && sanidade > 60)
        {
            saude += 1 + bonusDescansoVida;
        }

        // =========================
        // Impacto da sanidade
        // =========================

        if (sanidade < 50)
        {
            saude -= 1;
        }

        if (sanidade < 25)
        {
            saude -= 2;
        }

        // =========================
        // Limites
        // =========================

        saude = Mathf.Clamp(saude, 0, 100);
    }

    // =========================
    // Get e Set para Upgrades
    // =========================

    public int GetNivelTecnicas()
    {
        return nivelTecnicas;
    }

    public void SetNivelTecnicas(int valor)
    {
        nivelTecnicas = valor;
    }

    public int GetNivelProdutividade()
    {
        return nivelProdutividade;
    }

    public void SetNivelProdutividade(int valor)
    {
        nivelProdutividade = valor;
    }

    public int GetNivelComunicacao()
    {
        return nivelComunicacao;
    }

    public void SetNivelComunicacao(int valor)
    {
        nivelComunicacao = valor;
    }
    
    public float GetMultiplicadorRenda()
    {
        return multiplicadorRenda;
    }

    public void SetMultiplicadorRenda(float valor)
    {
        multiplicadorRenda = valor;
    }

    public void SetTrabalhoAtual(int valor)
    {
        trabalhoAtual = valor;

        switch(valor)
        {
            case 0:
                rendaBase = 0;
                break;

            case 1:
                rendaBase = 40;
                break;

            case 2:
                rendaBase = 60;
                break;

            case 3:
                rendaBase = 80;
                break;

            case 4:
                rendaBase = 150;
                break;

            case 5:
                rendaBase = 300;
                break;

            case 6:
                rendaBase = 750;
                break;
        }

        AtualizarCargo();
    }

    public int GetTrabalhoAtual()
    {
        return trabalhoAtual;
    }

    public void AdicionarDiasTrabalhados(int trabalho, int dias)
    {
        if (trabalho >= 0 && trabalho < Diastrabalhado.Count)
        {
            Diastrabalhado[trabalho] += dias;

            if(trabalho == trabalhoAtual)
            {
                AtualizarCargo();
            }

        }
    }
    
    public int GetDiasTrabalhados(int trabalho)
    {
        if (trabalho >= 0 && trabalho < Diastrabalhado.Count)
        {
            return Diastrabalhado[trabalho];
        }
        return 0;
    }

    public bool GetPossuiHabilitacao()
    {
        return possuiHabilitacao;
    }

    public void SetPossuiHabilitacao(bool valor)
    {
        possuiHabilitacao = valor;
    }

    public bool GetPossuiCursoBasico()
    {
        return possuiCursoBasico;
    }

    public void SetPossuiCursoBasico(bool valor)
    {
        possuiCursoBasico = valor;
    }

    public bool GetPossuiFaculdadeTecnologia()
    {
        return possuiFaculdadeTecnologia;
    }

    public void SetPossuiFaculdadeTecnologia(bool valor)
    {
        possuiFaculdadeTecnologia = valor;
    }

    public bool GetPossuiCursoAdministracao()
    {
        return possuiCursoAdministracao;
    }

    public void SetPossuiCursoAdministracao(bool valor)
    {
        possuiCursoAdministracao = valor;
    }

    public int GetProfissaoPendente()
    {
        return profissaoPendente;
    }

    public void SetProfissaoPendente(int valor)
    {
        profissaoPendente = valor;
    }

    public int GetDiasParaProfissao()
    {
        return diasParaProfissao;
    }

    public void SetDiasParaProfissao(int valor)
    {
        diasParaProfissao = valor;
    }

    private void AtualizarProcessoSeletivo()
    {
        if (diasParaProfissao > 0)
        {
            diasParaProfissao--;

            if (diasParaProfissao <= 0)
            {
                SetTrabalhoAtual(profissaoPendente);
                profissaoPendente = 0;
                diasParaProfissao = 0;
            }
        }
    }

    private void AtualizarCargo()
    {
        int dias = GetDiasTrabalhados(trabalhoAtual);

        switch (trabalhoAtual)
        {
            
            case 1:
                if(dias >= 180) cargoAtual = "Supervisor";
                else if(dias >= 90) cargoAtual = "Experiente";
                else if(dias >= 30) cargoAtual = "Efetivo";
                else cargoAtual = "Novato";
                break;

            case 2: // Entregador

                if(dias >= 180) cargoAtual = "Coordenador";
                else if(dias >= 90) cargoAtual = "Senior";
                else if(dias >= 30) cargoAtual = "Junior";
                else cargoAtual = "Iniciante";
                break;
            
            case 3: // Vendedor
                if(dias >= 180) cargoAtual = "Gerente de Vendas";
                else if(dias >= 90) cargoAtual = "Vendedor Sênior";
                else if(dias >= 30) cargoAtual = "Vendedor Júnior";
                else cargoAtual = "Vendedor";
                break;
            
            case 4: // Programador
                if(dias >= 180) cargoAtual = "Desenvolvedor Sênior";
                else if(dias >= 90) cargoAtual = "Desenvolvedor Pleno";
                else if(dias >= 30) cargoAtual = "Desenvolvedor Júnior";
                else cargoAtual = "Programador";
                break;

            case 5: // Gerente
                if(dias >= 180) cargoAtual = "Diretor";
                else if(dias >= 90) cargoAtual = "Gerente Sênior";
                else if(dias >= 30) cargoAtual = "Gerente Júnior";
                else cargoAtual = "Gerente";
                break;

            case 6: // CEO
                if(dias >= 180) cargoAtual = "Presidente";
                else if(dias >= 90) cargoAtual = "CEO Sênior";
                else if(dias >= 30) cargoAtual = "CEO Júnior";
                else cargoAtual = "CEO";
                break;
        }
    }

    public string GetCargoAtual()
    {
        return cargoAtual;
    }

    public float GetBonusCargo()
    {
        int dias = GetDiasTrabalhados(trabalhoAtual);

        if(dias >= 180) return 1.5f;
        if(dias >= 90) return 1.25f;
        if(dias >= 30) return 1.1f;

        return 1f;
    }

    public int GetDiasParaProximoCargo()
    {
        int dias = GetDiasTrabalhados(trabalhoAtual);

        if (dias < 30) return 30 - dias;
        if (dias < 90) return 90 - dias;
        if (dias < 180) return 180 - dias;

        return 0; // Cargo máximo
    }

    public void AdicionarCurso(int curso, int dias)
    {
        cursosEmAndamento.Add(curso);
        diasRestantesCurso.Add(dias);

        switch(curso)
        {
            case 0: // Habilitação
                desgasteEstudos += 0.05f;
                break;

            case 1: // Curso Básico
                desgasteEstudos += 0.15f;
                break;

            
            case 2: // Faculdade
                desgasteEstudos += 0.25f;
                break;

            case 3: // Administração
                desgasteEstudos += 0.35f;
                break;
        }
    }

    private void AtualizarCursos()
    {
        for(int i = diasRestantesCurso.Count - 1; i >= 0; i--)
        {
            diasRestantesCurso[i]--;

            if(diasRestantesCurso[i] <= 0)
            {
                ConcluirCurso(cursosEmAndamento[i]);

                cursosEmAndamento.RemoveAt(i);
                diasRestantesCurso.RemoveAt(i);
            }
        }
    }

    private void ConcluirCurso(int curso)
    {
        switch(curso)
        {
            case 0:
                desgasteEstudos -= 0.05f;
                possuiHabilitacao = true;
                multiplicadorRenda += 0.01f;
                break;

            case 1:
                desgasteEstudos -= 0.10f;
                possuiCursoBasico = true;
                multiplicadorRenda += 0.15f;
                break;

            case 2:
                desgasteEstudos -= 0.25f;
                possuiFaculdadeTecnologia = true;
                multiplicadorRenda += 0.10f;
                break;

            case 3:
                desgasteEstudos -= 0.35f;
                possuiCursoAdministracao = true;
                multiplicadorRenda += 0.25f;
                break;

        }
    }

    public bool CursoEmAndamento(int curso)
    {
        return cursosEmAndamento.Contains(curso);
    }

    public string GetCursosEmAndamento()
    {
        string resultado = "";

        for (int i = 0; i < cursosEmAndamento.Count; i++)
        {
            resultado += NomeCurso(cursosEmAndamento[i]) +
                        " (" + diasRestantesCurso[i] + " dias)\n";
        }

        if (resultado == "")
        {
            resultado = "Nenhum curso em andamento";
        }

        return resultado;
    }

    public string NomeCurso(int curso)
    {
        switch (curso)
        {
            case 0: return "Habilitação";
            case 1: return "Curso Básico";
            case 2: return "Faculdade Tecnologia";
            case 3: return "Administração";
        }

        return "Desconhecido";
    }

    public float GetDesgasteEstudos()
    {
        return desgasteEstudos;
    }

    public void addDesgasteEstudos(float valor)
    {
        desgasteEstudos += valor;
    }

    public void AdicionarDescanso(int descanso, int dias)
    {
        descansosEmAndamento.Add(descanso);
        diasRestantesDescanso.Add(dias);

        switch(descanso)
        {
            case 0: 
                bonusDescansoSanidade += 0.75f;
                bonusDescansoVida += 0.25f;
                break;
            case 1: 
                bonusDescansoSanidade += 1f; 
                bonusDescansoVida += 0.50f;
                break;
            case 2: 
                bonusDescansoSanidade += 2f; 
                break;
            case 3: 
                bonusDescansoSanidade += 1.5f; 
                bonusDescansoVida += 1f;
                break;
        }
    }

    private void AtualizarDescansos()
    {
        for(int i = diasRestantesDescanso.Count - 1; i >= 0; i--)
        {
            diasRestantesDescanso[i]--;

            if(diasRestantesDescanso[i] <= 0)
            {
                FinalizarDescanso(descansosEmAndamento[i]);

                descansosEmAndamento.RemoveAt(i);
                diasRestantesDescanso.RemoveAt(i);
            }
        }
    }

    public float GetBonusDescansoSanidade()
    {
        return bonusDescansoSanidade;
    }

    private void FinalizarDescanso(int descanso)
    {
        switch(descanso)
        {
            case 0: 
                bonusDescansoSanidade -= 0.75f;
                bonusDescansoVida -= 0.25f;
                break;
            case 1: 
                bonusDescansoSanidade -= 1f; 
                bonusDescansoVida -= 0.50f;
                break;
            case 2: 
                bonusDescansoSanidade -= 2f; 
                break;
            case 3: 
                bonusDescansoSanidade -= 1.5f; 
                bonusDescansoVida -= 1f;
                break;
        }
    }

    public bool DescansoEmAndamento(int descanso)
    {
        return descansosEmAndamento.Contains(descanso);
    }

    public string GetDescansoEmAndamento()
    {
        string resultado = "";

        for (int i = 0; i < descansosEmAndamento.Count; i++)
        {
            resultado += NomeDescansos(descansosEmAndamento[i]) +
                        " (" + diasRestantesDescanso[i] + " dias)\n";
        }

        if (resultado == "")
        {
            resultado = "Nenhum Lazer em andamento";
        }

        return resultado;
    }

    public string NomeDescansos(int descanso)
    {
        switch (descanso)
        {
            case 0: return "Ir ao Parque";
            case 1: return "Sono Revigorante";
            case 2: return "Terapia";
            case 3: return "Academia";
        }

        return "Desconhecido";
    }

    public bool GetVenceu()
    {
        return venceu;
    }

    public bool GetQuerContinuar()
    {
        return querContinuar;
    }

    public void SetVenceu(bool valor)
    {
        venceu = valor;
    }

    public void SetQuerContinuar(bool valor)
    {
        querContinuar = valor;
    }

    public bool GetPerdeuSanidade()
    {
        return perdeuSanidade;
    }

    public bool GetPerdeuVida()
    {
        return perdeuVida;
    }

    public void SetPerdeuSanidade(bool valor)
    {
        perdeuSanidade = valor;
    }

    public void SetPerdeuVida(bool valor)
    {
        perdeuVida = valor;
    }

    public int GetdiasDesdeUltimoEventoPositivo()
    {
        return diasDesdeUltimoEventoPositivo;
    }

    public int GetdiasDesdeUltimoEventoNegativo()
    {
        return diasDesdeUltimoEventoNegativo;
    }

    public int GetdiasDesdeUltimaCatastrofe()
    {
        return diasDesdeUltimaCatastrofe;
    }


    public void SetdiasDesdeUltimoEventoPositivo(int valor)
    {
        diasDesdeUltimoEventoPositivo = valor;
    }

    public void SetdiasDesdeUltimoEventoNegativo(int valor)
    {
        diasDesdeUltimoEventoNegativo = valor;
    }

    public void SetdiasDesdeUltimaCatastrofe(int valor)
    {
        diasDesdeUltimaCatastrofe = valor;
    }

    public int GetQuantidadeHerdouDivida()
    {
        return quantidadeHerdouDivida;
    }

    public void SetQuantidadeHerdouDivida(int valor)
    {
        quantidadeHerdouDivida = valor;
    }

    public void AddDiasDesdeUltimosEventos()
    {   
        diasDesdeUltimoEventoPositivo++;
        diasDesdeUltimoEventoNegativo++;
        diasDesdeUltimaCatastrofe++;
    }

    public void GameReset()
    {
        dinheiro = 0;
        rendaBase = 0;
        divida = 0;
        parcelaDivida = 0;
        tempoAteProximaParcela = 30;

        sanidade = 100;
        saude = 100;
        desgasteEstudos = 0;

        dia = 1;
        mes = 1;
        ano = 2026;
        diasPassados = 0;

        multiplicadorDivida = 0.05f;
        multiplicadorJuros = 0.05f;
        recuperacaoSanidade = 1f;
        custoDeUpgrade = 0.10f;
        multiplicadorRenda = 0f;

        eventosNegativos = 10f;
        venceu = false; 
        perdeuSanidade = false;
        perdeuVida = false;
        querContinuar = false; 
        diasDesdeUltimoEventoPositivo = 7;
        diasDesdeUltimoEventoNegativo = 0;
        diasDesdeUltimaCatastrofe = 0;
        quantidadeHerdouDivida = 0;

        nivelTecnicas = 0;
        nivelProdutividade = 0;
        nivelComunicacao = 0;

        trabalhoAtual = 0; 
        
        for(int i = 0; i < Diastrabalhado.Count; i++)
        {
            Diastrabalhado[i] = 0;
        }
        
        profissaoPendente = 0;
        diasParaProfissao = 0;

        cargoAtual = "";

        possuiHabilitacao = false;
        possuiCursoBasico = false;
        possuiFaculdadeTecnologia = false;
        possuiCursoAdministracao = false;

        cursosEmAndamento.Clear();
        diasRestantesCurso.Clear();

        descansosEmAndamento.Clear();
        diasRestantesDescanso.Clear();

        bonusDescansoSanidade = 0f;
        bonusDescansoVida = 0f;
    }

}