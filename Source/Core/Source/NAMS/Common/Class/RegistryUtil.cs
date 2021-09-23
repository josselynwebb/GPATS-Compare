// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;

namespace NAM
{
    public static class RegistryUtil
    {
        // Define Constants
        const string SUBKEY = "SOFTWARE";

        /// <summary>
        /// Try to read the registry subkey created on local machine
        /// </summary>
        /// <param name="key">Used to specify key location</param>
        /// <param name="subKey">Used to specify subKey location</param>
        /// <returns>Returns true/false finding registry subKey</returns>
        internal static bool CheckRegSubKey(string key, string subKey)
        {
            // Initial reading set to pass
            bool stat = true;

            // Get the registry subKey for the key string on the local machine. False = key not found.
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, false);

            // If nothing was read
            if (regKey == null)
            {
                // Failed reading
                stat = false;
            }
            else
            {
                Microsoft.Win32.RegistryKey regSubKey = regKey.OpenSubKey(subKey, false);
                // If nothing was read or the registry subkey didn't contain the subKey string
                if (regSubKey == null || !regSubKey.ToString().Contains(subKey))
                {
                    // Failed reading
                    stat = false;
                }
            }

            // Return the result
            return stat;
        }

        /// <summary>
        /// Try to create the registry newsubkey on the local machine
        /// </summary>
        /// <param name="key">Used to specify key location</param>
        /// <param name="newSubKey">Used to specify newSubKey location</param>
        /// <returns>Returns true/false creating registry subKey</returns>
        internal static bool CreateRegSubKey(string key, string newSubKey)
        {
            // Initial creation set for success
            bool stat = true;

            string account = ".\\USERS";

            System.Security.AccessControl.RegistrySecurity regSec = new System.Security.AccessControl.RegistrySecurity();

            regSec.AddAccessRule(new System.Security.AccessControl.RegistryAccessRule(account,
                System.Security.AccessControl.RegistryRights.ReadKey |
                System.Security.AccessControl.RegistryRights.WriteKey |
                System.Security.AccessControl.RegistryRights.Delete |
                System.Security.AccessControl.RegistryRights.QueryValues |
                System.Security.AccessControl.RegistryRights.SetValue |
                System.Security.AccessControl.RegistryRights.CreateSubKey,
                System.Security.AccessControl.InheritanceFlags.None,
                System.Security.AccessControl.PropagationFlags.None,
                System.Security.AccessControl.AccessControlType.Allow));

            regSec.AddAccessRule(new System.Security.AccessControl.RegistryAccessRule("Everyone",
                System.Security.AccessControl.RegistryRights.ReadKey |
                System.Security.AccessControl.RegistryRights.WriteKey |
                System.Security.AccessControl.RegistryRights.Delete |
                System.Security.AccessControl.RegistryRights.QueryValues |
                System.Security.AccessControl.RegistryRights.SetValue |
                System.Security.AccessControl.RegistryRights.CreateSubKey,
                System.Security.AccessControl.InheritanceFlags.None,
                System.Security.AccessControl.PropagationFlags.None,
                System.Security.AccessControl.AccessControlType.Allow));

            // Record what the registry subKey is for the key string on the local machine. True = write access allowed.
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                key,
                Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.ReadKey |
                System.Security.AccessControl.RegistryRights.WriteKey |
                System.Security.AccessControl.RegistryRights.QueryValues |
                System.Security.AccessControl.RegistryRights.SetValue |
                System.Security.AccessControl.RegistryRights.Delete |
                System.Security.AccessControl.RegistryRights.CreateSubKey);

            // If nothing was read
            if (regKey != null)
            {
                // Create the newSubKey on the registry. True = writeable. If it fails (null) do the following
                //if (regKey.CreateSubKey(newSubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree) == null)
                if (regKey.CreateSubKey(newSubKey, Microsoft.Win32.RegistryKeyPermissionCheck.Default, regSec) == null)
                {
                    // Failed to create the newSubKey
                    stat = false;
                }
            }

