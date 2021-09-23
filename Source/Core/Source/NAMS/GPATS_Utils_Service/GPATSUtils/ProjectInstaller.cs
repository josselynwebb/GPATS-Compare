using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace NAM
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Setup and start the service on the local machine
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the System.Configuration.Install.Installer.BeforeInstall event.
        /// </summary>
        /// <param name="savedState">An System.Collections.IDictionary that contains the state of the computer
        //     before the installers in the System.Configuration.Install.Installer.Installers
        //     property are installed. This System.Collections.IDictionary object should
        //     be empty at this point.</param>
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            string parameter = "GPATSUtils\" \"Application";
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
            base.OnBeforeInstall(savedState);
        }
    }
}
