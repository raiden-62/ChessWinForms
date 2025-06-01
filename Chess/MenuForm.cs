using System.Data;

namespace Chess
{
    public partial class MenuForm : Form
    {
        private string _folderPath;

        private string SelectedExtension
        {
            get
            {
                return cmbSerialization.SelectedItem.ToString() == "XML" ? ".xml" : ".json";
            }
        }
        public MenuForm()
        {
            InitializeComponent();

            btnSelectFolder.Click += SelectFolder;
            InitializeSerializationComboBox();
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            var gameForm = new GameForm(true, _folderPath, SelectedExtension);
            gameForm.Show();
            this.Hide();
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            var gameForm = new GameForm(false, _folderPath, SelectedExtension); //add loading previous game
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
