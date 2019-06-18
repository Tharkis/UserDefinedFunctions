using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace UserDefinedFunctions
{
    public class Function
    {
        public string FunctionName { get; set; }
        public Parameter[] Parameters { get; set; }
        public string InputData { get; set; }

        
    }

    public class Parameter
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

        public Parameter GetParameter(string parameterName)
        {
            List<Parameter> parameters = new List<Parameter>();
            
            return parameters.Where(functionParameter => functionParameter.ParameterName == parameterName).FirstOrDefault();
        }
    }

}
