using PhoneMarketing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneMarketing.FileManager
{
    public interface IFileService
    {
        List<string> ReadFile(string fileName);
    }
}
