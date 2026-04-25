using MachineLearning.ML;
using MachineLearning.Models;

ProjetoAnomalias();

void ProjetoAnomalias()
{
    var trainer = new ComprasCartaoModelTrainer();
     trainer.CarregarDadosCSV(Path.Combine(AppContext.BaseDirectory, "compras_cartao.csv"));
     trainer.TreinarModelo();
     trainer.AnalisarAnomalias();
    
    var pathModelo = Path.Combine(AppContext.BaseDirectory, "modelo-anomalias.zip");
    trainer.SalvarModelo(pathModelo);

     var predictor = new ComprasCartaoModelPredictor();
    predictor.CarregarModelo(pathModelo);

    var novaCompra = new ComprasCartaoInputData()
    {
        ValorCompra = 30000,
        Parcelado = 1,
        HoraCompra = 23
    };

    var resultado = predictor.Prever(novaCompra);
     Console.WriteLine($"Anomalia: {(resultado.EhAnormal ? "Sim" : "Não")}");
      Console.WriteLine($"Score: {resultado.Score:F4}");
}
