using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hippie.Repositories
{
    public static class Automation
    {      
        public static async Task WaitFortTitleContainsText(string text)
        {
            while (true)
            {
                IntPtr mainWindowHandle = await WaitForGarment();
                string windowTitle = WINAPI.GetWindowTitle(mainWindowHandle);
                if (windowTitle.Contains(text)) break;
                Thread.Sleep(1000);
            }
        }


        public static async Task<IntPtr> WaitForGarment()
        {
            dynamic mainWindowHandle = null;
            while (mainWindowHandle == null)
            {
                Process[] processes = Process.GetProcesses().Where(i => i.ProcessName.ToLower().Contains("garment")).ToArray();

                if (processes.Count() > 0)
                {
                    var auxHandle = processes.First().MainWindowHandle;
                    if (WINAPI.IsWindow(auxHandle))
                    {
                        mainWindowHandle = auxHandle;
                        break;
                    }
                }
                Thread.Sleep(1000);
            }

            return (IntPtr)mainWindowHandle;
        }


        public static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent, int maxCount)
        {
            var result = new List<IntPtr>();
            int ct = 0;
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;
            while (true && ct < maxCount)
            {
                currChild = WINAPI. FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero) break;
                result.Add(currChild);
                prevChild = currChild;
                ++ct;
            }
            return result;
        }

        public static async Task ClickPrintButton()
        {
            IntPtr mainWindowHandle = await WaitForGarment();

            WINAPI.Rect rect = new WINAPI.Rect();
            WINAPI.GetWindowRect(mainWindowHandle, ref rect);
            //Cursor = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(rect.Right - 100, rect.Bottom - 55);

            WINAPI.LeftMouseClick(rect.Right - 100, rect.Bottom - 55);
        }

        public static async Task OpenFile(string path, FormMain form)
        {


            ((Form)form).TopMost = false;

            //Helpers.ShowLoading(true);
            Process.Start(path);

            IntPtr mainWindowHandle = await WaitForGarment();
            // Se minimizado
            if (WINAPI.IsIconic(mainWindowHandle))
                WINAPI.ShowWindow(mainWindowHandle, 9);
            WINAPI.SetForegroundWindow(mainWindowHandle); // Traz a janela para o primeiro plano

            await WaitFortTitleContainsText(path);

            Thread.Sleep(5000);
            await ClickPrintButton();
       
            Thread.Sleep(15000);

            ((Form)form).TopMost = true;
            WINAPI.SetForegroundWindow(form.Handle);
            form.textBoxCode.Focus();

            //Helpers.ShowLoading(false);
        }

        public static void CleanTempDir()
        {
            new Thread(delegate () {
                while (true)
                {
                    foreach (var file in Directory.GetFiles(FileHelpers.GetTempPath(), "*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    Thread.Sleep(60000 * 5);
                }
            }).Start();                  
        }
    }
}
