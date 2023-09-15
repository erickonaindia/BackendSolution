using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Solution.Domain.Entities;
using Solution.Persistence.Contexts;
using System.Globalization;

namespace Solution.Persistence.Services
{
    public class CsvReaderService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CsvReaderService> _logger;

        public CsvReaderService(IServiceProvider serviceProvider, ILogger<CsvReaderService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void ReadCsvFile(string filePath)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<CsvRecord>();

                    foreach (var record in records)
                    {
                        try
                        {
                            var category = dbContext.Categories.SingleOrDefault(c => c.Code == record.CATEGORY_CODE);
                            if (category == null)
                            {
                                category = new Category
                                {
                                    Name = record.CATEGORY_NAME,
                                    Code = record.CATEGORY_CODE,
                                    Created = DateTime.Now
                                };
                                dbContext.Categories.Add(category);
                            }

                            var product = dbContext.Products.SingleOrDefault(p => p.Code == record.PRODUCT_CODE);
                            if (product == null)
                            {
                                product = new Product
                                {
                                    CategoryId = category.Id,
                                    Name = record.PRODUCT_NAME,
                                    Code = record.PRODUCT_CODE,
                                    CategoryCode = record.CATEGORY_CODE,
                                    Created = DateTime.Now
                                };
                                dbContext.Products.Add(product);
                            }

                            dbContext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Error processing CSV record: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
