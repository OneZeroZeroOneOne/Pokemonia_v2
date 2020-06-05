using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Pokemonia.Dal.LogicModels
{
    public class Coordinates<T>
    {
        public T Model { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double Gip { get; set; }
        public double CoefX { get; set; }
        public double CoefY { get; set; }
        public int SignX { get; set; }
        public int SignY { get; set; }


    }
}


