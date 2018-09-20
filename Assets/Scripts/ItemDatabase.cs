using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
	public class ItemDatabase : MonoBehaviour 
	{
		public List<string> Items { get; set; } = new List<string>();
        public List<Item> itemList = new List<Item>();
		public static ItemDatabase Instance { get; private set; }

		private void Awake()
		{
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                BuildDatabase();
            }

			 Items.Add("Emerald Slipper");
			 Items.Add("Empty Chalice");
             Items.Add("Bowtie");
		}

        public Item GetItem(int id)
        {
            return itemList.Find(item => item.id == id);
        }

        public Item GetItem(string itemName)
        {
            return itemList.Find(item => item.name == itemName);
        }

        void BuildDatabase()
        {
            itemList = new List<Item>()
            {
                new Item(0, "Iron sword", " A sword made of iron.",
            new Dictionary<string, int>
            {
                {"Power", 15},
                {"Value", 10}
            } ),
                 new Item(1, "ore diamond", "A pretty diamond.",
            new Dictionary<string, int>
            {
                {"Value", 300}
            } ),
                  new Item(2, "Iron armor", "A armor made of iron",
            new Dictionary<string, int>
            {
                {"Defence", 5},
                {"Value", 20}
            } ),
                  new Item(3, "potion", "Restore HP",
            new Dictionary<string, int>
            {
                {"Heal", 5},
                {"Value", 5}
            } ),
            };
        }
	}
}
