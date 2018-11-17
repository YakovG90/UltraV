using System;
using System.IO;
using Domain.Enums;

namespace Domain.Models
{
    public class CharacterViewModel
    {
        public string Name { get; set; }
        
        public int Level { get; set; }
        
        public CharacterClass CharacterClass { get; set; }
        
        public ItemViewModel Items { get; set; } = new ItemViewModel();
        
        public int AchievementPoints { get; set; }
        
        public SpecViewModel Spec { get; set; }
        
        public string PictureUrl { get; set; }
        
        public string Image { get; set; }
        
        public Guid PublicId { get; set; }
    }

    public class SpecViewModel
    {
        public string Name { get; set; }
        
        public string Role { get; set; }
    }

    public class ItemViewModel
    {
        public int AverageItemLevelEquipped { get; set; }
    }
}