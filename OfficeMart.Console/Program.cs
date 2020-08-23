using ICSharpCode.Decompiler.Util;
using System;
using System.Xml.Linq;

namespace OfficeMart.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ResXResourceWriter resx = new ResXResourceWriter(@"C:\Users\Interlude\Desktop\Projects\officemart\OfficeMart.Console\TestRes.resx"))
            {
                resx.AddResource("Salam", "Privet");
                resx.AddResource("HeaderString1", "Make");
                resx.AddResource("HeaderString2", "Model");
                resx.AddResource("HeaderString3", "Year");
                resx.AddResource("HeaderString4", "Doors");
                resx.AddResource("HeaderString5", "Cylinders");
            }
        }
    }
}
