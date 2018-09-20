using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TextRPG
{
    public class Mapping : MonoBehaviour
    {
        [SerializeField] Text minimapText;

        [SerializeField]
        private World world;

        /* [SerializeField]
         private Player player;*/
        Player player;
        public static Mapping Instance { get; set; }

        char[,] map;
        const char roomChar = '▢';
        const char playerChar= '◯';
        const char exitChar = '•';
        const char enemyChar = '≡';
        const char chestChar = '⌂';
        const char visitRoom = 'x';
        // Extra credit for adding walls!
        const char verticalWall = '║';
        const char horizontalWall = '═';
        const char upperLeftCorner = '╔';
        const char upperRightCorner = '╗';
        const char lowerLeftCorner = '╚';
        const char lowerRightCorner = '╝';

        private void Start()
        {
            player = this.transform.GetComponentInParent<Player>();
            int mapWidth = ( int)world.Grid.x;
            int mapHeight = (int)world.Grid.y;

            map = new char[mapWidth, mapHeight];
            Array.Clear(map, 0, map.Length);
            map[(int)player.RoomIndex.x, (int)player.RoomIndex.y] = playerChar;
            //map[(int)world.exitLocation.x, (int)world.exitLocation.y] = exitChar;
            //System.Random random = new System.Random(666);
            //map[random.Next(0, mapWidth), random.Next(0, mapHeight)] = thing;
            //Console.WriteLine(DrawMap(map));
            //Journal.Instance.Log(DrawMap(map));
            //Log(DrawMap(map));
            //Debug.Log(DrawMap(map));
           
        }

        static string DrawMap(char[,] map)
        {
            string result = "";
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                for (int x = 0; x < map.GetLength(0); ++x)
                {
                    if (map[x, y] == 0)
                    {
                        result += roomChar;
                    }
                    else
                    {
                        result += map[x, y];
                    }
                }
                result += '\n';
            }
            return result;
        }

        public void Log(string text)
        {
            minimapText.text += text;
        }

        public void DrawPlayerOnMap()
        {
            minimapText.text = "";
            Array.Clear(map, 0, map.Length);
            if(player.Room.Enemy != null)
            {
                map[(int)player.RoomIndex.x, (int)player.RoomIndex.y] = enemyChar;
                minimapText.color = Color.red;
            }
            else if (player.Room.Chest != null)
            {
                map[(int)player.RoomIndex.x, (int)player.RoomIndex.y] = chestChar;
                minimapText.color = Color.yellow;
            }
            else if (player.Room.Exit)
            {
                map[(int)world.exitLocation.x, (int)world.exitLocation.y] = exitChar;
                minimapText.color = Color.green;
            }
            else
            {
                map[(int)player.RoomIndex.x, (int)player.RoomIndex.y] = playerChar;
                minimapText.color = Color.white;
            }

            map[(int)world.exitLocation.x, (int)world.exitLocation.y] = exitChar;
            Log(DrawMap(map));
        }
    }
}
