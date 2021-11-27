using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace ModLocker
{
    public static class ModEvents
    {
        public static void AddWithClickHandler(this ControlCollection ctrls, MouseEventHandler onClickHandler, CheckBox newCheckBox)
        {
            newCheckBox.MouseClick += onClickHandler;
            ctrls.Add(newCheckBox);
        }
        public static void AddWithDragHandler(this ControlCollection ctrls, DragEventHandler onClickHandler, CheckBox newCheckBox)
        {
            newCheckBox.DragOver += onClickHandler;
            ctrls.Add(newCheckBox);
        }
    }
}
