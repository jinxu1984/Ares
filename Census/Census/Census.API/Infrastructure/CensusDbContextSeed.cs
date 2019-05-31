using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using System.Data.SqlClient;
using Census.API.Model;
using System.IO;
using Census.API.Infrastructure.Utilities;

namespace Census.API.Infrastructure
{
    public class CensusDbContextSeed
    {
        private readonly string seedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data.xlsx");
        private readonly string WorksheetNameForActuals = "Actuals";
        private readonly string WorksheetNameForEstimates = "Estimates";

        public async Task SeedAsync(CensusDbContext context, ILogger<CensusDbContextSeed> logger, int retries = 3)
        {
            var policy = CreatePolicy(retries, logger, nameof(CensusDbContextSeed));

            await policy.Execute(async () =>
            {
                await SeedDataSetAsync<ActualCensusEntity>(context, WorksheetNameForActuals);
                await SeedDataSetAsync<EstimatedCensusEnity>(context, WorksheetNameForEstimates);
            });
        }

        private async Task SeedDataSetAsync<TEntity>(CensusDbContext context, string WorksheetName) 
            where TEntity : class, new()
        {
            if (!context.Set<TEntity>().Any())
            {
                var entities = ExcelUtility.ReadFromWorksheet<TEntity>(seedFilePath, WorksheetName);
                await context.Set<TEntity>().AddRangeAsync(entities);

                await context.SaveChangesAsync();
            }
        }

        private Policy CreatePolicy(int retries, ILogger<CensusDbContextSeed> logger, string prefix)
        {
            return Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
