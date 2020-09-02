using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataBase;
using System.Data.Entity;

namespace WebML
{
    public class Usage
    {
        public static bool ContainsThisName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException(paramName: nameof(Name));
            using (Context db = new Context())
            {
                var s = (from item in db.Items where item.Path == Name select item).ToArray();
                return (s.Count() != 0);
            }
        }

        public static bool ContainsThisCounter(string Cyfer)
        {
            if (string.IsNullOrEmpty(Cyfer))
                throw new ArgumentNullException(paramName: nameof(Cyfer));
            using (Context db = new Context())
            {
                var s = (from item in db.Counters where item.Id == Cyfer select item).ToArray();
                return (s.Count() != 0);
            }
        }

        public static void AddCounterToDB(Counter mo)
        {
            if (mo == null)
                throw new ArgumentNullException(paramName: nameof(mo));
            using (Context db = new Context())
            {
                db.Counters.Add(new Counter(mo));
                db.SaveChanges();
            }
        }

        public static void IncreaseCounterFromDB(string Cyfer)
        {
            if (string.IsNullOrEmpty(Cyfer))
                throw new ArgumentNullException(paramName: nameof(Cyfer));
            using (Context db = new Context())
            {
                var s = (from item in db.Counters where item.Id == Cyfer select item).ToArray();
                if (s.Count() != 0)
                {
                    foreach (var item in s)
                        item.Count++; ;
                    db.SaveChanges();
                    return;
                }
                else
                    return;
            }
        }

        public static Item GetItemFromDB(string FullName)
        {
            if (string.IsNullOrEmpty(FullName))
                throw new ArgumentNullException(paramName: nameof(FullName));
            using (Context db = new Context())
            {
                var s = (from item in db.Items where item.Path == FullName select item).ToArray();
                if (s.Count() != 0)
                {
                    foreach (var item in s)
                        return item;
                    return null;
                }
                else
                    return null;
            }
        }

        public static List<Item> GetItemsFromDB()
        {
            using (Context db = new Context())
            {
                return db.Items.ToList();
            }
        }

        public static byte[] GetBlobById(int Id)
        {
            using (Context db = new Context())
            {
                var s = from item in db.BlobsClasses where item.Id == Id select item.Blob;
                foreach (var item in s)
                    return item;
                return null;
            }
        }

        public static void AddToDB(Item imgInfo, byte[] blob)
        {
            if (imgInfo == null)
                throw new ArgumentNullException(paramName: nameof(imgInfo));
            using (Context db = new Context())
            {
                db.Items.Add(imgInfo);
                db.BlobsClasses.Add(new BlobClass(imgInfo.Id, blob));
                db.SaveChanges();
            }
        }

        public static int IdContainingThisBlob(byte[] arr)
        {
            using (Context db = new Context())
            {
                bool flagOfEqual;
                foreach (var item in db.BlobsClasses)
                {
                    flagOfEqual = true;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (item.Blob[i] != arr[i])
                        {
                            flagOfEqual = false;
                            break;
                        }
                    }
                    if (flagOfEqual) return item.Id;
                }
                throw new InvalidOperationException();
            }
        }

        public static List<string> GetCountersFromDB()
        {
            using (Context db = new Context())
            {
                if (!db.Database.Exists())
                {
                    db.Database.Create();
                }
                //byte[] file = File.ReadAllBytes("4_2.bmp");
                //db.Items.Add(new Item() { Id = 4 });
                //db.BlobsClasses.Where(x=>x.Id == 4).First().Blob = file;
                //file = File.ReadAllBytes("5_1.bmp");
                //db.Items.Add(new Item() { Id = 2 });
                //db.BlobsClasses.Where(x => x.Id == 2).First().Blob = file;
                //file = File.ReadAllBytes("5_2.bmp");
                //db.Items.Add(new Item() { Id = 3 });
                //db.BlobsClasses.Where(x => x.Id == 3).First().Blob = file;
                //db.SaveChanges();

                List<string> lst = new List<string>();
                if (db.Counters != null)
                {
                    foreach (var item in db.Counters)
                    {
                        lst.Add(item.Id);
                    }
                }
                return lst;
            }
        }

        public static byte[] GetBlobFromDBByNumAndClass(string classOfItem, int num)
        {
            if (string.IsNullOrEmpty(classOfItem))
                throw new ArgumentNullException(paramName: nameof(classOfItem));
            using (Context db = new Context())
            {
                var a = from item in db.Items where item.Cyfer == classOfItem select item.Blob.Blob;
                int k = 0;
                foreach (var item in a)
                {
                    if (k == num) return item;
                    k++;
                }
                return null;
            }
        }

        public static void Clean()
        {
            using (Context db = new Context())
            {
                db.Clear();
            }
        }
    }
}
