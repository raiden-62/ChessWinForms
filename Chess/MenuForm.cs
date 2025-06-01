using System.Data;

namespace Chess
{
    public partial class MenuForm : Form
    {
        private string _folderPath;

        private bool isJSON
        {
            get
            {
                return cmbSerialization.SelectedItem.ToString() == "JSON";
            }
        }
        public MenuForm()
        {
            InitializeComponent();

            btnSelectFolder.Click += SelectFolder;
            btnResumeGame.Click += ResumeGame;
            btnNewGame.Click += StartNewGame;

            InitializeSerializationComboBox();
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            string folder = _folderPath != String.Empty ? _folderPath : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var gameForm = new GameForm(_folderPath, isJSON);
            gameForm.Show();
            this.Hide();
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            var gameForm = new GameForm(_folderPath, isJSON, false); //add loading previous game
            gameForm.Show();
            this.Hide();
        }
        // In your form's constructor or Load event
        private void InitializeSerializationComboBox()
        {
            // Add the serialization options
            cmbSerialization.Items.AddRange(new object[] { "JSON", "XML" });

            // Set JSON as default selected item
            cmbSerialization.SelectedItem = "JSON";

            // Make it non-editable (user can only select from list)
            cmbSerialization.DropDownStyle = ComboBoxStyle.DropDownList;

            // Optional: Set a tooltip
            //toolTip1.SetToolTip(comboSerialization, "Select serialization format");
        }
        private void SelectFolder(object sender, EventArgs e)
        {

            var folderDialog = new FolderBrowserDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Description = "Select a folder",
                UseDescriptionForTitle = true // This applies the description to the dialog title
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderDialog.SelectedPath;
                _folderPath = folderDialog.SelectedPath;
            }

        }
    }
}
