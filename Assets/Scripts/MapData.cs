using UnityEngine;

namespace RTank.CoreData
{
    [CreateAssetMenu(menuName = "Map Data", fileName = "New Map Data")]
    public class MapData : ScriptableObject
    {
        [SerializeField] int rows;
        [SerializeField] int columns;

        public int Rows => rows;
        public int Columns => columns;
        public float MidRow => (rows - 1) * 0.5f;
        public float MidColumn => (columns - 1) * 0.5f;
    }
}