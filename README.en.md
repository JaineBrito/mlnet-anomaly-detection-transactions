# ML.NET Anomaly Detection for Transactions

Language: [Portugues](README.md) | **English**

Study project in C# with ML.NET to detect anomalies in card transactions.

The goal is to practice a Machine Learning workflow in .NET:
- load CSV data;
- train an anomaly detection model;
- analyze transactions flagged as abnormal;
- save/load model artifacts;
- predict new transactions.

## Project structure

- `mlnet-anomaly-detection-transactions`
  Class library with ML logic (training, analysis, persistence, and prediction).
- `mlnet-anomaly-detection-transactions-console`
  Console app that runs the end-to-end workflow.

### Main files

- `mlnet-anomaly-detection-transactions-console/Program.cs`
  Orchestrates the full process (load data -> train -> analyze -> save -> predict).
- `mlnet-anomaly-detection-transactions/ML/ComprasCartaoModelTrainer.cs`
  Contains data loading, training, anomaly analysis, and model-saving methods.
- `mlnet-anomaly-detection-transactions/ML/ComprasCartaoModelPredictor.cs`
  Loads the saved model and performs predictions for new transactions.
- `mlnet-anomaly-detection-transactions/Models/ComprasCartaoInputData.cs`
  Defines CSV input columns.
- `mlnet-anomaly-detection-transactions/Models/ComprasCartaoPredictionResult.cs`
  Defines prediction output (`EhAnormal` and `Score`).

## How data is loaded

The CSV dataset is loaded with `LoadFromTextFile<ComprasCartaoInputData>()` using:
- header row (`hasHeader: true`);
- comma separator (`separatorChar: ','`).

Column mapping in `ComprasCartaoInputData`:
- column `0`: `ValorCompra`;
- column `1`: `Parcelado`;
- column `2`: `HoraCompra`.

## Training pipeline

Inside `ComprasCartaoModelTrainer`:

1. Concatenate input fields into `Features`.
2. Normalize features with `NormalizeMinMax`.
3. Train anomaly detection with `RandomizedPca` (`rank: 2`).
4. Apply the model to identify abnormal transactions and print `Score`.

## Save and load model

- `SalvarModelo(path)` saves the trained model to a `.zip` file.
- `CarregarModelo(path)` loads that model for future use.

This allows training and inference to be separated.

## Prediction

Prediction is done with:
- `CreatePredictionEngine<ComprasCartaoInputData, ComprasCartaoPredictionResult>()`
- `Predict(novaCompra)`

Project example:
- input: transaction amount, installment flag, and purchase hour;
- output: `EhAnormal` and `Score`.

## How to run

From repository root:

```bash
dotnet restore "mlnet-anomaly-detection-transactions.sln"
dotnet build "mlnet-anomaly-detection-transactions.sln" -c Debug
dotnet run --project "mlnet-anomaly-detection-transactions-console/mlnet-anomaly-detection-transactions-console.csproj"
```

## Dependencies

In the class library project:
- `Microsoft.ML`
- `Microsoft.ML.LightGbm`
- `Microsoft.ML.AutoML`

## Study notes

- This project is learning-focused, not production-ready.
- To evolve it further, you can:
  - add a train/test split;
  - test different normalization strategies;
  - experiment with other anomaly detection trainers;
  - tune `RandomizedPca` parameters and compare outcomes.