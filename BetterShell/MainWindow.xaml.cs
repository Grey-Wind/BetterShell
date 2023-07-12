using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace BetterShell
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                string command = InputTextBox.Text.Trim();
                ExecuteShellCommand(command);
            }
        }

        private async void ExecuteShellCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                // 如果命令为空或只包含空白字符，则不执行任何操作
                return;
            }
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            try
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            OutputTextBox.AppendText(e.Data + Environment.NewLine);
                            OutputTextBox.ScrollToEnd();

                            OutputTextBox.CaretIndex = OutputTextBox.Text.Length;
                            OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
                            OutputTextBox.SelectionLength = 0;
                            OutputTextBox.Focus();
                        });
                    }
                };

                process.Start();

                // 发送命令到标准输入流
                process.StandardInput.WriteLine(command);
                process.StandardInput.WriteLine("exit");
                process.BeginOutputReadLine(); // 开始异步读取标准输出流

                await Task.Run(() => process.WaitForExit()); // 等待命令执行完成

                OutputTextBox.AppendText(Environment.NewLine);

                // 显示当前目录
                OutputTextBox.AppendText(Environment.CurrentDirectory + "> ");

                if (string.IsNullOrWhiteSpace(command))
                {
                    // 如果命令为空或只包含空白字符，则不执行任何操作
                    return;
                }

                // 清除编辑框内容
                InputTextBox.Clear();

                if (command.ToLower() == "powershell" || command.ToLower() == "cmd")
                {
                    // 如果用户输入了 "powershell" 或 "cmd"，则不执行命令，直接返回
                    return;
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText("Error: " + ex.Message + Environment.NewLine);
            }
        }
    }
}
