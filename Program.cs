using ContactDirection;
using PhoneBook;
using System.Collections.Generic;
using static PhoneBook.Controller;


Console.SetWindowSize(145, 30);


Controller controller = new Controller();


//Başlangıç
List<Contact> phoneBook = new List<Contact>();
phoneBook.Add(new Contact(1, "Yavuz Selim", "Yazıcı", "0565149034"));
phoneBook.Add(new Contact(2, "Talip", "Bulundu", "05007740033"));
phoneBook.Add(new Contact(2, "Çağrı", "Topçu", "05501700933"));
phoneBook.Add(new Contact(3, "Recep", "Şerit", "05963541896"));
phoneBook.Add(new Contact(4, "Muhammed Fatih", "Yazıcı", "05927441034"));
phoneBook.Add(new Contact(2, "Bahadır Can", "Topçu", "05801704933"));
phoneBook.Add(new Contact(5, "Saliha", "Yazıcı", "05004321034"));

controller.FStart(phoneBook);




/*
 using ContactDirection;
using PhoneBook;


Console.SetWindowSize(145, 30);


//Rehber Listesi
List<Contact> phoneBook = new List<Contact>();

//Varsayılan Eklediğimiz 5 Kişi
phoneBook.Add(new Contact(1, "Yavuz Selim", "Yazıcı", "0565149034"));
phoneBook.Add(new Contact(2, "Bahaddin", "Yazıcı", "05007740033"));
phoneBook.Add(new Contact(3, "Recep", "Şerit", "05963541896"));
phoneBook.Add(new Contact(4, "Muhammed Fatih", "Yazıcı", "05927441034"));
phoneBook.Add(new Contact(5, "Saliha", "Yazıcı", "05004321034"));


//Başlangıç
FStart();

void FStart()
{

    string operation;
    Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz:");
    Console.WriteLine("**********************************");
    Console.WriteLine("(1) Yeni Numara Kaydetmek - (2) Varolan Numarayı Silmek - (3) Varolan Numarayı Güncelleme - (4) Rehberi Listelemek - (5) Rehberde Arama Yapmak");
Beginning:
    operation = Console.ReadLine();

    switch (operation)
    {
        case "1":
            Console.Clear();
            FRegister();
            break;
        case "2":
            Console.Clear();
            FDelete();
            break;
        case "3":
            Console.Clear();
            FUpdate();
            break;
        case "4":
            Console.Clear();
            FList();
            break;
        case "5":
            Console.Clear();
            FListSearch();
            break;
        default:
            Console.WriteLine("Lütfen geçerli bir sayı giriniz");
            goto Beginning;
    }
}

void FRegister()
{
Beginning:
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

    FStart();
}

void FDelete()
{
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
        FStart();

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
                Console.Clear();
                Console.WriteLine("Bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                Thread.Sleep(1000);
                goto Beginning;
            }
            else if (contactToDelete.ID == Convert.ToInt32(selectedId))
            {
                phoneBook.Remove(contactToDelete);
                Console.WriteLine($"{selectedId}. ID deki kişi başarıyla silindi.");
                Console.WriteLine("\nAna Menüye dönülüyor.");
                Thread.Sleep(1000);
                Console.Clear();
                FStart();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Listede bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                Thread.Sleep(1000);
                goto Beginning;
            }

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
            Thread.Sleep(1000);
            goto Beginning;
        }

    }
    //Yok ise ne yapacağı ile ilgili sorsun
    else if (selectedList.Count <= 0)
    {
    Beginning:
        Console.WriteLine(" Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n * Güncellemeyi sonlandırmak için    : (1)\n * Yeniden denemek için              : (2)");
        input = Console.ReadLine();
        switch (input)
        {
            case "1":
                FStart();
                break;
            case "2":
                FDelete();
                break;
            default:
                Console.WriteLine("Lütfen geçerli bir değer giriniz");
                goto Beginning;
        }
    }


}

void FUpdate()
{
    string input, selectedId, name, familyname, number; int id;
    Console.Write("Güncelemek istediğiniz kişinin adını ya da soyadını giriniz: ");
    input = Console.ReadLine();
    Console.WriteLine();

    var selectedList = phoneBook.Where(x => x.FirstName.Contains(input) || x.LastName.Contains(input)).ToList();

    //Listede 1 adet eleman varsa direkt güncelleme ekranına gelsin
    if (selectedList.Count == 1)
    {
    Beginning:
        Contact contactToUpdate = phoneBook.FirstOrDefault(c => c.FirstName == input || c.LastName == input);

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
        Console.WriteLine("Kişi Güncellendi");
        Console.WriteLine("\nAna Menüye dönülüyor.");
        Thread.Sleep(1000);
        Console.Clear();
        FStart();
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
                Console.Clear();
                Console.WriteLine("Bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                Thread.Sleep(1000);
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
                FStart();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Listede bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
                Thread.Sleep(1000);
                goto Beginning;
            }

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Listede bu ID numarasına sahip kişi bulunamadı. Tekrar deneyiniz");
            Thread.Sleep(1000);
            goto Beginning;
        }

    }
    //Yok ise ne yapacağı ile ilgili sorsun
    else if (selectedList.Count <= 0)
    {
    Beginning:
        Console.WriteLine(" Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n * Güncellemeyi sonlandırmak için    : (1)\n * Yeniden denemek için              : (2)");
        input = Console.ReadLine();
        switch (input)
        {
            case "1":
                FStart();
                break;
            case "2":
                FUpdate();
                break;
            default:
                Console.WriteLine("Lütfen geçerli bir değer giriniz");
                goto Beginning;
        }
    }

}

void FList()
{
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
            FListSearch();
            break;
        case "s":
            FListSearch();
            break;
        default:
            Thread.Sleep(1000);
            Console.Clear();
            FStart();
            break;

    }

}
void FListSearch()
{
    Console.Write("Arama yapmak istediğiniz tipi seçiniz\n**********************************************\nİsim veya soyisime göre arama yapmak için: (1)\nTelefon numarasına göre arama yapmak için: (2)\n");
    string secim = Console.ReadLine();

    Console.WriteLine("Sorgula");
    string query = Console.ReadLine();

    switch (secim)
    {
        case "1":
            var selectedList = phoneBook.Where(x => x.FirstName.Contains(query) || x.LastName.Contains(query)).ToList();
            foreach (var item in selectedList)
                Console.WriteLine(item);
            break;
        case "2":
            selectedList = phoneBook.Where(x => x.PhoneNumber.Contains(query)).ToList();
            foreach (var item in selectedList)
                Console.WriteLine(item);
            break;
        default:
            Console.WriteLine("Geçerli değer giriniz.");
            break;
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
*/