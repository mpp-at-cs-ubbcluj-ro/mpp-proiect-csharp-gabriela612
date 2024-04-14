using System.Windows.Forms;

namespace WindowsFormsApp1.utils;

public class MyMessageBox
{
    public static void Show(string message)
    {
        MessageBox.Show(null, message, "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}