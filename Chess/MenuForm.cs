using Model;
using System.Data;
using System.Windows.Forms;

namespace Chess
{
    public partial class MenuForm : Form
    {
        private string _folderPath;
        private string _filePath;
        private bool isJSON => cmbSerialization.SelectedItem.ToString() == "JSON"; //�������� ��� ������� � �����-�����

        
        private delegate void InitializeGame(string path, bool isJson, bool newGame); //������� ��� ������������� ����
        private InitializeGame _gameInitializer;

        public MenuForm()
        {
            InitializeComponent();

            //�������������� �������
            _gameInitializer = (path, isJson, newGame) =>
            {
                var gameForm = new GameForm(path, isJson, newGame);
                gameForm.Show();
                this.Hide();
            };

            btnResumeGame.Click += ResumeGame; //����������� ����
            btnNewGame.Click += SelectFolder; //����� �����
            btnNewGame.Click += StartNewGame; //������ ����� ���� (����� ������ �����)
            btnSelectFile.Click += SelectFile; //����� ����� ����������

            cmbSerialization.Items.AddRange(new object[] { "JSON", "XML" }); //��������� �������� � �����-����
            cmbSerialization.SelectedItem = "JSON"; //������� ������� ������ �� ��������� = JSON
            cmbSerialization.DropDownStyle = ComboBoxStyle.DropDownList; //����� = ���������� ������ � ������������� ����������
            cmbSerialization.SelectedIndexChanged += ChangeFormat; //����� ���������� ��� ��������� ���������� ��������
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            _gameInitializer(_folderPath, isJSON, true); //true = ����� ����
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            _gameInitializer(_filePath, isJSON, false); //false = ����������� ����
        }

        private void SelectFolder(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog() //���������� ���� ��� ������ �����
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop), //���������� ��������� ������� ���� � ����������
                Description = "�������� ����� ��� ����������",
                UseDescriptionForTitle = true
                //Description ������������ ����� ��� ������� ������ �����
                //Title - �������� ���� (����� ������)
                //UseDescriptionForTitle = true ������� Description ����� � ���������� ���� ����� ��� Title (����� ������)
                //��� false Title ����� "�������� �����"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK) //���� ������������ ����� "�������"
            {
                _folderPath = folderDialog.SelectedPath; //��������� ��������� ����
            }
        }

        private void SelectFile(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "�������� ���� ����������� ����"; //Title ����� ������� �����
                openFileDialog.Filter = "All Files|*.*"; //����� ����� ����� ���� �������
                openFileDialog.Multiselect = false; // ������ ������� ��������� ������


               
                if (openFileDialog.ShowDialog() == DialogResult.OK) //���� ������������ ����� "�������"
                {
                    _filePath = openFileDialog.FileName; //��������� ��������� ����
                    txtFolderPath.Text = openFileDialog.FileName; //���������� ��������� ����
                }
            }
            //���� ���� ���������� �� ������ "���������� ����" ������������, ����� �������������� 
            btnResumeGame.Enabled = Serializer.IsFileValid(_filePath, isJSON);
            if (!Serializer.IsFileValid(_filePath, isJSON)) MessageBox.Show("������ ������������ ����"); //������� ��������� ���� ���� ������������
        }

        private void ChangeFormat(object sender, EventArgs e)
        {
            //����������� �� ������ ������� � ������ �� ����� ��������� ���� ���� �� ������, ���� ������������ (������ �����������)
            if (!Serializer.IsFileValid(_filePath, !isJSON) || _filePath == null) return; 

            var json = new SerializerJSON(Path.GetDirectoryName(_filePath));
            var xml = new SerializerXML(Path.GetDirectoryName(_filePath));

            if (isJSON) //����������� XML->JSON
            {
                json.Serialize(xml.Deserialize());
            }
            else //����������� JSON->XML
            {
                xml.Serialize(json.Deserialize());
            }
        }
    }
}