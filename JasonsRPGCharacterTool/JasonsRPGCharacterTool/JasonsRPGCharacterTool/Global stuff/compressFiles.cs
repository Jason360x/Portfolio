using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool.Global_stuff
{
    //Zulässige gameModes sind "tes" und "asterius"
    class compressFiles
    {
        string save, saveToZip, yes, no;
        public void compress(string compressFrom, string compressedFilePath)
        {
            if (File.Exists(compressedFilePath))
            {
                File.Delete(compressedFilePath);
            }

            ZipFile.CreateFromDirectory(compressFrom, compressedFilePath);
        }

        public void extract(string extractFrom, string extractedFilePath)
        {
            Directory.Delete(extractedFilePath, true);

            ZipFile.ExtractToDirectory(extractFrom, extractedFilePath);
        }

        void askSave(string gameMode)
        {
            saveToZip = Properties.strings.saveToZip;
            yes = Properties.strings.yes;
            no = Properties.strings.no;
            save = Properties.strings.save;

            string charactersPath = "";

            switch (gameMode)
            {
                case "asterius":
                    charactersPath = Application.StartupPath + "/Asterius";
                    break;
                case "tes":
                    charactersPath = Application.StartupPath + "/TES";
                    break;
                default:
                    break;
            }

            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(saveToZip, save, yesNo);

            if (result == DialogResult.Yes && charactersPath != "")
            {
                saveZip(gameMode, charactersPath);
            }
        }

        public void saveZip(string gameMode, string charactersPath)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "ZIP File|*.zip";
            saveFile.Title = "Save the zip file.";
            saveFile.InitialDirectory = Application.StartupPath;
            saveFile.RestoreDirectory = true;
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                string sfdPath = Path.GetDirectoryName(saveFile.FileName);
                string sfdFile = Path.GetFileName(saveFile.FileName);
                string finalPath = sfdPath + "/" + gameMode + sfdFile;

                compress(charactersPath, finalPath);
            }
        }

        public void openZip(string gameMode)
        {
            askSave(gameMode);

            string charactersPath = "";

            switch (gameMode)
            {
                case "asterius":
                    charactersPath = Application.StartupPath + "/Asterius";
                    break;
                case "tes":
                    charactersPath = Application.StartupPath + "/TES";
                    break;
                default:
                    break;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load the zip file.";
                openFileDialog.Filter = "ZIP File|*.zip";
                openFileDialog.InitialDirectory = Application.StartupPath;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK && charactersPath != "")
                {
                    extract(openFileDialog.FileName, charactersPath);
                }
            }
        }
    }
}
