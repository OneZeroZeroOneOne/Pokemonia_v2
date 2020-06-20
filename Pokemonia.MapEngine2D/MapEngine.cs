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
        private InfoCurrentStateMap _outInfoCurrentStateMap;
        private int _spawnX;
        private int _spawnY;

        //user
        private Dictionary<long, User> _users = new Dictionary<long, User>();
        private Dictionary<long, Coordinates<User>> _usersCoordinates = new Dictionary<long, Coordinates<User>>();
        private Dictionary<long, Coordinates<User>> _usersMoveCoordinates = new Dictionary<long, Coordinates<User>>();
        private List<User> _usersOut;
        private List<Coordinates<User>> _usersMoveCoordinatesOut;
        private List<User> _disconnectUserOut;

        //monsters
        private Dictionary<Guid, Monster> _monsters = new Dictionary<Guid, Monster>();
        private Dictionary<Guid, Coordinates<Monster>> _monstersCoordinates = new Dictionary<Guid, Coordinates<Monster>>();
        private Dictionary<Guid, Coordinates<Monster>> _monstersMoveCoordinates = new Dictionary<Guid, Coordinates<Monster>>();
        private List<Monster> _killMonstersOut;
        public MapEngine(Map map, MapDataHolder mapDataHolder)
        {
            _map = map;
            _spawnX = 400;
            _spawnY = 300;
            _usersOut = mapDataHolder.users;
            _usersMoveCoordinatesOut = mapDataHolder.usersMoveCoordinates;
            _killMonstersOut = mapDataHolder.killMonsters;
            _outInfoCurrentStateMap = mapDataHolder.outInfoCurrentStateMap;
            _disconnectUserOut = mapDataHolder.disconnectUser;
            _random = new Random();
            _mapServiceDB = new MapServiceDB();
        }

        public void Run()
        {
            Console.WriteLine($"Start map {_map.Name}");
            while (true)
            {
                try
                {
                    GetUsers();
                    MoveUser();
                    MoveMonsterObjects();
                    KillMonsters();
                    SpawnMonsters();
                    DisconnectUser();
                }
                catch (InvalidOperationException) { }

            }
        }

        private void GetUsers()
        {
            User[] users = _usersOut.ToArray();
            _usersOut.Clear();
            for(int i = 0; i < users.Length; i++)
            {
                _users.Add(users[i].Id, users[i]);
                _usersCoordinates.Add(users[i].Id, new Coordinates<User>()
                {
                    x = _spawnX,
                    y = _spawnY,
                });
                Console.WriteLine($"added user name {users[i].Name}");
            }
        }

        private void MoveUser()
        {
            Coordinates<User>[] newMoveCoordinates = _usersMoveCoordinatesOut.ToArray();
            _usersMoveCoordinatesOut.Clear();
            for (int i = 0; i < newMoveCoordinates.Length; i++)
            {
                if(_users.ContainsKey(newMoveCoordinates[i].Model.Id))
                {
                    CalculateCoordinates.CalculateCoef(newMoveCoordinates[i],
                        _usersCoordinates.GetValueOrDefault(newMoveCoordinates[i].Model.Id));
                    if (_usersMoveCoordinates.ContainsKey(newMoveCoordinates[i].Model.Id))
                    {
                        _usersMoveCoordinates.Remove(newMoveCoordinates[i].Model.Id);
                    }
                    _usersMoveCoordinates.Add(newMoveCoordinates[i].Model.Id, newMoveCoordinates[i]);
                }
            }
            foreach(var userMoveCoord in _usersMoveCoordinates)
            {
                var userCoord = _usersCoordinates.GetValueOrDefault(userMoveCoord.Key);
                if (DateTime.Now > userCoord.TimeOffset)
                {
                    CalculateCoordinates.CalculatePosition(userCoord, userMoveCoord.Value, _map.MoveDistance);
                    CalculateCoordinates.CheckBorder(userCoord, userMoveCoord.Value);
                    Console.WriteLine($"user {_users.GetValueOrDefault(userMoveCoord.Key).Name} coord x - {userCoord.x}, y {userCoord.y}");
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
            Monster[] DeadMonsters = _killMonstersOut.ToArray();
            _killMonstersOut.Clear();
            for (int i = 0; i < DeadMonsters.Length; i++)
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

        private void DisconnectUser()
        {
            User[] disconnectUsers = _disconnectUserOut.ToArray();
            _disconnectUserOut.Clear();
            for (int i = 0; i < disconnectUsers.Length; i++)
            {
                _users.Remove(disconnectUsers[i].Id);
                _usersCoordinates.Remove(disconnectUsers[i].Id);
                _usersMoveCoordinates.Remove(disconnectUsers[i].Id);
            }
        }
    }   
}
