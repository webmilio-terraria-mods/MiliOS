using Microsoft.Xna.Framework.Graphics;
using MiliOS.Taskbar;

namespace MiliOS.Softwares.Donut;

public class DonutTaskbarDescriptor : TaskbarDescriptor
{
    public DonutTaskbarDescriptor() : base(MiliOS.Instance.Assets.Request<Texture2D>("Softwares/Donut/icon"))
    {
    }
}