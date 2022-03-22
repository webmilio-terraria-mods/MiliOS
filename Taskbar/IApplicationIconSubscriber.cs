namespace MiliOS.Taskbar;

public interface IApplicationIconSubscriber
{
    public void HoveredApplicationChanged(TaskbarDescriptor icon);
}