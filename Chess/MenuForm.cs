using Model;
using System.Data;
using System.Windows.Forms;

namespace Chess
{
    public partial class MenuForm : Form
    {
        private string _folderPath;
        private string _filePath;
        private bool isJSON => cmbSerialization.SelectedItem.ToString() == "JSON";
            
        
        public MenuForm()
        {
            InitializeComponent();

            btnResumeGame.Click += ResumeGame;
            btnNewGame.Click += SelectFolder;
            btnNewGame.Click += StartNewGame;
            btnSelectFile.Click += SelectFile;

            cmbSerialization.SelectedIndexChanged += ChangeFormat;

            InitializeSerializationComboBox();

            
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            string folder = _folderPath;
            var gameForm = new GameForm(_folderPath, isJSON);
            gameForm.Show();
            this.Hide();
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            var gameForm = new GameForm(_filePath, isJSON, false); //add loading previous game
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
                UseDescriptionForTitle = true //uses this^ for title
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                _folderPath = folderDialog.SelectedPath;
            }

        }
        private void SelectFile(object sender, EventArgs e)
        {
            string title = "Select a File";
            string filter = "All Files|*.*"; 
            string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.Filter = filter;
                openFileDialog.Multiselect = false; // Force single-file selection

                
                // Show dialog and return the selected file path
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = openFileDialog.FileName;
                    txtFolderPath.Text = openFileDialog.FileName;

                }
            }


            btnResumeGame.Enabled = Serializer.IsFileValid(_filePath, isJSON);

        }

        private void ChangeFormat(object sender, EventArgs e)
        {
            //btnResumeGame.Enabled = Serializer.IsFileValid(_filePath, isJSON); //SHOULDNT ACTUALLY BE HERE

            if (!Serializer.IsFileValid(_filePath, !isJSON) || _filePath == null) return;

            var json = new SerializerJSON(Path.GetDirectoryName(_filePath));
            var xml = new SerializerXML(Path.GetDirectoryName(_filePath));

            if (isJSON) //xml->json
            {
                json.Serialize(xml.Deserialize());

            }
            else //json->xml
            {
                xml.Serialize(json.Deserialize());
            }
        }
    }
}
