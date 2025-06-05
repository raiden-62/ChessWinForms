using Model;
using System.Data;
using System.Windows.Forms;

namespace Chess
{
    public partial class MenuForm : Form
    {
        private string _folderPath;
        private string _filePath;
        private bool isJSON => cmbSerialization.SelectedItem.ToString() == "JSON"; //Проверят что выбрано в комбо-боксе

        
        private delegate void InitializeGame(string path, bool isJson, bool newGame); //делегат для инициализации игры
        private InitializeGame _gameInitializer;

        public MenuForm()
        {
            InitializeComponent();

            //Инициализируем делегат
            _gameInitializer = (path, isJson, newGame) =>
            {
                var gameForm = new GameForm(path, isJson, newGame);
                gameForm.Show();
                this.Hide();
            };

            btnResumeGame.Click += ResumeGame; //Продолжение игры
            btnNewGame.Click += SelectFolder; //Выбор папки
            btnNewGame.Click += StartNewGame; //Начало новой игры (после выбора папки)
            btnSelectFile.Click += SelectFile; //Выбор файла сохранения

            cmbSerialization.Items.AddRange(new object[] { "JSON", "XML" }); //Добавляем элементы в комбо-бокс
            cmbSerialization.SelectedItem = "JSON"; //Элемент который выбран по умолчанию = JSON
            cmbSerialization.DropDownStyle = ComboBoxStyle.DropDownList; //Стиль = выпадающий список с неизменяемыми элементами
            cmbSerialization.SelectedIndexChanged += ChangeFormat; //Метод вызываемый при изменении выбранного элемента
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            _gameInitializer(_folderPath, isJSON, true); //true = новая игра
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            _gameInitializer(_filePath, isJSON, false); //false = продолжение игры
        }

        private void SelectFolder(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog() //Диалоговое окно для выбора папки
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop), //Изначально открывает рабочий стол в проводнике
                Description = "Выберите папку для сохранения",
                UseDescriptionForTitle = true
                //Description показывается снизу над строкой выбора папки
                //Title - название окна (слева сверху)
                //UseDescriptionForTitle = true убирает Description снизу и показывает этот текст как Title (слева сверху)
                //При false Title будет "Выберите папку"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK) //Если пользователь нажал "принять"
            {
                _folderPath = folderDialog.SelectedPath; //Сохраняем выбранный путь
            }
        }

        private void SelectFile(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите файл сохраненной игры"; //Title можно указать сразу
                openFileDialog.Filter = "All Files|*.*"; //Любые файлы могут быть выбраны
                openFileDialog.Multiselect = false; // Нельзя выбрать несколько файлов


               
                if (openFileDialog.ShowDialog() == DialogResult.OK) //Если пользователь нажал "принять"
                {
                    _filePath = openFileDialog.FileName; //Сохраняем выбранный путь
                    txtFolderPath.Text = openFileDialog.FileName; //Показываем выбранный путь
                }
            }
            //Если файл корректный то кнопка "Продолжить игру" активируется, иначе деактивируется 
            btnResumeGame.Enabled = Serializer.IsFileValid(_filePath, isJSON);
            if (!Serializer.IsFileValid(_filePath, isJSON)) MessageBox.Show("Выбран некорректный файл"); //Выводим сообщение если файл некорректный
        }

        private void ChangeFormat(object sender, EventArgs e)
        {
            //Копирование из одного формата в другой не нужно проводить если файл не выбран, файл некорректный (нельзя скопировать)
            if (!Serializer.IsFileValid(_filePath, !isJSON) || _filePath == null) return; 

            var json = new SerializerJSON(Path.GetDirectoryName(_filePath));
            var xml = new SerializerXML(Path.GetDirectoryName(_filePath));

            if (isJSON) //Копирование XML->JSON
            {
                json.Serialize(xml.Deserialize());
            }
            else //Копирование JSON->XML
            {
                xml.Serialize(json.Deserialize());
            }
        }
    }
}