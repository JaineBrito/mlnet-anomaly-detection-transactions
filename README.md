# ML.NET Anomaly Detection for Transactions

Idioma: **Portugues** | [English](README.en.md)

Projeto de estudo em C# com ML.NET para detectar anomalias em transacoes de cartao.

O objetivo e praticar o fluxo de Machine Learning no .NET:
- carregar dados CSV;
- treinar modelo de deteccao de anomalias;
- analisar compras marcadas como anormais;
- salvar/carregar modelo;
- prever novas transacoes.

## Estrutura do projeto

- `mlnet-anomaly-detection-transactions`
  Biblioteca com a logica de ML (treino, analise, persistencia e predicao).
- `mlnet-anomaly-detection-transactions-console`
  Aplicacao de console que executa o fluxo de ponta a ponta.

### Arquivos principais

- `mlnet-anomaly-detection-transactions-console/Program.cs`
  Orquestra todo o processo (carregar dados -> treinar -> analisar -> salvar -> prever).
- `mlnet-anomaly-detection-transactions/ML/ComprasCartaoModelTrainer.cs`
  Contem os metodos de carregamento dos dados, treino, analise de anomalias e salvamento.
- `mlnet-anomaly-detection-transactions/ML/ComprasCartaoModelPredictor.cs`
  Carrega o modelo salvo e faz previsoes para novas compras.
- `mlnet-anomaly-detection-transactions/Models/ComprasCartaoInputData.cs`
  Define as colunas de entrada do CSV.
- `mlnet-anomaly-detection-transactions/Models/ComprasCartaoPredictionResult.cs`
  Define a saida da predicao (`EhAnormal` e `Score`).

## Como os dados sao lidos

O dataset CSV e carregado por `LoadFromTextFile<ComprasCartaoInputData>()` com:
- cabecalho (`hasHeader: true`);
- separador virgula (`separatorChar: ','`).

Mapeamento de colunas no `ComprasCartaoInputData`:
- coluna `0`: `ValorCompra`;
- coluna `1`: `Parcelado`;
- coluna `2`: `HoraCompra`.

## Pipeline de treino

No `ComprasCartaoModelTrainer`:

1. Concatena os campos de entrada em `Features`.
2. Normaliza os atributos com `NormalizeMinMax`.
3. Treina deteccao de anomalias com `RandomizedPca` (`rank: 2`).
4. Aplica o modelo para identificar compras anormais e exibir o `Score`.

## Salvar e carregar modelo

- `SalvarModelo(path)` salva o modelo treinado em arquivo `.zip`.
- `CarregarModelo(path)` recupera esse modelo para uso futuro.

Isso permite separar treino e inferencia.

## Predicao

A predicao e feita com:
- `CreatePredictionEngine<ComprasCartaoInputData, ComprasCartaoPredictionResult>()`
- `Predict(novaCompra)`

Exemplo do projeto:
- entrada: valor da compra, se foi parcelada e horario da compra;
- saida: `EhAnormal` e `Score`.

## Como executar

Na raiz do repositorio:

```bash
dotnet restore "mlnet-anomaly-detection-transactions.sln"
dotnet build "mlnet-anomaly-detection-transactions.sln" -c Debug
dotnet run --project "mlnet-anomaly-detection-transactions-console/mlnet-anomaly-detection-transactions-console.csproj"
```

## Dependencias

No projeto de biblioteca:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Observacoes de estudo

- O projeto e focado em aprendizado, nao em producao.
- Para evoluir, voce pode:
  - adicionar separacao treino/teste;
  - testar diferentes tecnicas de normalizacao;
  - experimentar outros detectores de anomalias;
  - ajustar parametros do `RandomizedPca` para comparar resultados.