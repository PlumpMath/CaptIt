using System;
using System.Windows.Forms;
using System.Threading;

namespace CaptIt
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool flagMutex;

            Mutex m_hMutex = new Mutex(true, "TESTAPP", out flagMutex);
            if (flagMutex)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("이미 프로그램이 실행중입니다!");
                Application.Exit();
            }
        }
    }
}
