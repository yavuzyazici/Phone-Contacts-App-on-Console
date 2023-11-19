using ContactDirection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class Controller
    {

        public void FStart(List<Contact> contacts)
        {
            string operation; List<Contact> phoneBook = contacts;
            Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz:");
            Console.WriteLine("**********************************");
            Console.WriteLine("(1) Yeni Numara Kaydetmek - (2) Varolan Numarayı Silmek - (3) Varolan Numarayı Güncelleme - (4) Rehberi Listelemek - (5) Rehberde Arama Yapmak");
        Beginning:
            operation = Console.ReadLine();

            switch (operation)
            {
                case "1":
                    Console.Clear();
                    FRegister(phoneBook);
                    break;
                case "2":
                    Console.Clear();
                    FDelete(phoneBook);
                    break;
                case "3":
                    Console.Clear();
                    FUpdate(phoneBook);
                    break;
                case "4":
                    Console.Clear();
                    FList(phoneBook);
                    break;
                case "5":
                    Console.Clear();
                    FListSearch(phoneBook);
                    break;
                default:
                    Console.WriteLine("Lütfen geçerli bir sayı giriniz");
                    goto Beginning;
            }
        }

        public void FRegister(List<Contact> contacts)
        {
        Beginning:
            List<Contact> phoneBook = contacts;
            string name, familyname, number; int id;
            Console.Write("Lütfen isim giriniz              :");
            name = Console.ReadLine();

            Console.Write("Lütfen Soyisim giriniz           :");
            familyname = Console.ReadLine();

            Console.Write("Lütfen telefon numarası giriniz  :");
            number = Console.ReadLine();


            Contact maxid = biggestId(phoneBook);

            if (name.Length <= 0 || familyname.Length <= 0 || number.Length <= 0)
            {
                Console.WriteLine("Lütfen bütün bölümleri eksiksiz doldurunuz!!");
                Thread.Sleep(1000);
                Console.Clear();
                goto Beginning;
            }
            if (!IsAllDigits(number))
            {
                Console.WriteLine("Telefon numarası sadece rakam içermelidir");
                Thread.Sleep(1000);
                Console.Clear();
                goto Beginning;
            }

            phoneBook.Add(new Contact(maxid.ID + 1, name, familyname, number));
            Console.WriteLine("\nKayıt işlemi başarıyla gerçekleştirildi. - Ana Menüye Dönülüyor...");
            Thread.Sleep(2000);
            Console.Clear();

            FStart(phoneBook);
        }

        void FDelete(List<Contact> contacts)
        {
            List<Contact> phoneBook = contacts;
            string input, selectedId;
            Console.Write("Numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz: ");
            input = Console.ReadLine();
            Console.WriteLine();


            var selectedList = phoneBook.Where(x => x.FirstName.Contains(input) || x.LastName.Contains(input)).ToList();
            //.ForEach(x => Console.WriteLine($"ID: {x.ID} | İsim: {x.FirstName} | Soyisim: {x.LastName} | Telefon Numarası: {x.PhoneNumber}"));

            //Listede 1 adet eleman varsa direkt eklesin
            if (selectedList.Count == 1)
            {
                Contact contactToDelete = phoneBook.FirstOrDefault(c => c.FirstName == input || c.LastName == input);
                phoneBook.Remove(contactToDelete);
                Console.WriteLine($"Silme işlemi gerçekleştirildi. Silinen değer: {input}");
                Console.WriteLine("Ana Menüye Dönülüyor...");
                Thread.Sleep(1000);
                Console.Clear();
                FStart(phoneBook);

            }
            //Birden fazla ise listelesin
            else if (selectedList.Count > 1)
            {
            Beginning:
                foreach (var x in selectedList)
                    Console.WriteLine($"ID: {x.ID} | Ad: {x.FirstName} | Soyad: {x.LastName} | Telefon Numarası: {x.PhoneNumber}");

                Console.Write("Silinecek kişinin ID numarasını yazınız: ");
                selectedId = Console.ReadLine();


                if (IsAllDigits(selectedId))
                {
                    Contact contactToDelete = selectedList.FirstOrDefault(c => c.ID == Convert.ToInt32(selectedId));

                    if (contactToDelete == null)
                    {
                        Console.WriteLine("Bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto Beginning;
                    }
                    else if (contactToDelete.ID == Convert.ToInt32(selectedId))
                    {
                        phoneBook.Remove(contactToDelete);
                        Console.WriteLine($"{selectedId}. ID deki kişi başarıyla silindi.");
                        Console.WriteLine("\nAna Menüye dönülüyor.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        FStart(phoneBook);
                    }
                    else
                    {
                        Console.WriteLine("Listede bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                        Thread.Sleep(1000);
                        Console.Clear();

                        goto Beginning;
                    }

                }
                else
                {
                    Console.WriteLine("Bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                    Thread.Sleep(1000);
                    Console.Clear();

                    goto Beginning;
                }

            }
            //Yok ise ne yapacağı ile ilgili sorsun
            else if (selectedList.Count <= 0)
            {
            Beginning:
                Console.WriteLine(" Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n * Ana Menüye Dönmek için    : (1)\n * Yeniden denemek için              : (2)");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Thread.Sleep(2000);
                        Console.Clear();
                        FStart(phoneBook);
                        break;
                    case "2":
                        Thread.Sleep(2000);
                        Console.Clear();
                        FDelete(phoneBook);
                        break;
                    default:
                        Console.WriteLine("Lütfen geçerli bir değer giriniz");
                        goto Beginning;
                }
            }


        }

        void FUpdate(List<Contact> contacts)
        {
            Beggining:
            List<Contact> phoneBook = contacts;
            string input, selectedId, name, familyname, number; int id;
            Console.Write("Güncelemek istediğiniz kişinin adını ya da soyadını giriniz: ");
            input = Console.ReadLine();
            Console.WriteLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Geçerli bir değer giriniz");
                goto Beggining;
            }

            var selectedList = phoneBook.Where(x => x.FirstName.Contains(input) || x.LastName.Contains(input)).ToList();

            //Listede 1 adet eleman varsa direkt güncelleme ekranına gelsin
            if (selectedList.Count == 1)
            {
            Beginning:
                Contact contactToUpdate = selectedList.First();

                Console.Write($"Lütfen yeni ismi giriniz(*{contactToUpdate.FirstName})              :");
                name = Console.ReadLine();

                Console.Write($"Lütfen yeni soyisim giriniz(*{contactToUpdate.LastName})           :");
                familyname = Console.ReadLine();

                Console.Write($"Lütfen yeni telefon numarası giriniz(*{contactToUpdate.PhoneNumber})  :");
                number = Console.ReadLine();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(familyname) || string.IsNullOrEmpty(number))
                {
                    Console.WriteLine("Lütfen bütün bölümleri eksiksiz doldurunuz!!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto Beginning;
                }
                if (!IsAllDigits(number))
                {
                    Console.WriteLine("Telefon numarası sadece rakam içermelidir");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto Beginning;
                }

                contactToUpdate.FirstName = name;
                contactToUpdate.LastName = familyname;
                contactToUpdate.PhoneNumber = number;
                Console.WriteLine("Kişi Güncellendi");
                Console.WriteLine("\nAna Menüye dönülüyor.");
                Thread.Sleep(1000);
                Console.Clear();
                FStart(phoneBook);
            }
            //Birden fazla ise listelesin
            else if (selectedList.Count > 1)
            {
            Beginning:
                foreach (var x in selectedList)
                    Console.WriteLine($"ID: {x.ID} | Ad: {x.FirstName} | Soyad: {x.LastName} | Telefon Numarası: {x.PhoneNumber}");

                Console.Write("Güncellenecek kişinin ID numarasını yazınız: ");
                selectedId = Console.ReadLine();


                if (IsAllDigits(selectedId))
                {
                    Contact contactToUpdate = phoneBook.FirstOrDefault(c => c.ID == Convert.ToInt32(selectedId));

                    if (contactToUpdate == null)
                    {
                        Console.WriteLine("Bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                        Thread.Sleep(1000);
                        Console.Clear();

                        goto Beginning;
                    }
                    else if (contactToUpdate.ID == Convert.ToInt32(selectedId))
                    {
                        Console.Write($"Lütfen yeni ismi giriniz(*{contactToUpdate.FirstName})              :");
                        name = Console.ReadLine();

                        Console.Write($"Lütfen yeni soyisim giriniz(*{contactToUpdate.LastName})           :");
                        familyname = Console.ReadLine();

                        Console.Write($"Lütfen yeni telefon numarası giriniz(*{contactToUpdate.PhoneNumber})  :");
                        number = Console.ReadLine();

                        if (name.Length <= 0 || familyname.Length <= 0 || number.Length <= 0)
                        {
                            Console.WriteLine("Lütfen bütün bölümleri eksiksiz doldurunuz!!");
                            Thread.Sleep(1000);
                            Console.Clear();
                            goto Beginning;
                        }
                        if (!IsAllDigits(number))
                        {
                            Console.WriteLine("Telefon numarası sadece rakam içermelidir");
                            Thread.Sleep(1000);
                            Console.Clear();
                            goto Beginning;
                        }

                        contactToUpdate.FirstName = name;
                        contactToUpdate.LastName = familyname;
                        contactToUpdate.PhoneNumber = number;
                        Console.WriteLine($"{selectedId}. ID deki kişi başarıyla güncellendi.");
                        Console.WriteLine("\nAna Menüye dönülüyor.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        FStart(phoneBook);
                    }
                    else
                    {
                        Console.WriteLine("Listede bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                        Thread.Sleep(1000);
                        Console.Clear();

                        goto Beginning;
                    }

                }
                else
                {
                    Console.WriteLine("Listede bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto Beginning;
                }

            }
            //Yok ise ne yapacağı ile ilgili sorsun
            else if (selectedList.Count <= 0)
            {
            Beginning:
                Console.WriteLine(" Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n * Ana Menüye Dönmek için    : (1)\n * Yeniden denemek için              : (2)");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Thread.Sleep(2000);
                        Console.Clear();
                        FStart(phoneBook);
                        break;
                    case "2":
                        Thread.Sleep(2000);
                        Console.Clear();
                        FUpdate(phoneBook);
                        break;
                    default:
                        Console.WriteLine("Lütfen geçerli bir değer giriniz");
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto Beginning;
                }
            }

        }

        void FList(List<Contact> contacts)
        {
            List<Contact> phoneBook = contacts;
            Console.WriteLine("Telefon Rehberi\n**********************************************");
            foreach (var x in phoneBook)
            {
                Console.WriteLine($"ID: {x.ID} | Ad: {x.FirstName} | Soyad: {x.LastName} | Telefon Numarası: {x.PhoneNumber}");
            }
            Console.Write("Listede arama yapmak için(S) tuşuna Çıkış yapmak için diğer tuşlardan birine basınız(Power tuşu hariç): ");
            string cevap = Console.ReadLine();
            switch (cevap)
            {
                case "S":
                    FListSearch(phoneBook);
                    break;
                case "s":
                    FListSearch(phoneBook);
                    break;
                default:
                    Thread.Sleep(1000);
                    Console.Clear();
                    FStart(phoneBook);
                    break;

            }

        }
        void FListSearch(List<Contact> contacts)
        {
            List<Contact> phoneBook = contacts;
            Beggining:
            Console.Write("Arama yapmak istediğiniz tipi seçiniz\n**********************************************\nİsim veya soyisime göre arama yapmak için: (1)\nTelefon numarasına göre arama yapmak için: (2)\n");
            string secim = Console.ReadLine();
            if (secim != "1" && secim != "2")
            {
                Console.WriteLine("Geçerli değer giriniz.");
                Thread.Sleep(1000);
                Console.Clear();
                goto Beggining;
            }

            Console.Write("Sorgula: ");
            string query = Console.ReadLine();

            if (string.IsNullOrEmpty(query))
            {
                Console.WriteLine("Geçerli değer giriniz.");
                Thread.Sleep(1000);
                Console.Clear();
                goto Beggining;
            }
            var selectedList = phoneBook.Where(x => x.FirstName.Contains(query) || x.LastName.Contains(query) || x.PhoneNumber.Contains(query)).ToList();
            if (selectedList.Count == 0)
            {
            Beginning:
                Console.WriteLine(" Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n * Ana Menüye Dönmek için    : (1)\n * Yeniden denemek için              : (2)");
                string input = Console.ReadLine();
                Console.Clear();
                switch (input)
                {
                    case "1":
                        Thread.Sleep(1000);
                        Console.Clear();
                        FStart(phoneBook);
                        break;
                    case "2":
                        Thread.Sleep(1000);
                        Console.Clear();
                        FListSearch(phoneBook);
                        break;
                    default:
                        Console.WriteLine("Lütfen geçerli bir değer giriniz");
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto Beginning;
                }
            }
            switch (secim)
            {
                case "1":
                    selectedList = phoneBook.Where(x => x.FirstName.Contains(query) || x.LastName.Contains(query)).ToList();
                    foreach (var item in selectedList)
                        Console.WriteLine($"ID: {item.ID} | Ad: {item.FirstName} | Soyad: {item.LastName} | Telefon Numarası: {item.PhoneNumber}");
                    Beginning:
                    Console.WriteLine(" Lütfen bir seçim yapınız.\n * AnaMenüye Dönmek için    : (1)\n * Yeniden denemek için              : (2)");
                    string input = Console.ReadLine();
                    Console.Clear();
                    switch (input)
                    {
                        case "1":
                            FStart(phoneBook);
                            break;
                        case "2":
                            FListSearch(phoneBook);
                            break;
                        default:
                            Console.WriteLine("Lütfen geçerli bir değer giriniz");
                            Thread.Sleep(1000);
                            Console.Clear();
                            goto Beginning;
                    }
                    break;
                case "2":
                    selectedList = phoneBook.Where(x => x.PhoneNumber.Contains(query)).ToList();
                    foreach (var item in selectedList)
                        Console.WriteLine($"ID: {item.ID} | Ad: {item.FirstName} | Soyad: {item.LastName} | Telefon Numarası: {item.PhoneNumber}");
                    Beginning2:
                    Console.WriteLine(" Lütfen bir seçim yapınız.\n * AnaMenüye Dönmek için    : (1)\n * Yeniden denemek için              : (2)");
                    string input2 = Console.ReadLine();
                    Console.Clear();
                    switch (input2)
                    {
                        case "1":
                            FStart(phoneBook);
                            break;
                        case "2":
                            FListSearch(phoneBook);
                            break;
                        default:
                            Console.WriteLine("Lütfen geçerli bir değer giriniz");
                            Thread.Sleep(1000);
                            Console.Clear();
                            goto Beginning2;
                    }
                    break;
                default:
                    Console.WriteLine("Geçerli değer giriniz.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto Beggining;
            }
        }

        #region Karakterleri Rakammı? Metodu
        bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                {
                    if (c == ' ')
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region En Büyük ID'ye sahip eleman hangisi? Metodu
        Contact biggestId(List<Contact> list)
        {
            if (list.Count == 0)
            {
                return null;
            }

            Contact maxId = list.OrderByDescending(o => o.ID).First();
            return maxId;
        }

        #endregion
    }
}
