using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SagaLogin")]
[assembly: AssemblyDescription("This is PRG SagaECO LoginSv")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ECOAce Project.")]
[assembly: AssemblyProduct("SagaLogin")]
[assembly: AssemblyCopyright("Copyright ©  2014-2015 ECOAce Project.")]
[assembly: AssemblyTrademark("SagaECO™")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1127165b-4cf4-4fe0-a530-2bcb5f47ec0d")]

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


namespace SagaLogin
{
    public class GlobalInfo
    {
        public static string Version = ($WCREV$).ToString();
        public static string ModifyDate = "$WCDATE$";
    }
}
