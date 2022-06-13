

using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;

namespace PhoneMarketing.FileManager
{
    internal class CsvFileService : IFileService
    {
        private ILogger<CsvFileService> _logger;
        public CsvFileService(ILogger<CsvFileService> logger)
        {
            _logger = logger;
        } 

        public async Task<List<string>> ReadFile(string fileName)
        {
            //Take Data from file
           List<string> list = new List<string>();
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.UTF8,
                    Delimiter = ",",
                    HasHeaderRecord = false,
                    IgnoreBlankLines = true
                };

                using (TextReader fileReader = File.OpenText(fileName))
                {

                    var csv = new CsvReader(fileReader, configuration);

                    while (await csv.ReadAsync())
                    {
                        for (int i = 0; csv.TryGetField<string>(i, out string value); i++)
                        {
                            list.Add(value);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string message = $"Exception in Class = CsvFileService, Method = ReadFile, Message = {ex.Message}";
                _logger.LogError(message, ex.ToString());
            }
            return list;
        }
    }
}
