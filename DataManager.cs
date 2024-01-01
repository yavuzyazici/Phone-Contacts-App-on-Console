using ContactDirection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class DataManager
    {
        public void SaveData(List<Contact> contact)
        {
            string json = JsonConvert.SerializeObject(contact);

            // JSON'u bir dosyaya kaydetme
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\data.json";
            File.WriteAllText(path, json);
        }

        public List<Contact> LoadData()
        {
            // JSON'u dosyadan okuma
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\data.json";
            string json = File.ReadAllText(path);

            // JSON'u Board objesine dönüştürme
            List<Contact> contact = JsonConvert.DeserializeObject<List<Contact>>(json);

            return contact;
        }
    }
}
