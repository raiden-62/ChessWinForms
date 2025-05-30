namespace Chess
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            var gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            var gameForm = new GameForm(false); //add loading previous game
            gameForm.Show();
            this.Hide();
        }
    }
}
