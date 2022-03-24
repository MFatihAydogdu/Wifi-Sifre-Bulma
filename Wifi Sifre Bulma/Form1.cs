using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wifi_Sifre_Bulma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process wifi = new System.Diagnostics.Process();
            wifi.StartInfo.CreateNoWindow = true;
            wifi.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // Komut satirinin gizlenmiş bir şekilde acilmasini sagliyor.
            wifi.StartInfo.FileName = "cmd.exe"; // istenilen uygulamayi acmayi sagliyor
            wifi.StartInfo.Arguments = "/c netsh wlan show profile"; // acilan ugulamada ne yapilmasi gerektigi yazilir.
            wifi.StartInfo.UseShellExecute = false;
            wifi.StartInfo.RedirectStandardOutput = true;
            wifi.Start();
            string output = wifi.StandardOutput.ReadToEnd();
            //label1.Text = output;
            string[] a = output.Replace("\r\n    ", "").Split(new string[] { "All User Profile     : " }, StringSplitOptions.RemoveEmptyEntries);//Wifi ismini boşluklardan arındırmak için uyguladık.

            List<string> sifreler = new List<string>();//sifreleri listeliyoruz
            for (int i = 1; i < a.Length - 1; i++)
            {
                string sifreUzun = SifreBulma(a[i]);//Sifre bulma fonksiyonunu çağırdık.
                string baslamaText = "Key Content            :";//Wifi bulma kısmında başlangıçtaki kelimeyi atadık.
                string bitisText = "Cost settings";//Şifre bulma kısmındaki son stringi atadık.
                int baslangic = sifreUzun.IndexOf(baslamaText);//Çıktıda başlangıçtaki kelimeyi arıyor
                int bitis = sifreUzun.IndexOf(bitisText);//Çıktının bittiği son kelimeyi arıyor
                string tekSifre = sifreUzun.Substring(baslangic + baslamaText.Length, bitis - baslangic - baslamaText.Length).Replace("\r\n\r\n", "").Trim();//Sifreyi diğer kelime ve boşluklardan arındırdık.
                sifreler.Add(tekSifre);// Şifreler adlı listeye ekledik

                label1.Text += tekSifre + "\r\n";//Bilgi verilmesi için ekrana yazdırdık.
            }






        }

        string SifreBulma(string b)
        {
            System.Diagnostics.Process wifi = new System.Diagnostics.Process();
            wifi.StartInfo.CreateNoWindow = true;
            wifi.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // Komut satirinin gizlenmiş bir şekilde acilmasini sagliyor.
            wifi.StartInfo.FileName = "cmd.exe"; // istenilen uygulamayi acmayi sagliyor
            wifi.StartInfo.Arguments = "/c netsh wlan show profile" + ' ' + '"' + b + '"' + ' ' + "key = clear"; // acilan ugulamada ne yapilmasi gerektigi yazilir.
            wifi.StartInfo.UseShellExecute = false;
            wifi.StartInfo.RedirectStandardOutput = true;
            wifi.Start();
            string output2 = wifi.StandardOutput.ReadToEnd();
            return output2;


        }

    }
}

