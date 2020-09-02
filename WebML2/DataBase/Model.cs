using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace DataBase
{
    public struct In
    {
        public int Name { get; set; }
    }
    public class Counter
    {
        public int Count { get; set; }
        public string Id { get; set; }

        public Counter() { }
        
        public Counter(Counter mdl)
        {
            if (mdl == null)
                throw new ArgumentNullException(paramName: nameof(mdl));
            Count = mdl.Count;
            Id = mdl.Id;
        }

        public Counter(string Cyfer)
        {
            if (string.IsNullOrEmpty(Cyfer))
                throw new ArgumentNullException(paramName: nameof(Cyfer));
            Id = Cyfer;
            Count = 0;
        }
    }
    public class BlobClass
    {
        [Key]
        [ForeignKey("Info")]
        public int Id { get; set; }
        public byte[] Blob { get; set; }
        public Item Info { get; set; }
        public BlobClass() { }

        public BlobClass(int id,byte[] blob)
        {
            Id = id;
            Blob = blob;
        }
    }

    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public double Probability { get; set; }
        public string Cyfer { get; set; }
        public string Scores { get; set; }
        public virtual BlobClass Blob { get; set; }

        [NotMapped]
        public float[] DataForScores
        {
            get
            {
                return Array.ConvertAll(Scores.Split(';'), Single.Parse);
            }
            set
            {
                var _data = value;
                Scores = string.Join(";", _data.Select(p => p.ToString()).ToArray());
            }
        }

        public Item() { }
    }

    [NotMapped]
    public class Node : Item
    {
        public string FullName { get; set; }
        public Node(Item mdl,string Fullname)
        {
            Scores = mdl.Scores;
            Id = mdl.Id;
            Path = mdl.Path;
            Probability = mdl.Probability;
            Cyfer = mdl.Cyfer;
            FullName = Fullname;
        }
        public Node(IEnumerable<float> arg1, string arg2, string Fullname)
        {
            FullName = Fullname;
            var scr = new float[10];
            int i = 0;
            foreach (var item in arg1)
            {
                scr[i] = item;
                i++;
            }
            DataForScores = scr;
            Path = arg2;
            double sum = 0;
            int Cyfer1 = 0;
            for (int k = 0; k < 10; k++)
            {
                sum += Math.Exp(DataForScores[k] / 1000);
                if (DataForScores[Cyfer1] < DataForScores[k])
                    Cyfer1 = k;
            }
            Cyfer = Cyfer1.ToString();
            Probability = Math.Exp(DataForScores[Cyfer1] / 1000) * 100 / sum;
        }
    }
}
