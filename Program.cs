using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapilariOdev_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Liste tipi seçiniz: Kuyruk (1) veya Yığın (2)");
            int listeTipi = int.Parse(Console.ReadLine());

            if (listeTipi == 1)
            {
                var kuyruk = new KuyrukYapisi();
                islemSecimi(kuyruk);
            }
            else if (listeTipi == 2)
            {
                var yigin = new YiginYapisi();
                islemSecimi(yigin);
            }
        }

        static void islemSecimi(dynamic veriYapisi)
        {
            int secim = menu();
            while (secim != 0)
            {
                switch (secim)
                {
                    case 1:
                        Console.WriteLine("Ürün ID: ");

                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ürün Adı: ");
                        string ad = Console.ReadLine();
                        Console.WriteLine("Ürün Miktarı: ");
                        int miktar = int.Parse(Console.ReadLine());

                        veriYapisi.Ekle(new Urun(id, ad, miktar));
                        break;

                    case 2:
                        veriYapisi.Sil();
                        break;

                    case 3:
                        veriYapisi.Listele();
                        break;

                    case 4:
                        Console.WriteLine("Aranacak Ürün ID: ");
                        int aranacakID = int.Parse(Console.ReadLine());
                        veriYapisi.Ara(aranacakID);
                        break;

                    case 5:
                        veriYapisi.MiktaraGoreSirala();
                        break;
                }
                secim = menu();
            }
        }


        private static int menu()
        {
            Console.WriteLine("\n1- Ekle");
            Console.WriteLine("2- Sil");
            Console.WriteLine("3- Listele");
            Console.WriteLine("4- Ara");
            Console.WriteLine("5- Miktara Göre Sırala");
            Console.WriteLine("0- Çıkış");
            Console.Write("Seçiminiz: ");
            return int.Parse(Console.ReadLine());
        }

    }


    // Node sınıfı, Ürün nesnelerini bağlı listede saklayacak
    class Node
    {
        public Urun Data { get; set; }
        public Node Next { get; set; }

        public Node(Urun data)
        {
            Data = data;
            Next = null;
        }
    }
    class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int Miktar { get; set; }

        public Urun(int id, string ad, int miktar)
        {
            Id = id;
            Ad = ad;
            Miktar = miktar;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Ad: {Ad}, Miktar: {Miktar}";
        }
    }

    // Kuyruk yapısı sınıfı
    class KuyrukYapisi
    {
        private Node front;
        private Node rear;

        public void Ekle(Urun urun)
        {
            Node yeniNode = new Node(urun);
            if (front == null)
                front = rear = yeniNode;
            else
            {
                rear.Next = yeniNode;
                rear = yeniNode;
            }
            Console.WriteLine("Ürün kuyruğa eklendi.");
        }

        public void Sil()
        {
            if (front == null)
                Console.WriteLine("Kuyruk boş.");
            else
            {
                Console.WriteLine($"Silinen ürün: {front.Data}");
                front = front.Next;
            }
        }

        public void Listele()
        {
            if (front == null)
                Console.WriteLine("Kuyruk boş.");
            else
            {
                Node temp = front;
                while (temp != null)
                {
                    Console.WriteLine(temp.Data);
                    temp = temp.Next;
                }
            }
        }

        public void Ara(int id)
        {
            Node temp = front;
            while (temp != null)
            {
                if (temp.Data.Id == id)
                {
                    Console.WriteLine("Ürün bulundu: " + temp.Data);
                    return;
                }
                temp = temp.Next;
            }
            Console.WriteLine("Ürün bulunamadı.");
        }

        public void MiktaraGoreSirala()
        {
            List<Urun> urunListesi = new List<Urun>();
            Node temp = front;

            // Kuyruktaki ürünleri geçici listeye aktar
            while (temp != null)
            {
                urunListesi.Add(temp.Data);
                temp = temp.Next;
            }

            // Miktara göre sıralama
            urunListesi.Sort((x, y) => x.Miktar.CompareTo(y.Miktar));

            // Sıralı listeyi ekrana yazdır
            Console.WriteLine("\nMiktara Göre Sıralı Liste:");
            foreach (var urun in urunListesi)
            {
                Console.WriteLine(urun);
            }
        }
    }

    // Yığın yapısı sınıfı
    class YiginYapisi
    {
        private Node top;

        public void Ekle(Urun urun)
        {
            Node yeniNode = new Node(urun);
            if (top == null)
                top = yeniNode;
            else
            {
                yeniNode.Next = top;
                top = yeniNode;
            }
            Console.WriteLine("Ürün yığına eklendi.");
        }

        public void Sil()
        {
            if (top == null)
                Console.WriteLine("Yığın boş.");
            else
            {
                Console.WriteLine($"Silinen ürün: {top.Data}");
                top = top.Next;
            }
        }

        public void Listele()
        {
            if (top == null)
                Console.WriteLine("Yığın boş.");
            else
            {
                Node temp = top;
                while (temp != null)
                {
                    Console.WriteLine(temp.Data);
                    temp = temp.Next;
                }
            }
        }

        public void Ara(int id)
        {
            Node temp = top;
            while (temp != null)
            {
                if (temp.Data.Id == id)
                {
                    Console.WriteLine("Ürün bulundu: " + temp.Data);
                    return;
                }
                temp = temp.Next;
            }
            Console.WriteLine("Ürün bulunamadı.");
        }

        public void MiktaraGoreSirala()
        {
            List<Urun> urunListesi = new List<Urun>();
            Node temp = top;

            // Yığındaki ürünleri geçici listeye aktar
            while (temp != null)
            {
                urunListesi.Add(temp.Data);
                temp = temp.Next;
            }

            // Miktara göre sıralama
            urunListesi.Sort((x, y) => x.Miktar.CompareTo(y.Miktar));

            // Sıralı listeyi ekrana yazdır
            Console.WriteLine("\nMiktara Göre Sıralı Liste:");
            foreach (var urun in urunListesi)
            {
                Console.WriteLine(urun);
            }


        }
    }
}
