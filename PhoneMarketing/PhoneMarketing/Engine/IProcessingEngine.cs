using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneMarketing.Engine
{
    public interface IProcessingEngine
    {
        public Dictionary<string, string> Process(List<string> inputData);
    }
}
