using System.Diagnostics;
using System.Threading.Tasks;

namespace OfficeMart.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.DateTime StartDateTime = System.DateTime.Now;
            //System.Console.WriteLine(@"For Loop Execution start at : {0}", StartDateTime);
            //for (int i = 0; i < 10; i++)
            //{
            //    long total = DoSomeIndependentTask();
            //    System.Console.WriteLine("{0} - {1}", i, total);
            //}
            //System.DateTime EndDateTime = System.DateTime.Now;
            //System.Console.WriteLine(@"For Loop Execution end at : {0}", EndDateTime);
            //System.TimeSpan span = EndDateTime - StartDateTime;
            //int ms = (int)span.TotalMilliseconds;
            //System.Console.WriteLine(@"Time Taken to Execute the For Loop in miliseconds {0}", ms);
            //System.Console.WriteLine("Press any key to exist");
            //System.Console.ReadLine();

            System.DateTime StartDateTime = System.DateTime.Now;
            System.Console.WriteLine(@"Parallel For Loop Execution start at : {0}", StartDateTime);
            Parallel.For(0, 10, i =>
            {
                long total = DoSomeIndependentTask();
                System.Console.WriteLine("{0} - {1}", i, total);
            });
            System.DateTime EndDateTime = System.DateTime.Now;
            System.Console.WriteLine(@"Parallel For Loop Execution end at : {0}", EndDateTime);
            System.TimeSpan span = EndDateTime - StartDateTime;
            int ms = (int)span.TotalMilliseconds;
            System.Console.WriteLine(@"Time Taken to Execute the Loop in miliseconds {0}", ms);
            System.Console.WriteLine("Press any key to exist");
            System.Console.ReadLine();
        }
        static long DoSomeIndependentTask()
        {
            //Do Some Time Consuming Task here
            //Most Probably some calculation or DB related activity
            long total = 0;
            for (int i = 1; i < 100000000; i++)
            {
                total += i;
            }
            return total;
        }
     
    }
}
