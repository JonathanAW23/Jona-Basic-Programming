using System;

namespace Testing
{
    class Program
    {
        static int inputMenu;
        static int MaxStorage = 1000;
        static int MaxShoppingCart = 100;
        static string[] namaItem = new string[MaxStorage];
        static int[] stockItem = new int[MaxStorage];
        static int[] hargaItem = new int[MaxStorage];
        static string[] keranjangBarang = new string[MaxShoppingCart];
        static int[] keranjangHarga = new int[MaxShoppingCart];
        static int[] keranjangJumlah = new int[MaxShoppingCart];
        static int countBasket = 0;
        static int countItem = 0;
        static bool kurangStock = false;

        static void Menu() 
        {
            Console.Clear();
            Console.WriteLine("Sistem Cetak Invoice Sederhana");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Tambah Produk\n2. Beli Produk\n3. Jual Produk\n4. Checkout\n0. Keluar");
            Console.WriteLine("=============================\n");
            Console.Write("Silahkan Pilih Menu : ");
        }

        static void ExitMessage()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("Terima Kasih Sudah Menggunakan Aplikasi Ini");
            Console.WriteLine("===========================================");
        }

        static void ItemList()
        {
            string number, price;
            Console.Clear();
            Console.WriteLine("Item List :");
            Console.WriteLine("==================================================================");
            Console.WriteLine($"{"No",-6}{"Nama",-20}{"Harga",-23}{"Stock"}");
            Console.WriteLine("==================================================================");
            for (int i = 0; i < countItem; i++)
            {
                number =$"{i + 1}.";
                price = $"RP.{hargaItem[i]},00";
                Console.WriteLine($"{number,-6}{namaItem[i],-20}{price,-23}{stockItem[i]}");
            }
            Console.WriteLine("==================================================================\n");
        }

        static void JualList()
        {
            double finalTotal = 0;
            int totalItem = 0;
            double total,discount;
            string number, price, totalMoney, finalTotalMoney,discountMoney;
            Console.Clear();
            Console.WriteLine("Jual List :");
            Console.WriteLine("=============================================================================================");
            Console.WriteLine($"{"No",-6}{"Nama",-20}{"Harga",-23}{"Stock",-17}{"Total"}");
            Console.WriteLine("=============================================================================================");
            for (int i = 0; i < countBasket; i++)
            {
                number = $"{i + 1}.";
                price = $"RP.{keranjangHarga[i]},00";
                total = keranjangHarga[i] * keranjangJumlah[i];
                totalMoney = $"RP.{total},00";
                finalTotal = finalTotal + total;
                totalItem = totalItem + keranjangJumlah[i];
                Console.WriteLine($"{number,-6}{keranjangBarang[i],-20}{price,-23}{keranjangJumlah[i],-17}{totalMoney}");
            }
            Console.WriteLine("=============================================================================================");
            if (totalItem >= 50 && totalItem < 100) 
            {
                discount = finalTotal * 0.05;
                finalTotal = finalTotal - (discount);
                discountMoney= $"RP.{discount},00";
                Console.WriteLine($"{"Discount",-66}{discountMoney}");
            }
            if (totalItem >= 100 && totalItem < 200)
            {
                discount = finalTotal * 0.1;
                finalTotal = finalTotal - (discount);
                discountMoney = $"RP.{discount},00";
                Console.WriteLine($"{"Discount",-66}{discountMoney}");
            }
            if (totalItem >= 200)
            {
                discount = finalTotal * 0.2;
                finalTotal = finalTotal - (discount);
                discountMoney = $"RP.{discount},00";
                Console.WriteLine($"{"Discount",-66}{discountMoney}");
            }
            finalTotalMoney = $"RP.{finalTotal},00";
            Console.WriteLine($"{"Final Total",-66}{finalTotalMoney}\n");
        }

        static string InputBarang(string nama, int harga) 
        {
            namaItem[countItem] = nama;
            hargaItem[countItem] = harga;
            stockItem[countItem] = 0;
            countItem++;
            return "Input Barang berhasil";
        }

        static string RestockBarang(int index, int tambahStock)
        {
            if (index - 1 < countItem)
                stockItem[index - 1] = stockItem[index - 1] + tambahStock;
            else
                return "Restock Barang Gagal, item yang dipilih tidak ada pada item list";
            return "Restock Barang Berhasil";
        }

        static string JualBarang(int index, int jual)
        {
            if (index - 1 < countItem)
            {
                if (stockItem[index - 1] - jual >= 0)
                {
                    stockItem[index - 1] = stockItem[index - 1] - jual;
                    keranjangBarang[countBasket] = namaItem[index - 1];
                    keranjangHarga[countBasket] = hargaItem[index - 1];
                    keranjangJumlah[countBasket] = jual;
                    countBasket++;
                }
                else
                {
                    kurangStock = true;
                    return ($"Input Jual Barang Gagal, barang yang tersisa : {stockItem[index - 1]}");
                }
            }
            else 
            {
                kurangStock = false;
                return "Input Jual Barang Gagal, item yang dipilih tidak ada pada item list";
            }
            kurangStock = false;
            return "Input Jual Barang Berhasil";
        }