            // Return the result
            return stat;
        }

        /// <summary>
        /// Try to read the registry key value created on local machine
        /// </summary>
        /// <param name="key">Used to specify key location</param>
        /// <param name="value">Used to specify the subKey value in the key location</param>
        /// <returns>Returns true/false finding registry subKey value</returns>
        internal static bool CheckRegValue(string key, string value)
        {
            // Initial reading set to pass
            bool stat = true;

            // Record what the registry subKey is for the key string on the local machine. False = write access denied.
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, false);

            // If nothing was read
            if (regKey == null)
            {
                // Fail the reading
                stat = false;
            }
            else
            {
                // Record the subKey value found in the key location
                object dWord = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, false).GetValue(value);

                // If nothing is read
                if (dWord == null)
                {
                    // Failed
                    stat = false;
                }
            }

            // Return the result
            return stat;
        }

        /// <summary>
        /// Try to create the registry key value on the local machine
        /// </summary>
        /// <param name="key">Used to specify key location</param>
        /// <param name="valueName">Designates the name used</param>
        /// <param name="value">Used to specify the subKey value in the key location</param>
        /// <returns>Returns true/false creating Registry Key Value</returns>
        internal static bool CreateRegValue(string key, string valueName, int value)
        {
            // Initial creation set for success
            bool stat = true;

            // Record what the registry subKey is for the key string on the local machine. True = write access allowed.
            // Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, true);
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                key,
                Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.ReadKey |
                System.Security.AccessControl.RegistryRights.WriteKey |
                System.Security.AccessControl.RegistryRights.QueryValues |
                System.Security.AccessControl.RegistryRights.SetValue |
                System.Security.AccessControl.RegistryRights.Delete |
                System.Security.AccessControl.RegistryRights.CreateSubKey);


            // If nothing was read
            if (regKey == null)
            {
                // Fail
                stat = false;
            }
            else
            {
                // value = specify data to be stored. valueName = what to call it.
                regKey.SetValue(valueName, value);

                // Close the registry key when finished
                regKey.Close();
            }

            // Return the result
            return stat;
        }

        /// <summary>
        /// Return what is read from the registry key value created on local machine
        /// </summary>
        /// <param name="key">Used to specify key location</param>
        /// <param name="value">Used to specify the subKey value in the key location</param>
        /// <param name="registryValueKind">Specifies the data type of a value stored in the registry</param>
        /// <returns>Returns value found on Local Machine Registry Key</returns>
        internal static object GetRegVal(string key, string value, Microsoft.Win32.RegistryValueKind registryValueKind)
        {
            return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, false).GetValue(value);
        }

        /// <summary>
        /// Try to create the registry key value on the local machine
        /// </summary>
        /// <param name="key">Used to specify key location</param>
        /// <param name="name">Specifies name used to hold data</param>
        /// <param name="value">Used to specify the subKey value in the key location</param>
        /// <param name="registryValueKind">Specifies the data type of a value stored in the registry</param>
        /// <returns>Returns true/false setting the registry key value</returns>
        internal static bool SetRegVal(string key, string name, dynamic value, Microsoft.Win32.RegistryValueKind registryValueKind)
        {
            // Initial condition successful
            bool stat = true;
            Microsoft.Win32.RegistryKey regKey;

            try
            {
                // Record what the registry subKey is for the key string on the local machine. True = write access allowed.
                regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, true);

                // If it does not exist
                if (regKey == null)
                {
                    // Failed
                    stat = false;
                }
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo("RegistryUtil.SetRegVal() - Error occurred opening registry key HKLM\\" + key); }
                if (Program.debugMode) { Debug.WriteDebugInfo("RegistryUtil.SetRegVal() - " + Ex.Message); }
                throw;
            }

            try
            {
                // Open the registry key, sets the name and value using the resgistryValueKind data type. Closes the key when complete.
                regKey.SetValue(name, value, registryValueKind);
            }
            catch (Exception Ex)
            {
                // Discard any exception message
                if (Program.debugMode) { Debug.WriteDebugInfo("RegistryUtil.SetRegVal() - Error occurred writing registry value HKLM\\" + key + "\\" + name); }
                if (Program.debugMode) { Debug.WriteDebugInfo("RegistryUtil.SetRegVal() - " + Ex.Message); }
                throw;
            }

            // Return the result
            return stat;
        }

        /// <summary>
        /// Remove a registry value at the given subkey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if successful, False if not</returns>
        internal static bool RemoveRegVal(string key, string value)
        {
            // Initial creation set for success
            bool stat = true;

            // Record what the registry subKey is for the key string on the local machine. True = write access allowed.
            //Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(key, true);
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                key,
                Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.ReadKey |
                System.Security.AccessControl.RegistryRights.WriteKey |
                System.Security.AccessControl.RegistryRights.QueryValues |
                System.Security.AccessControl.RegistryRights.SetValue |
                System.Security.AccessControl.RegistryRights.Delete |
                System.Security.AccessControl.RegistryRights.CreateSubKey);


            // If nothing was read
            if (regKey == null)
            {
                // Fail
                stat = false;
            }
            else
            {
                // value = specify data to be stored. valueName = what to call it.
                regKey.DeleteValue(value, false);

                // Close the registry key when finished
                regKey.Close();

            }

            // Return the result
            return stat;

        }

        internal static bool SetRegKeyWriteAccess(string key)
        {
            System.Security.AccessControl.RegistrySecurity regSec = new System.Security.AccessControl.RegistrySecurity();

            regSec.AddAccessRule(new System.Security.AccessControl.RegistryAccessRule("./Users",
                System.Security.AccessControl.RegistryRights.ReadKey |
                System.Security.AccessControl.RegistryRights.WriteKey |
                System.Security.AccessControl.RegistryRights.Delete,
                System.Security.AccessControl.InheritanceFlags.None,
                System.Security.AccessControl.PropagationFlags.None,
                System.Security.AccessControl.AccessControlType.Allow));

            return true;
        }

    }

}
