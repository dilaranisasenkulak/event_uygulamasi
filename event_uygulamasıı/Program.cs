using System;
using System.Threading;

namespace KargoFilosuYonetim
{
    
    public class KargoAraci
    {
       
        public string Plaka { get; private set; }
        public string Marka { get; private set; }
        public double Enlem { get; private set; }
        public double Boylam { get; private set; }
        private byte hiz;
        private const byte HIZ_LIMITI = 110; 

        public delegate void SpeedHandler(object sender, EventArgs e);
        public event SpeedHandler? SpeedExceeded;

        
        public byte Hiz
        {
            get { return hiz; }
            private set
            {
                hiz = value;
                if (SpeedExceeded != null && hiz > HIZ_LIMITI)
                {
                    SpeedExceeded(this, EventArgs.Empty);
                }
            }
        }

       
        public KargoAraci(string plaka, string marka, double enlem, double boylam)
        {
            Plaka = plaka;
            Marka = marka;
            Enlem = enlem;
            Boylam = boylam;
        }

        
        public void HizGuncelle(byte yeniHiz)
        {
            Enlem += new Random().NextDouble();
            Boylam += new Random().NextDouble();
            Hiz = yeniHiz;
        }
    }

   
    class Program
    {
        static void Main(string[] args)
        {
            
            KargoAraci arac1 = new KargoAraci("34DS289", "Mercedes", 10.0, 20.0);
            arac1.SpeedExceeded += Arac_SpeedExceeded;

            KargoAraci arac2 = new KargoAraci("10LV471", "Tesla", 15.0, 25.0);
            arac2.SpeedExceeded += Arac_SpeedExceeded;

            byte hizArtisMiktari = 5;

            
            for (byte hiz = 80; hiz < 130; hiz += 5)
            {
                arac1.HizGuncelle(hiz);
                arac2.HizGuncelle((byte)(hiz + hizArtisMiktari));

                Console.WriteLine($"{arac1.Plaka} plakalı aracın hızı = {arac1.Hiz}");
                Console.WriteLine($"{arac2.Plaka} plakalı aracın hızı = {arac2.Hiz}\n");

                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }

        
        private static void Arac_SpeedExceeded(object sender, EventArgs e)
        {
            KargoAraci arac = (KargoAraci)sender;
            Console.WriteLine($"UYARI: {arac.Plaka} plakalı {arac.Marka} marka kargo aracı hız limitini aştı.");
            Console.WriteLine($"Aracın hız limitini aştığı konum: Enlem {arac.Enlem:F4}, Boylam {arac.Boylam:F4}");
            Console.WriteLine($"{DateTime.Now} anında aracın hızı = {arac.Hiz} olarak ölçüldü.\n");
        }
    }
}
