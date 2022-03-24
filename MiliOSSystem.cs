using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using WebmilioCommons.Inputs;
using WebmilioCommons.UI;

namespace MiliOS;

public class MiliOSSystem : ModSystem
{
    public override void Load()
    {
        KeybindAttribute.RegisterKeybinds(this);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        ExplorerLayer.ModifyInterfaceLayers(layers);
    }

    public override void PostUpdateEverything()
    {
        Explorer.ForRunning(app => app.PostUpdateEverything());
    }

    public override void PostUpdateInput()
    {
        try
        {
            if (ShowMenu.JustPressed)
            {
                Explorer.Taskbar.Toggle();
            }
        }
        catch
        {
            ;
        }

        Explorer.ForRunning(app => app.PostUpdateInput());
    }

    public override void PreUpdateEntities()
    {
        Explorer.ForRunning(app => app.PreUpdateEntities());
    }

    public override void UpdateUI(GameTime gameTime)
    {
        ExplorerLayer.Update(gameTime);
        Explorer.ForRunning(app => app.UpdateUI(gameTime));
    }

    private UILayer<Explorer.Explorer> ExplorerLayer { get; } = new(new(), nameof(ExplorerLayer), InterfaceScaleType.UI)
    {
        IsVisible = true
    };

    public Explorer.Explorer Explorer => ExplorerLayer.State;

    [Keybind("Show Menu", Keys.OemTilde)]
    public ModKeybind ShowMenu { get; set; }
}