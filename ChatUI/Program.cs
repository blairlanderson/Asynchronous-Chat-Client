using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Practices.Unity;
using Ninject;

using ChatLogger;
using BlairAnderson_Logger;
using DavidCrawford_Logger;
using Interfaces;
using ChatLib;

namespace ChatUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //unity ioc container dependency injection method
            UnityContainer container = new UnityContainer();
            container.RegisterType<ILoggingService, BasicLogger>();
            container.RegisterType<Client>(new InjectionConstructor("127.0.0.1", 1337, container.Resolve<ILoggingService>()));
            Application.Run(container.Resolve<ChatUI>());

            /*Uncomment to use Ninject Dependency Injection
             * 
            //Ninject container method
            create Ninject kernel
            IKernel kernel = new StandardKernel();

            //bind Interface to Logger class
            //kernel.Bind<ILoggingService>().To<BasicLogger>();
            kernel.Bind<ILoggingService>().To<NLogger>();
            kernel.Bind<Client>().ToSelf().WithConstructorArgument("server", "127.0.0.1").WithConstructorArgument("port", 1337);            //kernel.Bind<ILoggingService>().To<Logger>();
            
            //get instance of chatForm with resolved dependencies to init
            var chatForm = kernel.Get<ChatUI>();
            Application.Run(chatForm);
            */
        }
    }
}
