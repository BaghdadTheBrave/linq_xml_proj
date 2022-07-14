using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace Program
{
 class MyClass
 {
     
 }
}
namespace L2{
    
    class Program{
      
        static void Main(string[] args)
        {
            
            
            List<Manufacturer> manufacturers=new List<Manufacturer>{
                new Manufacturer{
                    id=1,
                    itemsId=new List<int>{1,3,5},
                    name ="Krokodyl Gena"},
                new Manufacturer{
                    id=2,
                    itemsId=new List<int>{2,4,8,10},
                    name ="Shapoklyak"},
                new Manufacturer{
                    id=3,
                    itemsId=new List<int>{6,7,9},
                    name ="Cheburashka"},
            };
            List<Item> items = new List<Item>{
                new Item{
                    id=1,
                    amount=20,
                    price=29,
                    name="Abobs",
                    deliveriesId=new List<int>{1,4,7,9},
                    Manufacturer=1
                },
                                new Item{
                    id=2,
                    amount=210,
                    price=229,
                    name="MnM",
                    deliveriesId=new List<int>{2,4,7,9},
                    Manufacturer=2
                },
                                new Item{
                    id=3,
                    amount=223,
                    price=1219,
                    name="KlKl",
                    deliveriesId=new List<int>{3,7,8},
                    Manufacturer=1
                },
                                new Item{
                    id=4,
                    amount=202,
                    price=291,
                    name="Ukbk",
                    deliveriesId=new List<int>{5,6,10},
                    Manufacturer=2
                },
                                new Item{
                    id=5,
                    amount=1,
                    price=212,
                    name="Jbs",
                    deliveriesId=new List<int>{2,4,5,6},
                    Manufacturer=1
                },
                                new Item{
                    id=6,
                    amount=545,
                    price=761,
                    name="wtr",
                    deliveriesId=new List<int>{1,2,3},
                    Manufacturer=3
                },
                                new Item{
                    id=7,
                    amount=220,
                    price=229,
                    name="Jabster",
                    deliveriesId=new List<int>{7,9},
                    Manufacturer=3
                },
                                new Item{
                    id=8,
                    amount=120,
                    price=219,
                    name="HE",
                    deliveriesId=new List<int>{1,9},
                    Manufacturer=2
                },
                                new Item{
                    id=9,
                    amount=999,
                    price=999,
                    name="Frogie frog",
                    deliveriesId=new List<int>{1,2,3,4,5,6,7,8,9,10},
                    Manufacturer=3
                },
                                new Item{
                    id=10,
                    amount=0,
                    price=1,
                    name="coin",
                    deliveriesId=new List<int>{1,2,7,9},
                    Manufacturer=2
                }
            };
            List<Deliveries> deliveries=new List<Deliveries>{};
            DateOnly[]dates= new DateOnly[9]{
                new DateOnly(2021, 2, 2),
                new DateOnly(2021, 2, 3),
                new DateOnly(2021, 2, 12),

                new DateOnly(2021, 3, 1),
                new DateOnly(2021, 3, 5),
                new DateOnly(2021, 3, 10),

                new DateOnly(2021, 3, 17),
                new DateOnly(2021, 3, 28),
                new DateOnly(2021, 4, 2)
            };
            for(int i=0;i<9;i++){
                deliveries.Add(new Deliveries(items,dates[i],i+1));
            }
            
            List<itemsDeliveries> itemsDeliveries = new List<itemsDeliveries>{};
            foreach (var item in items){
                foreach (var deliveryId in item.deliveriesId){
                    itemsDeliveries.Add(new itemsDeliveries{itemId=item.id, deliveryId=deliveryId});
                }
            }
            var sts = new XmlWriterSettings();
            sts.Indent=true;

            using (XmlWriter writer = XmlWriter.Create("data.xml", sts)){
                writer.WriteStartElement("linqToXml");
                writer.WriteStartElement("manufacturers");
                foreach(var man in manufacturers){
                    writer.WriteStartElement("Manufacturer");

                    writer.WriteElementString("id",man.id.ToString());
                    writer.WriteElementString("name",man.name);
                    writer.WriteStartElement("itemsID");
                    foreach(var itm in man.itemsId){
                        writer.WriteElementString("id",itm.ToString());
                    }
                    writer.WriteEndElement();
                    
                    
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                
                writer.WriteStartElement("items");
                foreach(var itm in items){
                    writer.WriteStartElement("Item");

                    writer.WriteElementString("id",itm.id.ToString());
                    writer.WriteElementString("amount",itm.amount.ToString());
                    writer.WriteElementString("price",itm.price.ToString());
                    writer.WriteElementString("name",itm.name);
                    writer.WriteStartElement("deliveriesId");
                    foreach(var dlvr in itm.deliveriesId){
                        writer.WriteElementString("id",dlvr.ToString());
                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("manufacturer",itm.Manufacturer.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("DeliveriesNodes");
                foreach(var dlvr in deliveries){
                    writer.WriteStartElement("Deliveries");
                    writer.WriteElementString("id",dlvr.id.ToString());
                    writer.WriteElementString("date",dlvr.date.ToString("MM-dd-yyyy"));
                    writer.WriteStartElement("items");
                    foreach(var itm in dlvr.items){
                        writer.WriteElementString("id",itm.ToString());
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("ItemsDeliveries");
                foreach(var itde in itemsDeliveries){
                    writer.WriteStartElement("itemDelivery");

                    writer.WriteElementString("itemId",itde.itemId.ToString());
                    writer.WriteElementString("deliveryId",itde.deliveryId.ToString());
                    
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            

            XmlDocument doc = new XmlDocument();
            doc.Load("data.xml");
            foreach(XmlNode mnfctrr in doc.DocumentElement.FirstChild){
                // Console.WriteLine(mnfctrr.Attributes["id"].Value + ": " + mnfctrr.Attributes["name"].Value);
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine(mnfctrr.ChildNodes[i].InnerText);
                }
                XmlNode deliveriesId = mnfctrr.ChildNodes[2];
                for (int i = 0; i < deliveriesId.ChildNodes.Count; i++)
                {
                    Console.WriteLine(deliveriesId.ChildNodes[i].InnerText);
                }
                Console.WriteLine();
            }

            DateOnly check = new DateOnly(2020,9,9);
            Console.WriteLine(check.ToString("MM-dd-yyyy"));
            Console.WriteLine(check.ToString());
            Console.WriteLine(DateOnly.Parse("09-09-2020").ToString("MM-dd-yyyy"));
            Query1();
            Query2();
            Query3();
            Query4();
            Query5();
            Query6();
            Query7();
            Query8();
            Query9();
            Query10();
            Query11();
            Query12();
            Query13();
            Query14();
            Query15();

        }
        static void Query1(){
            Console.WriteLine("showing names of each item:");
            XDocument xDoc = XDocument.Load("data.xml");
            XElement? root = xDoc.Element("linqToXml").Element("items");
            foreach(XElement item in root.Elements("Item")){
                XElement? val = item.Element("name");
                Console.WriteLine(val?.Value);
            }
             
            Console.WriteLine();
        }
        static void Query2(){
            Console.WriteLine("showing each delivery after 10.10.2020, containing item with id 3:");
            XDocument xDoc = XDocument.Load("data.xml");
            XElement? root = xDoc.Element("linqToXml").Element("DeliveriesNodes");
            bool is3;
            DateOnly minDate = new DateOnly(2020,10,10);
            foreach(XElement del in root.Elements("Deliveries")){
                is3=false;
                XElement? date = del.Element("date");
                XElement? items = del.Element("items");
                if(DateOnly.Parse(date?.Value).CompareTo(minDate)>0){
                    
                    foreach(var itm in items.Elements("id")){
                        if (Int32.Parse(itm.Value)==3){
                            is3=true;
                            break;
                        }
                    }
                    if(is3==true){
                        Console.WriteLine("id: {0}, date: {1}",del.Element("id").Value,del.Element("date").Value);
                    }
                
                }

            }
            
            Console.WriteLine();
        }
        static void Query3(){
            Console.WriteLine("showing each item with total price less than 20000 in descending order:");
            XDocument xDoc = XDocument.Load("data.xml");
            XElement? root = xDoc.Element("linqToXml").Element("items");
            
            var result = root.Descendants("Item")
            .Where(p => Int32.Parse(p.Element("amount").Value)*Int32.Parse(p.Element("price").Value)<2000)
            .Select(p => p.Element("name").Value)
            .OrderByDescending(p => p.Trim());
            foreach(var item in result){
                Console.WriteLine(item);

            }
             
            Console.WriteLine();
        }
        static void Query4(){
            Console.WriteLine("showing each item grouping by manufacturer:");
            XDocument xDoc = XDocument.Load("data.xml");
            XElement? root = xDoc.Element("linqToXml").Element("items");
            
            var result = root.Descendants("Item")
            .GroupBy(p=>p.Element("manufacturer").Value);
            foreach(var group in result){
                foreach(var itm in group){
                    Console.WriteLine(itm.Element("name").Value);
                }
            }
            
            Console.WriteLine();
        }
        static void Query5(){
            Console.WriteLine("showing each item with it's manufacturer name:");
            XElement root = XDocument.Load("data.xml").Element("linqToXml");
            var result = from i in root.Element("items").Elements("Item")
                        join m in  root.Element("manufacturers").Elements("Manufacturer")
                        on i.Element("manufacturer").Value equals m.Element("id").Value

                        select new{
                            name=i.Element("name").Value,
                            manufacturer=m.Element("name").Value
                        };

            foreach(var r in result){
                Console.WriteLine("{0} is created by {1}",r.name, r.manufacturer);
            }   
            Console.WriteLine(); 
        }
        static void Query6(){
            Console.WriteLine("showing each item with manufacturer's name first letter is S:");

            XElement root = XDocument.Load("data.xml").Element("linqToXml");
            var result = from i in root.Element("items").Elements("Item")
                        join m in  root.Element("manufacturers").Elements("Manufacturer")
                        on i.Element("manufacturer").Value equals m.Element("id").Value
                        where m.Element("name").Value.Substring(0,1)=="S"
                        select new{
                            name=i.Element("name").Value,
                            manufacturer=m.Element("name").Value
                        };

            foreach(var r in result){
                Console.WriteLine("{0} is created by {1}",r.name, r.manufacturer);
            }   
            Console.WriteLine(); 

        }
        static void Query7(){
            Console.WriteLine("showing date of each delivery containing 'Abobs' item");
            
            XElement root = XDocument.Load("data.xml").Element("linqToXml");
            var result = from id in root.Element("ItemsDeliveries").Elements("itemDelivery")
                        join i in root.Element("items").Elements("Item") on id.Element("itemId").Value 
                            equals i.Element("id").Value 
                        join d in root.Element("DeliveriesNodes").Elements("Deliveries") on id.Element("deliveryId").Value
                            equals d.Element("id").Value
                        where i.Element("name").Value=="Abobs"
                        select d.Element("date").Value.ToString();
            
            foreach(var r in result){
                Console.WriteLine(r);
            }
            Console.WriteLine();
        }
        static void Query8(){
            Console.WriteLine("showing items made by first manufacturer then by second");

            XElement root = XDocument.Load("data.xml").Element("linqToXml").Element("items");

            var result1 = from i in root.Elements("Item")
                            where Int32.Parse(i.Element("manufacturer").Value) == 1
                            select i;
            var result2 = from i in root.Elements("Item")
                            where Int32.Parse(i.Element("manufacturer").Value) == 2
                            select i;
            var result = result1.Concat(result2);

            foreach(var r in result){
                Console.WriteLine(r.Element("name").Value);
            }
            Console.WriteLine();
        }
        static void Query9(){
            Console.WriteLine("showing items grouped by manufacturer and sorted amount");

            XElement root = XDocument.Load("data.xml").Element("linqToXml").Element("items");
            var result = root.Elements("Item").OrderBy(i => i.Element("amount").Value).GroupBy(i =>i.Element("manufacturer").Value);

            foreach(var group in result){
                foreach(var r in group){
                    Console.WriteLine(r.Element("name").Value);
                }
            }
            Console.WriteLine();
        }
        static void Query10(){
            Console.WriteLine("showing average price of third manufacturer's goods");

            XElement rootItm = XDocument.Load("data.xml").Element("linqToXml").Element("items");
            XElement rootMan = XDocument.Load("data.xml").Element("linqToXml").Element("manufacturers");

            var result = rootItm.Elements("Item")
                .Where(i=>Int32.Parse(i.Element("manufacturer").Value)==3)
                .Join(rootMan.Elements("Manufacturer"),
                    i=>Int32.Parse(i.Element("manufacturer").Value),
                    m=>Int32.Parse(m.Element("id").Value),
                    (i,m)=>new{
                price=Int32.Parse(i.Element("price").Value),
                name=m.Element("name").Value
            });
            var sum=0;
            foreach(var r in result){
                sum += r.name=="Cheburashka"?r.price:0;
            }
            Console.WriteLine("Cheburashka's goods average price is {0}",sum/result.Count());
            Console.WriteLine();
        }
        static void Query11(){
            Console.WriteLine("showing items, avg price of which is under average");
            XElement root = XDocument.Load("data.xml").Element("linqToXml").Element("items");
            
            
            
            int sum=0;
            foreach(var i in root.Elements("Item")){
                sum+=Int32.Parse(i.Element("price").Value);
            }
            var result=root.Elements("Item").Where(i=>Int32.Parse(i.Element("price").Value)<sum/root.Elements("Item").Count());
            foreach(var r in result){
                Console.WriteLine(r.Element("name").Value);
            }
            Console.WriteLine();
        }
        static void Query12(){
            Console.WriteLine("showing avg price for goods of each manufacturer");
            
            var items = XDocument.Load("data.xml").Element("linqToXml").Element("items").Elements("Item");
            var manufacturers = XDocument.Load("data.xml").Element("linqToXml").Element("manufacturers").Elements("Manufacturer");


            foreach(var m in manufacturers){
                var result = items.Where(i=>Int32.Parse(i.Element("manufacturer").Value)==Int32.Parse(m.Element("id").Value));
                var sum=0;
                foreach(var r in result){
                    sum+=Int32.Parse(r.Element("price").Value);
                }
                Console.WriteLine("{0}'s goods avg price is:{1}",m.Element("name").Value,sum);
            }
            Console.WriteLine();
        }
        static void Query13(){
            Console.WriteLine("showing cheapest and the least numerical items");
            var items = XDocument.Load("data.xml").Element("linqToXml").Element("items").Elements("Item");
            var cheapest = items.Where(i=>Int32.Parse(i.Element("price").Value)<100);
            var leastNumerical = items.Where(i=>Int32.Parse(i.Element("amount").Value)<10);
            var result= cheapest.Concat(leastNumerical).Distinct();
            foreach(var r in result){
                    Console.WriteLine(r.Element("name").Value);
                }
            Console.WriteLine();
        }
        static void Query14(){
            Console.WriteLine("showing products names ordered by their names and their manufacturer's names");
            var items = XDocument.Load("data.xml").Element("linqToXml").Element("items").Elements("Item");
            var manufacturers = XDocument.Load("data.xml").Element("linqToXml").Element("manufacturers").Elements("Manufacturer");
            var result = from i in items
            join m in manufacturers on Int32.Parse(i.Element("manufacturer").Value) equals Int32.Parse(m.Element("id").Value)
                orderby i.Element("name").Value,m.Element("name").Value
                select new{
                    name=i.Element("name").Value
                };
            foreach(var r in result){
                Console.WriteLine(r.name);
            }
            Console.WriteLine();
        }
        static void Query15(){
            Console.WriteLine("showing each item delivered 2/2/2021");
            var items = XDocument.Load("data.xml").Element("linqToXml").Element("items").Elements("Item");
            var deliveries = XDocument.Load("data.xml").Element("linqToXml").Element("DeliveriesNodes").Elements("Deliveries");
             
            var preResult = deliveries.Where(d=>DateOnly.Parse(d.Element("date").Value).Year==2021 
                && DateOnly.Parse(d.Element("date").Value).Month==2 
                && DateOnly.Parse(d.Element("date").Value).Day==2);
            foreach(var pr in preResult){
                var result = pr.Element("items").Elements("id").Join(items,il=>il.Value,i=>i.Element("id").Value,(il,i)=>new{
                    name=i.Element("name").Value
                });
                foreach(var r in result){
                    Console.WriteLine(r.name);
                }
            }
           Console.WriteLine();
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public class Manufacturer{
            public int id{get;set;}
            public List<int>itemsId{get;set;}
            public string name{get;set;}

            
            public override string ToString()
            {
                string list = "";
                foreach(int i in itemsId){
                    list+=i+", ";
                }
                return string.Format("id:{0}, name:{1}, items:{2}",id,name,list);
            }
        }
        public class Deliveries{
            public int id;
            public DateOnly date;
            public List<int>items;
            public Deliveries(List<Item>list, DateOnly date, int deliveriesId){
                this.date=date;
                this.id=deliveriesId;
                this.items=new List<int>{};
                foreach (Item item in list){
                    foreach (int id in item.deliveriesId){
                        if (id==this.id){
                            this.items.Add(item.id);
                        }
                    }
                    
                }
            }
            public override string ToString()
            {
                string list = "";
                foreach(int i in items){
                    list+=i+", ";
                }
                return string.Format("id:{0}, date:{1}, items delivered:{2}",id,date,list);
            }

        }
        public class Item{
            public int id;
            public List<int> deliveriesId;
            public int Manufacturer;

            public int amount;
            public int price;
            public string name;

            public override string ToString()
            {
                string list = "";
                foreach(int i in deliveriesId){
                    list+=i+", ";
                }
                return string.Format("id:{0}, name:{1}, price:{2}, amount:{3}, deliveries:{4}, manufacturer:{5}",id,name,price,amount,list, Manufacturer);
            }
            
        }
        
        public class itemsDeliveries{
            public int itemId;
            public int deliveryId;
        }
    

    }
}
