using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AutoForumReader
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]

        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]

        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        static void Main(string[] args)
        {
            Console.Title = "Auto Forum Reader: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            ForumReader newForumReader = new ForumReader();

            Console.WriteLine("Start Polling Websites....." + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            newForumReader.forumReaderInit();
            Console.WriteLine(".....Job Finished Exiting");

            newForumReader.CloseLogFile();
            newForumReader = null;

            IntPtr intpr = new IntPtr();
            intpr = FindWindowByCaption(IntPtr.Zero, Console.Title.ToString());
            int ProcessId = 0;

            GetWindowThreadProcessId(intpr.ToInt32(), out ProcessId);
            Console.WriteLine("Killing Process.....");
            System.Diagnostics.Process.GetProcessById(ProcessId).Kill();
        }
    }
}
