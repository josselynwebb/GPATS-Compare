// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Linq;
using System.Reflection;

namespace NAM
{

    /// <summary>
    /// <c>AppUtil</c> class
    /// Contains methods for retreiving application unique info from its assembly attributes
    /// </summary>
    public static class AppUtil
    {
        /// <summary>
        /// Get the company name from the assembly location of the executing code
        /// </summary>
        /// <returns>Returns the Company Name</returns>
        internal static string GetCompanyName()
        {
            // Store the attributes from the executing assembly
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            string name = "";

            // If there is at least 1 attribute present
            if (attributes.Length > 0)
            {
                // Return the first attribute
                AssemblyCompanyAttribute attribute = attributes[0] as AssemblyCompanyAttribute;
                // Record the first attribute's company name
                name = attribute.Company;
            }

            // Return the result as a string
            return name;
        }

        /// <summary>
        /// Get the current application assembly name
        /// </summary>
        /// <returns>Retrun the Assembly Name</returns>
        internal static string GetName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// Get the current application version
        /// </summary>
        /// <returns></returns>
        internal static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Get the current application description
        /// </summary>
        /// <returns></returns>
        internal static string GetDescription()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault().Description;
        }

        /// <summary>
        /// Get the current application GUID
        /// </summary>
        /// <returns></returns>
        internal static string GetGuid()
        {
            return typeof(Program).GUID.ToString();
        }
    }

}
