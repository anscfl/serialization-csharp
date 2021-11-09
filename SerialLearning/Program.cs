using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;


namespace SerialLearning
{
    //[Serializable]
    public class Machine
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public int Warranty { get; set; }
        public string Color { get; set; }
        public int NumberOfButtons { get; set; }
        public int PowerConsumption { get; set; }
        public Machine(string name, string company, int warranty, string color, int numberOfButtons, int powerConsumption)
        {
            Name = name;
            Company = company;
            Warranty = warranty;
            Color = color;
            NumberOfButtons = numberOfButtons;
            PowerConsumption = powerConsumption;
        }
        public Machine()
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // объект для сериализации
            Machine m1 = new Machine("Dishwasher", "BOSH", 24, "white", 5, 2100);
            Machine m2 = new Machine("Washing", "Beko", 24, "white", 8, 800);
            Machine m3 = new Machine("TV", "Samsung", 12, "Black", 0, 79);
            Machine m4 = new Machine("Notebook", "Lenovo", 12, "Grey", 104, 80);
            Machine m5 = new Machine("Smartphone", "Xiaomi", 12, "Blue", 0, 67);
            Machine[] mach = new Machine[] { m1, m2, m3, m4, m5 };
            Console.WriteLine("Объекты созданы");
            //BinarySerialization(mach);
            //JsonSerialization(mach);
            //XMLSerialization(mach);
        }
        static void BinarySerialization(Machine[] mach)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("machines.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, mach);
                Console.WriteLine("Объекты сериализован");
            }
            // десериализация
            using (FileStream fs = new FileStream("machines.dat", FileMode.OpenOrCreate))
            {
                Machine[] desMachine = (Machine[])formatter.Deserialize(fs);
                foreach (Machine m in desMachine)
                {
                    Console.WriteLine("Объект десериализован");
                    Console.WriteLine($"Наименование: {m.Name}\nПроизводитель: {m.Company}\nГарантия: {m.Warranty}\nЦвет: {m.Color}\nКол-во кнопок: {m.NumberOfButtons}\nПотребляемая мощность: {m.PowerConsumption}\n");
                }
            }
        }
        static void JsonSerialization(Machine[] mach)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter("user.json"))
            {
                foreach (Machine elem in mach)
                {
                    //serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(sw, elem);
                    sw.WriteLine(',');
                }

            }
            using (StreamReader file = File.OpenText("user.json"))
            {
                JsonTextReader reader = new JsonTextReader(file);
                reader.SupportMultipleContent = true;
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    JsonSerializer serializer2 = new JsonSerializer();
                    Machine m = serializer2.Deserialize<Machine>(reader);
                    Console.WriteLine($"Наименование: {m.Name}\nПроизводитель: {m.Company}\nГарантия: {m.Warranty}\nЦвет: {m.Color}\nКол-во кнопок: {m.NumberOfButtons}\nПотребляемая мощность: {m.PowerConsumption}\n");
                }
            }
        }
        static void XMLSerialization(Machine[] mach)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Machine[]));
            using (StreamWriter fs = new StreamWriter("machines.xml"))
            {   
                formatter.Serialize(fs, mach);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Machine[]));
            using (StreamReader fs = new StreamReader("machines.xml"))
            {
                Machine[] newmachs = (Machine[])serializer.Deserialize(fs);
                foreach (Machine p in newmachs)
                {
                    Console.WriteLine($"Наименование: {p.Name}\nПроизводитель: {p.Company}\nГарантия: {p.Warranty}\nЦвет: {p.Color}\nКол-во кнопок: {p.NumberOfButtons}\nПотребляемая мощность: {p.PowerConsumption}\n");
                }
            }
        }
    }
}
