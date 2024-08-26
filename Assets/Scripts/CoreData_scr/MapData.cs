using System;
using System.Collections.Generic;
using UnityEngine;
using Yee.Math;

namespace RTank.CoreData
{
    [CreateAssetMenu(menuName = "Map Data", fileName = "New Map Data")]
    public class MapData : ScriptableObject
    {
        [SerializeField] int rows;
        [SerializeField] int columns;
        [SerializeField] int radarLoads;

        long obstaclePositions;
        
        public int Rows => rows;
        public int Columns => columns;
        public int TotalTiles => rows * columns;
        public int RadarLoads => radarLoads;
        public float MidRow => (rows - 1) * 0.5f;
        public float MidColumn => (columns - 1) * 0.5f;

        public void ResetTile() => obstaclePositions = 1;
        public void RemoveFromTile(long l) => obstaclePositions &= l;
        public void AddToTile(long l) => obstaclePositions |= l;

        public long GetTile(int x, int z) => (long)Mathf.Pow(2, z * columns + x);

        public Vector3 GetCoordinate(int index, float yPosition)
        {
            int row = index / columns;
            int column = index % columns;

            return new Vector3(column, yPosition, row);
        }

        public bool CanMoveToTile(Vector3 point)
        {
            bool isOutOfBounds = point.x < 0 || point.x >= columns || point.z < 0 || point.z >= rows; 

            long tile = GetTile((int)point.x, (int)point.z);

            return (tile & obstaclePositions) != 0 || isOutOfBounds;
        }

        public bool TileIsOccupied(int point)
        {
            long pointInBit = (long)Mathf.Pow(2, point);

            return (pointInBit & obstaclePositions) != 0;
        }

        public List<int> GetFreeTiles(bool removeInitialRange = false)
        {
            List<int> freeSpaces = new List<int>();

            string invertedOccupiedSpaces = Convert.ToString(obstaclePositions, 2);
            string occupiedSpaces = MathY.Reverse(invertedOccupiedSpaces);

            for (int i = 1; i < TotalTiles; i++)
            {
                if (occupiedSpaces.Length > i) { if (occupiedSpaces[i] == '1') { continue; } }

                if (removeInitialRange)
                {
                    Vector3 point = GetCoordinate(i, 0);
                    if (point.x < 3 && point.z < 3) { continue; }
                }

                freeSpaces.Add(i);
            }

            return freeSpaces;
        }
    }
}