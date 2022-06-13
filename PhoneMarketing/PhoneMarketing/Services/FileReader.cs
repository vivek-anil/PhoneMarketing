using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhoneMarketing.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneMarketing.Services
{
    public class FileReader : IInputService
    {
        private ILogger<FileReader> _logger;
        private IConfiguration _configuration;
        private  string InputFileName;
        private IFileService _fileService ;
        public FileReader(IConfiguration configuration, ILogger<FileReader> logger, IFileService fileService)
        {
            _configuration = configuration;
            _logger = logger;
            _fileService = fileService;
           InputFileName = _configuration["InputFile"].ToString();
        }
        public List<string> GetData()
        {
            var FullFileName = AppDomain.CurrentDomain.BaseDirectory + InputFileName;
            List<string> list = _fileService.ReadFile(FullFileName);
            return list;
        }
    }
}
