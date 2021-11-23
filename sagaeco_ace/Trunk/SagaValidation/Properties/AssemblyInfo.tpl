using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SagaValidation")]
[assembly: AssemblyDescription("This is PRG SagaECO ValidSv")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ECOAce Project.")]
[assembly: AssemblyProduct("SagaValidation")]
[assembly: AssemblyCopyright("Copyright ©  2014-2015 ECOAce Project.")]
[assembly: AssemblyTrademark("SagaECO™")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("8503f480-fb27-4089-9d6d-e2accacccbce")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.$WCREV$")]
[assembly: AssemblyFileVersion("1.0.0.$WCREV$")]



namespace SagaValidation
{
    public class GlobalInfo
    {
        public static string Version = ($WCREV$).ToString();
        public static string ModifyDate = "$WCDATE$";
    }
}
