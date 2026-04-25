using System;
using MachineLearning.Models;
using Microsoft.ML;

namespace MachineLearning.ML;

public class ComprasCartaoModelPredictor
{
    private MLContext mLContext = new MLContext();
    private ITransformer modeloCarregado;

    public void CarregarModelo(string path)
    {
        DataViewSchema modeloSchema;
        modeloCarregado = mLContext.Model.Load(path, out modeloSchema);
    }

    public ComprasCartaoPredictionResult Prever(
        ComprasCartaoInputData novaCompra
    )
    {
        var predEgine = mLContext.Model.CreatePredictionEngine<ComprasCartaoInputData, ComprasCartaoPredictionResult>(
         modeloCarregado
        );
        return predEgine.Predict(novaCompra);
    }

}
