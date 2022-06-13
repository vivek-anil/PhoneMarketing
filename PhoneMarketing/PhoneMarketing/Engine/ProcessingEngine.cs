using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhoneMarketing.FileManager;

namespace PhoneMarketing.Engine
{
    public class ProcessingEngine : IProcessingEngine
    {
        private ILogger<ProcessingEngine> _logger;
        private IConfiguration _configuration;
        private readonly Dictionary<char, string> PhoneMap;
        private  string DictionaryNames;
        private IFileService _fileService;
        public ProcessingEngine(IConfiguration configuration, ILogger<ProcessingEngine> logger, IFileService fileService)
        {
            _configuration = configuration;
            _logger = logger;
            _fileService = fileService;
            DictionaryNames = _configuration["DictionaryNames"].ToString();

            PhoneMap = new Dictionary<char, string>();
            PhoneMap.Add('2', "ABC");
            PhoneMap.Add('3', "DEF");
            PhoneMap.Add('4', "GHI");
            PhoneMap.Add('5', "JKL");
            PhoneMap.Add('6', "MNO");
            PhoneMap.Add('7', "PQRS");
            PhoneMap.Add('8', "TUV");
            PhoneMap.Add('9', "WXYZ");
        }
        
        public  Dictionary<string, string> Process(List<string> nums)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<string> dictNames = GetDictionaryNames();
            foreach (string num in nums)
            {
                List<string> names = dictNames;
                var phone = num.Split("-")?.GetValue(1)?.ToString();
                if (phone != null && (phone.Contains('0') || phone.Contains('1')))
                {
                    result.Add(num, string.Empty);
                }
                else
                {
                    int k = 0;
                    names = names.FindAll(x => x.Length == phone?.Length);
                    for (int i = 0; i < phone?.Length; i++)
                    {
                        char n = (phone[i]);

                        if (n != '0' && n != '1')
                        {
                            var filter = names.FindAll(c => PhoneMap[n].Contains(Convert.ToChar(c.Substring(k, 1))));
                            if (filter.Count > 0)
                            {
                                names = filter;

                            }
                            else
                            {
                                names.RemoveAll(x => x.Length == phone?.Length);
                                break;
                            }
                        }

                        k++;
                    }
                    if (names != null && names.Count > 0)
                        result.Add(num, names[0]);
                    else
                        result.Add(num, string.Empty);
                }
            }
            return result;
        }

        private List<string> GetDictionaryNames()
        {
            var FullFileName = AppDomain.CurrentDomain.BaseDirectory + DictionaryNames;
             
            List<string> companies = _fileService.ReadFile(FullFileName);
            return companies;
        }
    }
}
