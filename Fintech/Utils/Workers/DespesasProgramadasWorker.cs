using Fintech.Interfaces;

namespace Fintech.Utils.Workers;

public class DespesasProgramadasWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DespesasProgramadasWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Get today's date at midnight for comparison
                Console.WriteLine("Despesas de programadas");
                var today = DateTime.Now.Date;

                using var scope = _serviceProvider.CreateScope();

                var despesasService = scope.ServiceProvider.GetRequiredService<IDespesaProgramadaService>();

                await despesasService.CheckForAvailableProgramadas();

                // Calculate time until next day at midnight
                var tomorrow = DateTime.Now.AddDays(1);
                var delay = tomorrow.Subtract(today);

                // wait until tomorrow
                await Task.Delay(delay, stoppingToken);
            }
            catch (Exception ex)
            {
                // Log the error but don't stop the service
                // TODO: Add proper logging
                Console.WriteLine($"Error processing scheduled expenses: {ex.Message}");
                
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}