using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
	public class Player : Character {
	public int Floor { get; set; }
        public Room Room { get; set; }
        [SerializeField]
        Encouter encounter;
        [SerializeField]
        public World world;
        [SerializeField]
        Mapping mapping;

        void Start ()
		{
			Floor = 0;
			Energy = 30;
			Attack = 10;
			Defence = 5;
			Gold = 0;
			Inventory = new List<string>();
			RoomIndex = new Vector2 (2,2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty = true;
            mapping = mapping.GetComponent<Mapping>();
            mapping.DrawPlayerOnMap();
            encounter.ResetDynamicControls();
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(2);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(3);
            }

            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                encounter.Attack();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                encounter.Flee();
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                encounter.OpenChest();
            }

        }

        public void Move(int direction)
        {
            if(this.Room.Enemy)
            {
                return;
            }
            
            if(direction == 0 && RoomIndex.y > 0)
            {
                Journal.Instance.Log("You move north.");
                //Mapping.Instance.Map("x");
                RoomIndex -= Vector2.up;
            }
            if(direction == 1 && RoomIndex.x < world.Dungeon.GetLength(0)-1)
            {
                Journal.Instance.Log("You move east.");
                //Mapping.Instance.Map("x");
                RoomIndex += Vector2.right;
            }
            //getlenght(1) koordinaatisto 0 = x  ja y = 1
            if(direction == 2 && RoomIndex.y < world.Dungeon.GetLength(1)-1)
            {
                Journal.Instance.Log("You move south.");
                //Mapping.Instance.Map("\n x");
                RoomIndex -= Vector2.down;
            }
            if(direction == 3 && RoomIndex.x > 0)
            {
                Journal.Instance.Log("You move west.");
                //Mapping.Instance.Map(" x");
                RoomIndex += Vector2.left;
            }
            if(this.Room.RoomIndex != RoomIndex)
            {
                Investigate();
            }
            
        }

        public void Investigate()
        {
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            mapping.DrawPlayerOnMap();
            encounter.ResetDynamicControls();
            if(this.Room.Empty)
            {
                Journal.Instance.Log("You find yourself in an empty room.");
            }
            else if(this.Room.Chest != null)
            {
                Journal.Instance.Log("You've found a chest! What would you like to do?");
                encounter.StartChest();
            }
            else if (this.Room.Enemy != null)
            {
                Journal.Instance.Log("You are jumped by a " + Room.Enemy.Description + "! What would you like to do?");
                encounter.StartCombat();
            }
            else if (this.Room.Exit)
            {
                
                Journal.Instance.Log("You've found the exit to the next floor. Would you like to continue?");
                encounter.StartExit();
                mapping.DrawPlayerOnMap();
            }
            
        }

		public void AddItem(string item)
		{
            Journal.Instance.Log("You were given item: " + item);
            Inventory.Add(item);
            UIController.OnPlayerInventoryChange();
        }

        public void Additem(int item)
        {
            Inventory.Add(ItemDatabase.Instance.Items[item]);
            UIController.OnPlayerInventoryChange();
        }

        public void  NewFloorStartingpPoint()
        {
            RoomIndex = new Vector2(2, 2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty = true;
            mapping.DrawPlayerOnMap();
            encounter.ResetDynamicControls();
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
        }

		public override void TakeDamage(int amount)
		{
			Debug.Log("Player TakeDamge");
			base.TakeDamage(amount);
            UIController.OnPlayerStatChange();
        }
		public override void Die()
		{
			Debug.Log("Player died. Game Over!");
			base.Die();
		}
	}
}
