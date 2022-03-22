using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiliOS.Taskbar;
using Terraria.ModLoader;
using Terraria.UI;
using WebmilioCommons.Inputs;

namespace MiliOS;

public class MiliOSSystem : ModSystem
{
    public override void Load()
    {
        KeybindAttribute.RegisterKeybinds(this);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        Taskbar.ModifyInterfaceLayers(layers);
    }

    public override void PostUpdateInput()
    {
        if (ShowMenu.JustPressed)
        {
            Taskbar.State.ToggleHidden();
        }
    }

    public override void UpdateUI(GameTime gameTime)
    {
        Taskbar.Update(gameTime);
    }

    public TaskbarLayer Taskbar { get; } = new();

    [Keybind("Show Menu", Keys.OemTilde)]
    public ModKeybind ShowMenu { get; set; }
}