        static void TambahProduk()
        {
            if (countItem < MaxStorage)
            {
                int hargaItem = 0;
                string namaItem;
                Start:
                ItemList();
                Console.Write("Masukan Nama Item Baru : ");
                namaItem = Console.ReadLine();
                if (string.IsNullOrEmpty(namaItem))
                {
                    Console.Clear();
                    Console.WriteLine("Nama item tidak boleh kosong");
                    Console.ReadLine();
                    goto Start;
                }
                Console.Write("Masukan Harga Item Baru : ");
                try
                {
                    hargaItem = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Harga item hanya boleh menggunakan angka non desimal dan tidak boleh kosong");
                    Console.ReadLine();
                    goto Start;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Terjadi kesalahan input harga item");
                    Console.ReadLine();
                    goto Start;
                }
                if (hargaItem <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("harga item harus diatas Rp.0,00");
                    Console.ReadLine();
                    goto Start;
                }
                Console.Clear();              
                Console.WriteLine(InputBarang(namaItem, hargaItem));
                Console.ReadLine();
            }
            else
            {
                ItemList();
                Console.Write("\nTambah barang tidak dapat dilakukan karena sudah jumlah barang sudah melewati batas");
                Console.ReadLine();
            }
        }

        static void BeliProduk() 
        {
            int nomorItem = 0;
            int jumlahItem = 0;
            Start:
            ItemList();
            Console.Write("Masukan Nomor Item yang Ingin Dibeli: ");
            try
            {
                nomorItem = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Nomor Item tidak boleh kosong atau menggunakan huruf");
                Console.ReadLine();
                goto Start;
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Terdapat kesalahan pada input nomor item, silahkan input ulang kembali");
                Console.ReadLine();
                goto Start;
            }
            if (nomorItem <= 0 || nomorItem > MaxStorage)
            {
                Console.Clear();
                Console.WriteLine("input nomor item tidak tersedia, silahkan input ulang kembali");
                Console.ReadLine();
                goto Start;
            }
            Console.Write("Masukan Berapa Banyak Jumlah Item yang Ingin Dibeli : ");
            try
            {
                jumlahItem = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Jumlah Item tidak boleh kosong atau menggunakan huruf");
                Console.ReadLine();
                goto Start;
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Terdapat kesalahan pada input jumlah item, silahkan input ulang kembali");
                Console.ReadLine();
                goto Start;
            }
            if (jumlahItem <= 0)
            {
                Console.Clear();
                Console.WriteLine("Input jumlah item minimal 1");
                Console.ReadLine();
                goto Start;
            }
            Console.Clear();
            Console.WriteLine(RestockBarang(nomorItem, jumlahItem));
            Console.ReadLine();
        }

        static void JualProduk() 
        {
            if (countBasket < MaxShoppingCart)
            {
                int nomorJual = 0;
                int jumlahJual = 0;
                string kompromi;
            Start:
                ItemList();
                Console.Write("Masukan Nomor Item yang Ingin Dijual : ");
                try
                {
                    nomorJual = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Nomor Item tidak boleh kosong atau menggunakan huruf");
                    Console.ReadLine();
                    goto Start;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Terdapat kesalahan pada input nomor item, silahkan input ulang kembali");
                    Console.ReadLine();
                    goto Start;
                }
                if (nomorJual <= 0 || nomorJual > 1000)
                {
                    Console.Clear();
                    Console.WriteLine("input nomor item tidak tersedia, silahkan input ulang kembali");
                    Console.ReadLine();
                    goto Start;
                }
                Console.Write("Masukan Berapa Banyak Jumlah Item yang Inging Dijual : ");
                try
                {
                    jumlahJual = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Jumlah Item tidak boleh kosong atau menggunakan huruf");
                    Console.ReadLine();
                    goto Start;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Terdapat kesalahan pada input jumlah item, silahkan input ulang kembali");
                    Console.ReadLine();
                    goto Start;
                }
                if (jumlahJual <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Input jumlah item minimal 1");
                    Console.ReadLine();
                    goto Start;
                }
                RedoKompromi:
                Console.Clear();
                Console.WriteLine(JualBarang(nomorJual, jumlahJual));
                if (kurangStock == true && stockItem[nomorJual - 1] > 0)
                {
                    Console.Write("Apakah anda mau membeli seluruh stock yang tersedia saja? (Y/N) : ");
                    kompromi = Console.ReadLine();
                    if (kompromi == "Y" || kompromi == "N")
                    {
                        if (kompromi == "Y") 
                        {
                            Console.Clear();
                            Console.WriteLine(JualBarang(nomorJual, stockItem[nomorJual - 1]));
                        } 
                    }
                    else 
                    {
                        Console.Clear();
                        Console.WriteLine("Pilihan yang tersedia hanya Y atau N, silahkan lakukan input ulang kembali");
                        Console.ReadLine();
                        goto RedoKompromi;
                    }
                }
                Console.ReadLine();
            }
            else 
            {
                JualList();
                Console.Write("\nTambah barang ke keranjang tidak dapat dilakukan karena keranjang belanja sudah penuh");
                Console.ReadLine();
            }
        }

        static void Checkout() 
        {
            JualList();
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Start:
            do
            {
                Menu();
                try
                { 
                    inputMenu = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Input tidak boleh kosong atau menggunakan huruf");
                    Console.ReadLine();
                    goto Start;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Terdapat kesalahan pada input, silahkan input ulang kembali");
                    Console.ReadLine();
                    goto Start;
                }
                switch (inputMenu) 
                {
                    case 1:
                        TambahProduk();
                        break;
                    case 2:
                        BeliProduk();
                        break;
                    case 3:
                        JualProduk();
                        break;
                    case 4:
                        Checkout();
                        break;
                }
            } while (inputMenu != 0);
            ExitMessage();
        }
    }
}
