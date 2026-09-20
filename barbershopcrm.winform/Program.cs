namespace BarberShopCRM;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        while (true)
        {
            using (var loginForm = new Forms.LoginForm())
            {
                if (loginForm.ShowDialog() != System.Windows.Forms.DialogResult.OK || loginForm.LoggedInUser == null)
                {
                    break; // User closed or cancelled login window -> exit application
                }

                // Run main desktop application shell as the primary thread window
                var mainForm = new Forms.MainForm(loginForm.LoggedInUser);
                System.Windows.Forms.Application.Run(mainForm);

                if (!mainForm.IsLoggingOut)
                {
                    break; // User exited MainForm -> close application
                }
            }
        }
    }    
}
