using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Buildings;
using System.Linq;

namespace GRANGERUltimate
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.GameLoop.TimeChanged += OnTimeChanged;
            helper.Events.Player.Warped += OnWarped;
        }

        // كل المتاجر 9–7
        private void OnWarped(object sender, WarpedEventArgs e)
        {
            if (e.NewLocation is ShopLocation shop)
            {
                shop.openingTime = 900;
                shop.closingTime = 1900;
            }
        }

        // البناء ليلة واحدة + تقليل مواد روبين 50٪
        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            foreach (var blueprint in CarpenterMenu.blueprints)
            {
                blueprint.daysToConstruct = 1;

                if (blueprint.itemsRequired != null)
                {
                    var keys = blueprint.itemsRequired.Keys.ToList();
                    foreach (var key in keys)
                    {
                        blueprint.itemsRequired[key] /= 2;
                    }
                }
            }
        }

        // كلينت: تطوير سريع للنحاس والحديد
        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {
            foreach (var tool in Game1.player.tools)
            {
                if (tool.UpgradeLevel <= 1)
                {
                    tool.UpgradeLevel++;
                }
            }
        }
    }
}
