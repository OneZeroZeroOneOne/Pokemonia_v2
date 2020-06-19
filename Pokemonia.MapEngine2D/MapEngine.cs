using System;
using System.Collections.Generic;
using Pokemonia.Utils;
using Pokemonia.Dal.Models;
using Pokemonia.Dal.LogicModels;
using Pokemonia.Bll.Services;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

namespace Pokemonia.MapEngine2D
{
    public class MapEngine
    {
        //configs
        private Random _random;
        //map
        private MapServiceDB _mapServiceDB;
        private Map _map;
        private BlockingCollection<InfoCurrentStateMap> _outInfoCurrentStateMap;
        private int _spawnX;
        private int _spawnY;

        //user
        private Dictionary<long, User> _users = new Dictionary<long, User>();
        private Dictionary<long, Coordinates<User>> _usersCoordinates = new Dictionary<long, Coordinates<User>>();
        private Dictionary<long, Coordinates<User>> _usersMoveCoordinates = new Dictionary<long, Coordinates<User>>();
        private BlockingCollection<User> _usersBlockingCollection;
        private BlockingCollection<Coordinates<User>> _usersMoveCoordinatesBlockingCollection;

        //monsters
        private Dictionary<Guid, Monster> _monsters = new Dictionary<Guid, Monster>();
        private Dictionary<Guid, Coordinates<Monster>> _monstersCoordinates = new Dictionary<Guid, Coordinates<Monster>>();
        private Dictionary<Guid, Coordinates<Monster>> _monstersMoveCoordinates = new Dictionary<Guid, Coordinates<Monster>>();
        private BlockingCollection<Monster> _killMonsterBlockingCollection;
        public MapEngine(Map map, BlockingCollection<User> usersBlockingCollection
                                                        , BlockingCollection<Coordinates<User>> usersMoveCoordinatesBlockingCollection
                                                        , BlockingCollection<Monster> killMonsterBlockingCollection
                                                        , BlockingCollection<InfoCurrentStateMap> outInfoBlockingCollection)
        {
            _map = map;
            _spawnX = 400;
            _spawnY = 300;
            _usersBlockingCollection = usersBlockingCollection;
            _usersMoveCoordinatesBlockingCollection = usersMoveCoordinatesBlockingCollection;
            _killMonsterBlockingCollection = killMonsterBlockingCollection;
            _outInfoCurrentStateMap = outInfoBlockingCollection;
            _random = new Random();
            _mapServiceDB = new MapServiceDB();
            User user1 = new User() { Id = 1, Name = "Dima" };
            _users.Add(1, user1);
            _usersCoordinates.Add(1, new Coordinates<User>() { Model = user1, x = _spawnX, y = _spawnY });
            Guid mId1 = Guid.NewGuid();
            _monsters.Add(mId1, new Monster() { Id = mId1, Name = "Pidrilo" });
            _monstersCoordinates.Add(mId1, new Coordinates<Monster>() { x = _spawnX, y = _spawnY });
        }

        public void Run()
        {
            Console.WriteLine($"Start map {_map.Name}");
            while (true)
            {
                try
                {
                    User[] users = _usersBlockingCollection.ToArray();
                    for (int i = 0; i<users.Length; i++)
                    {
                        _users.Add(users[i].Id, users[i]);
                        Console.WriteLine($"added user name {users[i].Name}");
                        _usersCoordinates.Add(users[i].Id, new Coordinates<User>() { Model = users[i], x = _spawnX, y = _spawnY });
                    }
                    Coordinates<User>[] moveCoordinates = _usersMoveCoordinatesBlockingCollection.ToArray();
                    for (int i = 0; i < moveCoordinates.Length; i++)
                    {
                        if(_users.TryGetValue(moveCoordinates[i].Model.Id, out User user))
                        {
                            CalculateCoordinates.CalculateCoef(moveCoordinates[i],
                                _usersCoordinates.GetValueOrDefault(user.Id));
                            _usersMoveCoordinates.Add(user.Id, moveCoordinates[i]);
                        }
                    }
                    MoveUserObjects();
                    MoveMonsterObjects();
                    KillMonsters();
                    SpawnMonsters();
                }
                catch (InvalidOperationException) { }

            }
        }

        private void MoveUserObjects()
        {
            foreach (var userMoveCoord in _usersMoveCoordinates)
            {
                if (_users.TryGetValue(userMoveCoord.Key, out User user))
                {
                    _usersCoordinates.TryGetValue(userMoveCoord.Key, out Coordinates<User> userCoord);
                    CalculateCoordinates.CalculatePosition(userCoord, userMoveCoord.Value, _map.MoveDistance);
                    Console.WriteLine($"user {user.Name} coord x - {_usersCoordinates.GetValueOrDefault(user.Id).x}, y {_usersCoordinates.GetValueOrDefault(user.Id).y}");
                }
            }
            
        }

        private void MoveMonsterObjects()
        {
            foreach(var monsterCoord in _monstersCoordinates)
            {
                Coordinates<Monster> moveMonsterCoord;
                
                if (!_monstersMoveCoordinates.TryGetValue(monsterCoord.Key, out moveMonsterCoord))
                {
                    //генерим новую точку и считаем переменные направления

                    Coordinates<Monster> moveCoord = new Coordinates<Monster>();

                    CalculateCoordinates.GenerateNewMoveCoord(monsterCoord.Value, moveCoord, _map.Width, _map.Height);
                    _monstersMoveCoordinates.Add(monsterCoord.Key, moveCoord);
                }
                else
                {
                    if (DateTime.Now > monsterCoord.Value.TimeOffset)
                    {
                        CalculateCoordinates.CalculatePosition(monsterCoord.Value, moveMonsterCoord, _map.MoveDistance);
                        if (CalculateCoordinates.CheckBorder(monsterCoord.Value, moveMonsterCoord))
                        {
                            CalculateCoordinates.GenerateNewMoveCoord(monsterCoord.Value, moveMonsterCoord, _map.Width, _map.Height);
                        }
                        Console.WriteLine($"Mapid - {_map.Id} Monster {_monsters.GetValueOrDefault(monsterCoord.Key).Name} coord x - {monsterCoord.Value.x}, y - {monsterCoord.Value.y}");
                    }
                }

            }
        }
        private void KillMonsters()
        {
            Monster[] DeadMonsters = _killMonsterBlockingCollection.ToArray();
            for(int i = 0; i < DeadMonsters.Length; i++)
            {
                _monsters.Remove(DeadMonsters[i].Id);
                _monstersCoordinates.Remove(DeadMonsters[i].Id);
                _monstersMoveCoordinates.Remove(DeadMonsters[i].Id);
            }
        }

        private void SpawnMonsters()
        {
            if(_monsters.Count < _map.MonstersQuantity)
            {
                for(int i = 0; i< _map.MonstersQuantity - _monsters.Count; i++)
                {
                    Console.WriteLine("spawning monster");
                    Guid mId = Guid.NewGuid();
                    _monsters.Add(mId, new Monster() { Id = mId, Name = "Debs" });
                    _monstersCoordinates.Add(mId, new Coordinates<Monster>()
                    {
                        x = _random.Next(0, _map.Width),
                        y = _random.Next(0, _map.Height)
                    });
                }
            }
        }





    }
}
