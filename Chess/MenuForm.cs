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

            cmbSerialization.Items.AddRange(new object[] { "JSON", "XML" });
            cmbSerialization.SelectedItem = "JSON";
            cmbSerialization.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSerialization.SelectedIndexChanged += ChangeFormat;
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
            var gameForm = new GameForm(_filePath, isJSON, false);
            gameForm.Show();
            this.Hide();
        }

        private void SelectFolder(object sender, EventArgs e)
        {

            var folderDialog = new FolderBrowserDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Description = "Выберите папку для сохранения",
                UseDescriptionForTitle = true //uses this^ for title
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                _folderPath = folderDialog.SelectedPath;
            }

        }
        private void SelectFile(object sender, EventArgs e)
        {
            string title = "Выберите файл сохраненной игры";
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
            if (!Serializer.IsFileValid(_filePath, isJSON)) MessageBox.Show("Выбран некорректный файл");
        }

        private void ChangeFormat(object sender, EventArgs e)
        {

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
