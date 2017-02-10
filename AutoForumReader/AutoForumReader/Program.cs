using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AutoForumReader
{
    /// <summary>
    /// Auto Forum Reader is an application set up to scan through Bnet forums for prospective recruits.  
    /// The application can scan through as many websites as is added in the appconfig file as a list.
    /// The application is intended to be run as a scheduled task.  If the application finds a prospective
    /// recruit, an email is generated and sent to an inbox for recruiters to review.  Additionally functionality 
    /// may be added to allow tags to be added to the emails sent to the recruitment inbox in order to identify
    /// what type of spec a potential recruit is (Tank/DPS/Healer).
    /// </summary>
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]

        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]

        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        /// <summary>
        /// Main executing program block
        /// 
        /// Exit code 0:  Everything worked
        /// Exit code 50: Error was encountered during runtime.  Check server log for more detail
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            ForumReader newForumReader = new ForumReader();

            try
            {
                Console.Title = "Auto Forum Reader: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                //Start processing
                Console.WriteLine("Start Polling Websites....." + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                newForumReader.forumReaderInit();
                Console.WriteLine(".....Job Finished Exiting");

                //Close server log file
                newForumReader.CloseLogFile();
                newForumReader = null;

                //Operation completed successfully
                Environment.Exit(0);
            }
            catch
            {
                Console.WriteLine(".....Aborting job, runtime error!");

                newForumReader.CloseLogFile();
                newForumReader = null;

                Console.WriteLine("Killing Process.....");
                //Invalid command runtime error
                Environment.Exit(50);
            }
        }
    }
}
