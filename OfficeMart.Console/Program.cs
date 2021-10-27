using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace OfficeMart.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryInfo = new DirectoryInfo(@"C:\Users\User\Desktop\Projects\officemart\OfficeMart.Console\Products");
            var fileInfo = directoryInfo.GetFiles();
            foreach (var file in fileInfo)
            {
                var data = file.Name;
            }
        }
     
    }
}
