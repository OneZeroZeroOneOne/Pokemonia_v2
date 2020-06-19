using System;
using System.Collections.Generic;
using System.Text;
using Pokemonia.Dal.LogicModels;
using Pokemonia.Dal.Models;

namespace Pokemonia.Utils
{
    static public class CalculateCoordinates
    {
        public static void CalculateCoef<T>(Coordinates<T> moveCoordinates, Coordinates<T> coordinates)
        {
            double lengthX = moveCoordinates.x - coordinates.x;
            double lengthY = moveCoordinates.y - coordinates.y;
            moveCoordinates.Gip = Math.Sqrt(lengthX * lengthX + lengthY * lengthY);
            moveCoordinates.CoefX = Math.Abs(lengthX) / moveCoordinates.Gip;
            moveCoordinates.CoefY = Math.Abs(lengthY) / moveCoordinates.Gip;
            moveCoordinates.SignX = Math.Sign(lengthX);
            moveCoordinates.SignY = Math.Sign(lengthY);
        }

        public static void CalculatePosition<T>(Coordinates<T> coordinates, Coordinates<T> moveCoordinates, double distance)
        {
            
            coordinates.x += (moveCoordinates.CoefX * distance) * moveCoordinates.SignX;
            coordinates.y += (moveCoordinates.CoefY * distance) * moveCoordinates.SignY;
            
        }

        public static bool CheckBorder<T>(Coordinates<T> coordinates, Coordinates<T> moveCoordinates)
        {
            if (moveCoordinates.SignX > 0)
            {
                if (coordinates.x > moveCoordinates.x)
                {
                    coordinates.x = moveCoordinates.x;
                    coordinates.y = moveCoordinates.y;
                    return true;
                }
                return false;
            }
            else
            {
                if (coordinates.x < moveCoordinates.x)
                {
                    coordinates.x = moveCoordinates.x;
                    coordinates.y = moveCoordinates.y;
                    return true;
                }
                return false;
            }
            
        }
        public static void GenerateNewMoveCoord<T>(Coordinates<T> coordinates, Coordinates<T> newMoveCoordinates, int maxX, int maxY)
        {
            Random random = new Random();
            newMoveCoordinates.x = random.Next(0, maxX);
            newMoveCoordinates.y = random.Next(0, maxY);
            CalculateCoef(newMoveCoordinates, coordinates);
        }
    }
}
