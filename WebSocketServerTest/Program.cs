using Pokemonia.Bll.Services;
using System;
using Pokemonia.Dal.Models;
using System.Collections.Generic;
using Pokemonia.MapEngine2D;
using Pokemonia.Dal.LogicModels;

namespace Pokemonia.WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MapEngineGenerator mapEngineGenerator = new MapEngineGenerator();
            mapEngineGenerator.RunMapsEngine();
            SocketServer server = new SocketServer(mapEngineGenerator.GetCollections());
        }
    }
}
