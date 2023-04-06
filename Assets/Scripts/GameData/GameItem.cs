using System;
using UnityEngine;

namespace GameData
{
   [CreateAssetMenu(fileName = "GameItem", menuName = "GameItem")]
   public class GameItem : ScriptableObject, IComparable<GameItem>
   {
      public string Name => _name;
      public Sprite View => _view;

      [SerializeField] private string _name;

      [SerializeField] private Sprite _view;

      public int CompareTo(GameItem other)
      {
         if (_name == other.Name)
         {
            return 1;
         }

         return -1;
      }
   }
}
