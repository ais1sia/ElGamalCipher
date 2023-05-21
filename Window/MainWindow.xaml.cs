using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using ElGamalCipher;

namespace Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        private string key1;
        private string key2;
        private string key3;
        private string key4;
        KeyPair x = KeyGenerator.GenerateKeys(1024);

        private BigInteger[]? encryptedBuffer;
        private byte[]? decryptedBuffer;
        private bool onReadFile = false;

        public MainWindow()
        {
            InitializeComponent();
            key1 = new string("3733e744192fa9d02e3d602491252af73d64644a" +
                              "c177ab61b7ec835bd786003e348f59c4970ee139b129a8a29a7230b27a0844839789339e623f23c312" +
                              "c7977f32986ae6259fdc4d18498823d9ef1e5ac9287c616b081a2f12228" +
                              "379be3dd32752e6e412683d06aba422a67856fbd74ee3ad1d70cc706d825ca3df87c652dc53");
            GeneratedKey1_TextArea.Text = key1;
            key2 = new string("155d4c348dd96966766cc77371d65268cba75cd2c68e" +
                              "fd212e4dc224e7eae01957e44859bc3ab15dac28496dc063ab0" +
                              "d7206fcf2bd5162b43f95433604666d05edd5205552eccf6d3b0" +
                              "385ac684de7e39f6b9028206c485ebd4e76d5af8273e607a9ae3168585fc9c9" +
                              "e55aaf69bae0d8d202f83acc2d9bdc5c9c158c74874280");
            GeneratedKey2_TextArea.Text = key2;
            key3 = new string("076662ac03ff1947cc39ef9473afc27ee" +
                              "a4807fdcd9c99ba26f33efac9f193ff4ecb70dbfe1" +
                              "0b9f9cd8b37ef3741849bf604bab0f44b31118b6ffc5e" +
                              "b32b900757f8c22e305fe3915113e5ea75358e00f4c048cb3de9258d6e4b1" +
                              "924541bef20f1ec12f93477134dbcb16a7ddb050869acfc61aba9c3ca851244c83f50cbfbb1");
            GeneratedKey3_TextArea.Text = key3;
            key4 = new string("32b7db4e91869c546bdf9370a20a857d131fbaf87a3cdaaccfc3" +
                              "354d79cad246938b6b51fdfd6e319206d9507826a5b4ae4cb6b6cdd" +
                              "e674e6d6b925e7526fa248cda96d4f613c9f2441109382e8501865944d63c0580ad7e52537" +
                              "5b9191a241b2c3e703908f0b6d3777c66e9d8e3b27278188046c40b2e3a719d70065742e8c5");
            GeneratedKey4_TextArea.Text = key4;
            
        }

        private void GenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            x = KeyGenerator.GenerateKeys(1024);
            key1 = Converter.BigIntegerToHex(x.P);
            key2 = Converter.BigIntegerToHex(x.G);
            key3 = Converter.BigIntegerToHex(x.Y);
            key4 = Converter.BigIntegerToHex(x.X);
        }

        private void CheckKeys_Click(object sender, RoutedEventArgs e)
        {
            string s1 = GeneratedKey1_TextArea.Text;
            string s2 = GeneratedKey2_TextArea.Text;
            string s3 = GeneratedKey3_TextArea.Text;
            string s4 = GeneratedKey4_TextArea.Text;

            if (s1.Length == 1024 && System.Text.RegularExpressions.Regex.IsMatch(s1, "^[0-9A-Fa-f]+$") &&
                s2.Length == 1024 && System.Text.RegularExpressions.Regex.IsMatch(s2, "^[0-9A-Fa-f]+$") &&
                s3.Length == 1024 && System.Text.RegularExpressions.Regex.IsMatch(s3, "^[0-9A-Fa-f]+$") &&
                s4.Length == 1024 && System.Text.RegularExpressions.Regex.IsMatch(s4, "^[0-9A-Fa-f]+$"))
            {
                key1 = s1;
                key2 = s2;
                key3 = s3;
                key4 = s4;
                MessageBox.Show("Keys are OK!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (s1.Length != 1024 || s2.Length != 1024 || s3.Length != 1024 || s4.Length != 1024)
            {
                MessageBox.Show("The every key length must be 1024!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("The every key must be in hexadecimal format!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GeneratedKey1_TextChanged(object sender, TextChangedEventArgs e)
        {
            key1 = GeneratedKey1_TextArea.Text.Length == 0 ? null : GeneratedKey1_TextArea.Text;
        }

        private void GeneratedKey2_TextChanged(object sender, TextChangedEventArgs e)
        {
            key2 = GeneratedKey2_TextArea.Text.Length == 0 ? null : GeneratedKey2_TextArea.Text;

        }

        private void GeneratedKey3_TextChanged(object sender, TextChangedEventArgs e)
        {
            key3 = GeneratedKey3_TextArea.Text.Length == 0 ? null : GeneratedKey3_TextArea.Text;

        }
        
        private void GeneratedKey4_TextChanged(object sender, TextChangedEventArgs e)
        {
            key3 = GeneratedKey4_TextArea.Text.Length == 0 ? null : GeneratedKey4_TextArea.Text;

        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            encryptedBuffer = ElGamalAlgorithm.Encrypt(decryptedBuffer, x);
            onReadFile = true;
            Encrypted_TextArea.Text = Converter.BigIntegerArrayToHexString(encryptedBuffer);

        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            decryptedBuffer = ElGamalAlgorithm.Decrypt(encryptedBuffer, x);
            onReadFile = true;
            Decrypted_TextArea.Text = Encoding.UTF8.GetString(decryptedBuffer);

        }


        private void Encrypted_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!onReadFile)
                encryptedBuffer = Encrypted_TextArea.Text.Length == 0 ? null : Converter.ByteArrayToBigIntegerArray(Converter.StringToByteArray(Encrypted_TextArea.Text));
            else
                onReadFile = false;
        }

        private void Decrypted_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!onReadFile)
                decryptedBuffer = Decrypted_TextArea.Text.Length == 0 ? null : Encoding.UTF8.GetBytes(Decrypted_TextArea.Text);
            else
                onReadFile = false;
        }

        


        private void OpenDecryptedFile_Click(object sender, RoutedEventArgs e)
        {
            // specifies metadata for explorer window
            OpenFileDialog openFileDialog = new()
            {
                Filter = "All files (*.*)|*.*",
                Title = "Read File"
            };

            // open explorer window for user
            if (openFileDialog.ShowDialog() == true)
            {
                // trying to read data from file
                try
                {
                    onReadFile = true;
                    decryptedBuffer = File.ReadAllBytes(openFileDialog.FileName);
                    Decrypted_TextArea.Text = Encoding.UTF8.GetString(decryptedBuffer);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ah sh, here we go again!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void OpenEncryptedFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "ElGamal Cypher files files (*.*)|*.*",
                Title = "Read ElGamal Cypher"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    onReadFile = true;
                    encryptedBuffer = Converter.ByteArrayToBigIntegerArray(File.ReadAllBytes(openFileDialog.FileName));
                    Encrypted_TextArea.Text = Converter.BigIntegerArrayToHexString(encryptedBuffer);
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void SaveDecryptedFile_Click(object sender, RoutedEventArgs e)
        {
            
            if (decryptedBuffer == null)
            {
                MessageBox.Show("TextBox is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|PDF documents (*.pdf)|*.pdf",
                Title = "Save File As"
            };

            // showing user explorer window
            if (saveFileDialog.ShowDialog() == true)
            {
                // trying to save text to file
                try
                {
                    // saving text in specified file
                    File.WriteAllBytes(saveFileDialog.FileName, decryptedBuffer);
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Error!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveEncryptedFile_Click(object sender, RoutedEventArgs e)
        {
            if (encryptedBuffer == null)
            {
                MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // specifies metadata for explorer window
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "ElGamal Cypher files All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Save ElGamal Cypher As"
            };

            // showing user explorer window
            if (saveFileDialog.ShowDialog() == true)
            {
                // trying to save cypher to file
                try
                {
                    // saving cypher in specified file
                    File.WriteAllBytes(saveFileDialog.FileName, Converter.BigIntegerArrayToByteArray(encryptedBuffer));
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
           

        }

       
    }
}