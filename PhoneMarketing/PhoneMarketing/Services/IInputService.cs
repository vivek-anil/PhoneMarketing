using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneMarketing.Services
{
    public interface IInputService
    {
       Task <List<string>> GetData();
    }
}
