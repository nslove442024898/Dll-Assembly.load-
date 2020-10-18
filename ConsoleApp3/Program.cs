using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "YOURDOMAIN");
            string dir= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Assembly ass = Assembly.Load(File.ReadAllBytes(Path.Combine(dir,"ClassLibrary2.dll")));

            List<Assembly> assDepe = new List<Assembly>();

            Type t = ass.GetType("ClassLibrary2.Class");

            var obj=Activator.CreateInstance(t);
            
            var refereces = ass.GetReferencedAssemblies();

            foreach (AssemblyName item in refereces)
            {
                if (AppDomain.CurrentDomain.GetAssemblies().Count(c => c.FullName== item.FullName)==0)
                {
                    assDepe.Add(Assembly.Load(File.ReadAllBytes(Path.Combine(dir,item.Name + ".dll"))));
                }
            }
            var ms = t.GetMethod("MyFunc").Invoke(obj, new object[] { 20, 30 });
            Console.WriteLine(ms);
            Console.ReadLine();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
           return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().FullName.Split(',')[0] == args.Name.Split(',')[0]);
        }
    }
}
