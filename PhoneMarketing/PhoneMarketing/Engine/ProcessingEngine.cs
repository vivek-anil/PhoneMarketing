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
        private string DictionaryNames;
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

        public async Task<Dictionary<string, string>> Process(List<string> nums)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<string> names;
            try
            {
                List<string> dictNames = await GetDictionaryNames();


                nums.ForEach(num =>
                {
                    names = dictNames;
                    var phone = num.Split("-")?.GetValue(1)?.ToString();
                    int k = 0;
                    names = names.FindAll(x => x.Length == phone?.Length);
                    for (int i = 0; i < phone?.Length; i++)
                    {
                        char n = (phone[i]);

                        if (n != '0' && n != '1')
                        {
                            var filter = names.FindAll(c => PhoneMap[n].Contains(c.Substring(k, 1)));
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
                });
            }
            catch (Exception ex)
            {
                string message = $"Exception in Class = ProcessingEngine, Method = Process, Message = {ex.Message}";
                _logger.LogError(message, ex.ToString());
            }
            return result;
        }

        private async Task<List<string>> GetDictionaryNames()
        {
            List<string> names = new List<string>();
            try
            {
                var FullFileName = AppDomain.CurrentDomain.BaseDirectory + DictionaryNames;
                names = await _fileService.ReadFile(FullFileName);
            }
            catch (Exception ex)
            {
                string message = $"Exception in Class = ProcessingEngine, Method = GetDictionaryNames, Message = {ex.Message}";
                _logger.LogError(message, ex.ToString());
            }

            return names;
        }
    }
}
