

using CsvHelper;
using CsvHelper.Configuration;
using PhoneMarketing.Models;
using System.Globalization;
using System.Text;

namespace PhoneMarketing.FileManager
{
    internal class CsvFileService : IFileService
    {
         

        public List<string> ReadFile(string fileName)
        {
            //Take Data from file
           List<string> list = new List<string>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8, 
                Delimiter = "," ,
                HasHeaderRecord=false,
                IgnoreBlankLines=true
            };
            using (TextReader fileReader = File.OpenText(fileName))
            {

                var csv = new CsvReader(fileReader, configuration);
                while (csv.Read())
                {
                    for (int i = 0; csv.TryGetField<string>(i, out string value); i++)
                    {
                        list.Add(value);
                    }
                }
                
            }
            return list;
        }
    }
}
