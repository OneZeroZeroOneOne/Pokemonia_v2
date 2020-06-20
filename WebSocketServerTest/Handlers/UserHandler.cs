using Pokemonia.Dal.LogicModels;
using Pokemonia.Dal.Models;
using Pokemonia.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemonia.WebServer.Handlers
{
    public class UserHandler : BaseHandler
    {
        private Dictionary<int, MapDataHolder> _collections;
        public UserHandler(Dictionary<int, MapDataHolder> collections) : base ()
        {
            _collections = collections;
        }
        public void AddUserOnMap(UserConnection userConnection, int mapId)
        {
            if(_collections.TryGetValue(mapId, out MapDataHolder collection))
            {
                collection.users.Add(userConnection.GetUser());
                userConnection.SetMapId(mapId);
                userConnection.Send(_byteMessageFactory.AddedUser());
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.MapNotFound, "Map not found");
            }
            
        }

        public void ChangeMap(UserConnection userConnection, int mapId)
        {
            DisconnectUserFromMap(userConnection);
            AddUserOnMap(userConnection, mapId);
            userConnection.Send(_byteMessageFactory.ChangeMap(mapId));
        }

        public void DisconnectUserFromMap(UserConnection userConnection)
        {
            if(_collections.TryGetValue(userConnection.GetMapId(), out MapDataHolder collection))
            {
                collection.disconnectUser.Add(userConnection.GetUser());
                userConnection.Send(_byteMessageFactory.DisconnectUserFromMap(userConnection.GetMapId()));
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.MapNotFound, "Map not found");
            }
            
        }

        public void AddMoveCoordinates(UserConnection userConnection, int x, int y)
        {
            //понять як і куда його блять всунуть 
        }


    }
}
