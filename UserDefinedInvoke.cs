/*Post Build Instructions
 * 
 * Copy the built DLL files to: C:\Program Files\Corepoint Health\Custom Objects
 * Open a command prompt as administator
 * Change directory to the Custom Objects folder
 * Run the following command to register the UserDefinedFunction DLL:
 * %systemroot%\Microsoft.NET\Framework64\v4.0.30319\regasm UserDefinedFunctions.dll /tlb /nologo
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CorepointHealth.Common.Interfaces.Gear.ItemInvoke;
using Newtonsoft.Json;

namespace UserDefinedFunctions
{
     /* Guid instructions
     * The following Guid identifies the DLL and should be replaced in any new com object you create.
     * New Guids can be generated here: https://www.guidgen.com/
     */
    [Guid("4ab2d189-c08d-44d1-ac62-ea360db8a106")]

    public class UserDefinedInvoke : IInvoke
    {
        /// <summary>
        /// The Invoke function is called by Corepoint and is the entry point for our User Defined Functions.
        /// </summary>
        /// <param name="sourceOperand">The value of the "Source Operand" field.</param>
        /// <param name="options">The value of the "Object Options" field.</param>
        /// <param name="destinationOperand">The value that will be returned in the "Destination Operand" variable.</param>
        /// <param name="status">The value that will be returned in the "Status" variable</param>
        public void Invoke(ref string sourceOperand, ref string options, ref string destinationOperand, ref string status)
        {
            // Deserialize the JSON and make it an Object
            //Action action = JsonConvert.DeserializeObject<Action>(options);
            Function function = JsonConvert.DeserializeObject <Function>(sourceOperand);
            //List<Parameter> parameterList = JsonConvert.DeserializeObject<List<Parameter>>(sourceOperand);

            // Perform functions based on the Function Name.
            //string actionParameters = action.Parameters;




            switch (function.FunctionName)
            {
                case "Test":
                    // Just a test response
                    destinationOperand = "The test worked!";
                    status = "Success";
                    return;

                case "Repeat":
                    // Repeat what was provided in the source operand
                    destinationOperand = "SourceData: " + sourceOperand;
                    status = "Success";
                    return;
                case "SubstitutionCipher":
                    // Test the old Corepoint SDK functionality.
                    var functionParams = function.Parameters.Where(functionParameter => functionParameter.ParameterName == "Cipher").FirstOrDefault();
                    string parameterValue = functionParams.ParameterValue;
                    SubstitutionCipher.SubstituteStrings(function.InputData, parameterValue, out destinationOperand, out status);
                    return;
                default:
                    // If we can't find a valid action, throw an error.
                    destinationOperand = "The action \"" + function.FunctionName + "\" is invalid.";
                    status = "Error";

                    return;
            }

        }


        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(
              "CLSID\\" + t.GUID.ToString("B") + "\\Implemented Categories\\{740B2584-57D6-46CB-A85D-B2D255115A97}");

            if (key != null)
            {
                key.Close();
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKey(
              "CLSID\\" + t.GUID.ToString("B") + "\\Implemented Categories\\{740B2584-57D6-46CB-A85D-B2D255115A97}", false);
        }
    }
}
