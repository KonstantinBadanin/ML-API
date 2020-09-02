using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using DataBase;
using WebML;

namespace Recognition
{
    public class TupleIn
    {
        public TupleIn() { }
        public byte[] Blob { get; set; }
        public string Name { get; set; }
    }

    class Inference
    {
        public static bool StopInference { get; private set; } = false;
        public static readonly object Locker = new object();
        private const int Width = 28;
        private const int Height = 28;
        public static List<Node> ListOfResults { get; private set; } = new List<Node>();//создаем лист результатов
        
        public void FinishInference()
        {
            StopInference = true;
        }

        public static Node Conversion(object img,object filename)
        {
            byte[] file = img as byte[];
            MemoryStream stream = new MemoryStream();
            stream.Write(file, 0, file.Length);
            Bitmap Cifer = new Bitmap(stream);
            float[] inputData = new float[Width * Height];

                for (int i = 0; i < Width; i++)
                    for (int j = 0; j < Height; j++)
                        inputData[i * Width + j] = Cifer.GetPixel(j, i).R;

            Node t=null;
            //using (var session = new InferenceSession("model.onnx"))
            //{
            //    var inputMeta = session.InputMetadata;
            //    var container = new List<NamedOnnxValue>();
            //    foreach (var name in inputMeta.Keys)
            //    {
            //        inputMeta[name].Dimensions[0] = 1;
            //        inputMeta[name].Dimensions[1] = 1;
            //        inputMeta[name].Dimensions[2] = Width;
            //        inputMeta[name].Dimensions[3] = Height;
            //        var tensor = new DenseTensor<float>(inputData, inputMeta[name].Dimensions);
            //        container.Add(NamedOnnxValue.CreateFromTensor<float>(name, tensor));
            //    }
            //    // Run the inference.
            //    var results = session.Run(container);
            //    foreach (var r in results)
            //    {
            //        lock (Locker)
            //        {
            //            t = new Node(r.AsEnumerable<float>(), filename as string, null);
            //            Usage.AddToDB(t as Item,file);
            //            if (!Usage.ContainsThisCounter(t.Cyfer))
            //            {
            //                Usage.AddCounterToDB(new Counter(t.Cyfer));
            //            }
            //            Usage.IncreaseCounterFromDB(t.Cyfer);
            //        }
            //    }
            //}
            return t;
        }
    }
}
