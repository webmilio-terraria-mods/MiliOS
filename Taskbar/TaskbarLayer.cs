using Microsoft.Xna.Framework;
using Terraria.UI;
using WebmilioCommons.UI;

namespace MiliOS.Taskbar;

public class TaskbarLayer : UILayer<TaskBar>
{
    public TaskbarLayer() : base(new(), nameof(TaskbarLayer), InterfaceScaleType.UI)
    {
        IsVisible = true;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsVisible)
            Interface.Update(gameTime);
    }
}