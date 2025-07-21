using System;
using UnityEngine;

namespace Managers
{
    public class ResourcesFolderManager
    {
        
        
        private readonly string _weaponPath = "Weapon";
        private readonly string _bodyPath = "Body";

        private readonly string _thumbnail = "Thumbnail";
        
        public Sprite LoadThumbnailSprite(PartsType type, TierType tier, string name)
        {
            // Thumbnail_{Tier}_{Name}
            string tierS = tier.ToString();
            string path = "";

            switch (type)
            {
                case PartsType.Weapon:
                    path = _weaponPath;
                    break;
                case PartsType.Body:
                    path = _bodyPath;
                    break;
            }
                
            return Resources.Load<Sprite>($"{path}/{_thumbnail}_{tierS}_{name}");
        }
    }
}