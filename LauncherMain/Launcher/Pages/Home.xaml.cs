using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using PlooshFN.Services;
using PlooshFN.Services.Launch;
using System.Runtime.InteropServices;

public class Launcher
{

    public static void Launch()
    {
        try
        {
            string Path69 = UpdateINI.ReadValue("Auth", "Path");
            if (Path69 != "NONE") // NONE THE BEST RESPONSE!
            {
                //MessageBox.Show(Path69);
                if (File.Exists(System.IO.Path.Combine(Path69, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe")))
                {
                    if (UpdateINI.ReadValue("Auth", "Email") == "NONE" || UpdateINI.ReadValue("Auth", "Password") == "NONE")
                    {
                        MessageBox.Show("Please add your PlooshFN info in settings!");
                        //this.Close();
                        //(new Form2()).Show();
                        return;
                    }
                    WebClient OMG = new WebClient();
                    if (!File.Exists(Path.Combine(Path69, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib2.x64.dll")))
                    {
                        File.Copy(Path.Combine(Path69, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"), Path.Combine(Path69, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib2.x64.dll"));
                    }
                    //OMG.DownloadFile("https://cdn.discordapp.com/attachments/883144741838020629/1174502844636856380/Cobalt.dll", Path.Combine(Path69, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                    OMG.DownloadFile("https://cdn.discordapp.com/attachments/999336953654804550/1182103788505542686/Cobalt.dll?", Path.Combine(Path69, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                    //AntiCheat.Start(Path69);
                    PSBasics.Start(Path69, "-plooshfn -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", UpdateINI.ReadValue("Auth", "Email"), UpdateINI.ReadValue("Auth", "Password"));
                    FakeAC.Start(Path69, "FortniteClient-Win64-Shipping_BE.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "r");
                    FakeAC.Start(Path69, "FortniteLauncher.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "dsf");
                    try
                    {
                        PSBasics._FortniteProcess.WaitForExit();
                    } catch (Exception) {}
                    try
                    {
                        FakeAC._FNLauncherProcess.Close();
                        FakeAC._FNAntiCheatProcess.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There has been an error closing Fake AC.");
                    }



                    //Injector.Start(PSBasics._FortniteProcess.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "EonCurl.dll"));
                }
                else
                {
                    MessageBox.Show("Please add your HEAT info in settings!");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    public static void Kill()
    {
        try
        {
            Process.GetProcessById(PSBasics._FortniteProcess.Id).Kill();
            FakeAC._FNLauncherProcess.Kill();
            FakeAC._FNAntiCheatProcess.Kill();
        } catch (Exception ex)
        {

        }
    }
}

namespace PlooshFN.Pages
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : UserControl
	{

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public Home()
		{
			InitializeComponent();
		}

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            Launcher.Kill();
            this.Button.Content = "Start PlooshFN";
            this.Button.Click -= Button_Click_Stop;
            this.Button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
		{
            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(h, 6);
            
            Thread launcherThread = new Thread(Launcher.Launch);
            launcherThread.Start();
            //this.PathBox.Text = commonOpenFileDialog.FileName;
            this.Button.Content = "Stop PlooshFN";
            this.Button.Click -= Button_Click;
            this.Button.Click += Button_Click_Stop;
        }
	}
}
