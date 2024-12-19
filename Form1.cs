using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyToExe
{
    public partial class Form1 : Form
    {
        private const string repoOwner = "eroge69"; // Ganti dengan username GitHub Anda
        private const string repoName = "PyToExe"; // Ganti dengan nama repositori GitHub Anda
        private const string token = "XXX"; // Ganti dengan personal access token GitHub Anda

        private Timer statusUpdateTimer; // Timer untuk polling status
        private string currentRunId; // Simpan ID workflow yang sedang berjalan

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        // Inisialisasi timer untuk polling status setiap detik
        private void InitializeTimer()
        {
            statusUpdateTimer = new Timer();
            statusUpdateTimer.Interval = 5000; // Setiap 5 detik (5000ms)
            statusUpdateTimer.Tick += StatusUpdateTimer_Tick;
        }

        private void PlayCompletionSound()
        {
            // Set the path to your sound file (.wav)
            string soundFilePath = "completion_sound.wav";

            // Use SoundPlayer to play the sound
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFilePath);
            if (btnSound.Checked == true)
            {
                player.Play();
            }
        }
        private string downloadlink = " ";
        private string uploadedFileName;
        // Fungsi untuk mengupload file Python
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
          {
             Filter = "Python Files (*.py)|*.py",
              Title = "Select Python File"
           };

          if (ofd.ShowDialog() == DialogResult.OK)
          {
               string filePath = ofd.FileName;
                uploadedFileName = filePath;
                upload();
          }
        }
        
        private async void  upload()
        {

            string fileName = Path.GetFileName(uploadedFileName);
            string content = Convert.ToBase64String(File.ReadAllBytes(uploadedFileName));
            string branchName = "app"; // Ganti dengan nama branch target

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("User-Agent", "WindowsFormsApp");

                // Sertakan query parameter ?ref=branchName
                string url = $"https://api.github.com/repos/{repoOwner}/{repoName}/contents/{fileName}?ref={branchName}";

                // Pertama, periksa apakah file sudah ada di branch
                var getResponse = await client.GetAsync(url);
                string sha = null;

                if (getResponse.IsSuccessStatusCode)
                {
                    // Jika file ada, ambil sha dari file
                    string getResponseBody = await getResponse.Content.ReadAsStringAsync();
                    dynamic getFileResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(getResponseBody);
                    sha = getFileResponse.sha;
                }

                var body = new
                {
                    message = $"Upload {fileName}",
                    content = content,
                    sha = sha, // Sertakan sha jika file sudah ada
                    branch = branchName // Pastikan file diupload ke branch yang benar
                };

                var response = await client.PutAsync(
                    url,
                    new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    txtNotif.Text = ("File uploaded successfully!");
                    uploadedFileName = Path.GetFileName(uploadedFileName);
                    // Setelah upload berhasil, trigger workflow baru dan ambil run ID
                    await StartBuildWorkflow(client);
                    await Task.Delay(3000);
                    checkstatusea();
                    btnCheckStatus.Enabled = true;
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"Error: {responseBody}";
                    txtNotif.Text = (Environment.NewLine+errorMessage);
                    CopyToClipboard(errorMessage); // Menyalin pesan error ke clipboard
                }
            }
        }
        // Fungsi untuk menyalin teks ke clipboard
        private void CopyToClipboard(string message)
        {
            Clipboard.SetText(message);
            txtNotif.Text = ($"{Environment.NewLine}Error message copied to clipboard!");
        }

        // Fungsi untuk memulai workflow baru setelah upload
        private async Task StartBuildWorkflow(HttpClient client)
        {
            string workflowUrl = $"https://api.github.com/repos/{repoOwner}/{repoName}/actions/workflows/build-exe.yml/dispatches";
            var body = new
            {
                @ref = "app"  // Specify the branch ref (e.g., main, or any other branch you want to use)
            };

            var response = await client.PostAsync(workflowUrl, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                txtStatus.Text = ("Build workflow started!");

                await Task.Delay(3000);

                // Get the latest workflow run ID
                var getResponse = await client.GetAsync($"https://api.github.com/repos/{repoOwner}/{repoName}/actions/runs");
                string responseBody = await getResponse.Content.ReadAsStringAsync();
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                if (jsonResponse.workflow_runs.Count > 0)
                {
                    currentRunId = jsonResponse.workflow_runs[0].id.ToString(); // Save the latest run ID
                }
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                txtNotif.Text = ($"{Environment.NewLine}Error starting build: {responseBody}");
                CopyToClipboard(responseBody); // Copy error message to clipboard
            }
        }

        private void checkstatusea()
        {
            if (string.IsNullOrEmpty(currentRunId))
            {
                txtStatus.Text = ("No active build found.");
                return;
            }

            statusUpdateTimer.Start(); // Mulai polling status
            txtStatus.Text = $"Checking status...{Environment.NewLine}"; // Menampilkan pesan awal
        }

        // Fungsi untuk memeriksa status build terbaru yang dimulai
        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentRunId))
            {
                txtStatus.Text = ("No active build found.");
                return;
            }

            statusUpdateTimer.Start(); // Mulai polling status
            txtStatus.Text = $"Checking status...{Environment.NewLine}"; // Menampilkan pesan awal
        }

        // Fungsi untuk memperbarui status secara berkala
        private async void StatusUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentRunId))
            {
                statusUpdateTimer.Stop();
                txtStatus.Text = ("No active build to check.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("User-Agent", "WindowsFormsApp");

                string url = $"https://api.github.com/repos/{repoOwner}/{repoName}/actions/runs/{currentRunId}";

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                    txtStatus.Clear();
                    txtStatus.AppendText($"ID: {jsonResponse.id}{Environment.NewLine}");
                    txtStatus.AppendText($"Name: {jsonResponse.name}{Environment.NewLine}");
                    txtStatus.AppendText($"Status: {jsonResponse.status}{Environment.NewLine}");
                    txtStatus.AppendText($"Conclusion: {jsonResponse.conclusion}{Environment.NewLine}");

                    // Jika status 'success', hentikan polling
                    if (jsonResponse.status == "completed" && jsonResponse.conclusion == "success")
                    {
                        statusUpdateTimer.Stop(); // Berhenti polling
                        txtNotif.Text = ($"{Environment.NewLine}Build completed successfully. You can now download the artifact.");
                        btnDownload.Enabled = true;
                        PlayCompletionSound();
                        delete.Visible = true;
                    }
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    txtNotif.Text = ($"{Environment.NewLine} Error: {responseBody}");
                    CopyToClipboard(responseBody); // Menyalin pesan error ke clipboard
                    PlayCompletionSound();
                    delete.Visible = true;
                }              
            }
            delete.Visible = true;
        }

        // Fungsi untuk mengunduh artifact
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    client.DefaultRequestHeaders.Add("User-Agent", "WindowsFormsApp");

                    string artifactsUrl = $"https://api.github.com/repos/{repoOwner}/{repoName}/actions/artifacts";

                    var response = await client.GetAsync(artifactsUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

                        if (jsonResponse.total_count > 0)
                        {
                            string downloadUrl = jsonResponse.artifacts[0].archive_download_url;
                            downloadlink = downloadUrl;
                            btncopydownload.Visible = true;


                            // Mendapatkan stream download tanpa seek
                            var downloadResponse = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);

                            if (downloadResponse.IsSuccessStatusCode)
                            {
                                using (var artifactStream = await downloadResponse.Content.ReadAsStreamAsync())
                                {
                                    // Gunakan variable untuk mendeteksi jumlah byte yang telah diunduh
                                    long totalBytesDownloaded = 0;
                                    byte[] buffer = new byte[8192]; // 8 KB buffer

                                    using (var fileStream = new FileStream("ExeFile.zip", FileMode.Create))
                                    {
                                        int bytesRead;
                                        DateTime startTime = DateTime.Now; // Waktu mulai untuk estimasi kecepatan unduhan

                                        while ((bytesRead = await artifactStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                        {
                                            // Tulis ke file stream
                                            await fileStream.WriteAsync(buffer, 0, bytesRead);

                                            // Perbarui jumlah byte yang telah diunduh
                                            totalBytesDownloaded += bytesRead;

                                            // Estimasi waktu dan update status di TextBox
                                            var elapsedTime = DateTime.Now - startTime;
                                            var speed = totalBytesDownloaded / elapsedTime.TotalSeconds / 1024; // Kecepatan dalam KB/s

                                            txtStatus.Clear();
                                            txtStatus.AppendText($"{downloadUrl}{Environment.NewLine}{Environment.NewLine}");
                                            txtStatus.AppendText($"Downloading... {totalBytesDownloaded / 1024} KB downloaded{Environment.NewLine}");
                                            txtStatus.AppendText($"Speed: {speed:F2} KB/s{Environment.NewLine}");

                                            // Optional: Tunda sebentar untuk memberi kesempatan UI untuk merender
                                            await Task.Delay(200);
                                        }
                                    }
                                }
                                PlayCompletionSound();
                                txtNotif.Text = ($"{Environment.NewLine}Artifact downloaded successfully!");
                                
                            }
                            else
                            {
                                PlayCompletionSound();
                                txtNotif.Text = ($"{Environment.NewLine}Error downloading artifact: {downloadResponse.StatusCode}");
                                CopyToClipboard($"Error downloading artifact: {downloadResponse.StatusCode}");
                            }
                        }
                        else
                        {
                            txtNotif.Text = ($"{Environment.NewLine}No artifacts found.");
                        }
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        txtNotif.Text = ($"{Environment.NewLine} Error: {responseBody}");
                        CopyToClipboard(responseBody); // Menyalin pesan error ke clipboard
                    }
                }
            }
            catch (Exception ex)
            {
                // Menangkap exception dan menampilkan pesan error yang lebih rinci
                txtNotif.Text = ($"{Environment.NewLine}An error occurred: {ex.Message}\n{ex.StackTrace}");
                CopyToClipboard($"An error occurred: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSound.Checked = Properties.Settings.Default.SOUND;
            btnTop.Checked = Properties.Settings.Default.TOP;
            this.TopMost = btnTop.Checked;
        }

        private void btnSound_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SOUND = btnSound.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = btnTop.Checked;
            Properties.Settings.Default.TOP = btnTop.Checked;
            Properties.Settings.Default.Save();
        }

        // Fungsi untuk menghapus file Python
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            // Dapatkan nama file yang telah diupload sebelumnya
            string fileName = uploadedFileName; // Fungsi ini harus mengembalikan nama file yang diupload sebelumnya
            string branchName = "app";

            if (string.IsNullOrEmpty(fileName))
            {
                txtStatus.Text = ("No file uploaded yet.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("User-Agent", "WindowsFormsApp");

                string url = $"https://api.github.com/repos/{repoOwner}/{repoName}/contents/{fileName}?ref={branchName}";

                // Pertama, ambil informasi file yang akan dihapus (termasuk sha)
                var getResponse = await client.GetAsync(url);
                if (getResponse.IsSuccessStatusCode)
                {
                    string getResponseBody = await getResponse.Content.ReadAsStringAsync();
                    dynamic getFileResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(getResponseBody);
                    string sha = getFileResponse.sha;

                    // Membuat body JSON untuk penghapusan file
                    var body = new
                    {
                        message = $"Delete {fileName}",
                        sha = sha,        
                        branch = branchName
                    };

                    // Kirim permintaan DELETE dengan body JSON
                    var deleteResponse = await client.SendAsync(new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url),
                        Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                    });

                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        txtNotif.Text = ($"{Environment.NewLine}File deleted successfully!");
                    }
                    else
                    {
                        string responseBody = await deleteResponse.Content.ReadAsStringAsync();
                        string errorMessage = $"Error: {responseBody}";
                        txtNotif.Text = (Environment.NewLine + errorMessage);
                        CopyToClipboard(errorMessage); // Menyalin pesan error ke clipboard
                    }
                }
                else
                {
                    string responseBody = await getResponse.Content.ReadAsStringAsync();
                    txtNotif.Text = ($"{Environment.NewLine}Error: {responseBody}");
                    CopyToClipboard(responseBody); // Menyalin pesan error ke clipboard
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Ambil array file path yang di-drop
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Periksa apakah file pertama memiliki ekstensi PY
                string fileExtension = Path.GetExtension(files[0]).ToLower();

                if (fileExtension == ".py")
                {
                    e.Effect = DragDropEffects.Copy; // Tampilkan efek copy jika ekstensi valid
                }
                else
                {
                    e.Effect = DragDropEffects.None; // Tidak bisa drop jika ekstensi tidak valid
                }
            }
            else
            {
                e.Effect = DragDropEffects.None; // Tidak bisa drop
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // Jika ada file yang di-drop
            if (files.Length > 0)
            {
                uploadedFileName = files[0]; // Ambil file pertama
                upload();


            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            try
            {
                // Mendapatkan lokasi file eksekusi program
                string programLocation = Path.GetDirectoryName(Application.ExecutablePath);

                // Membuka lokasi menggunakan File Explorer
                if (!string.IsNullOrEmpty(programLocation))
                {
                    Process.Start("explorer.exe", programLocation);
                }
                else
                {
                    MessageBox.Show("Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Menampilkan pesan error jika terjadi kesalahan
                MessageBox.Show($"Something wrong : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btncopydownload_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(downloadlink);
            btncopydownload.Text = "Copied to your Clipboard";
            await Task.Delay(3000);
            btncopydownload.Text = "Copy Download Link";

        }

        private async void txtNotif_TextChanged(object sender, EventArgs e)
        {
            await Task.Delay(10000);
            txtNotif.Text = string.Empty;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://github.com/eroge69/PytoexeApp";
            try
            {
                // Membuka URL di browser default
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Menyuruh sistem untuk membuka URL dengan browser
                });
            }
            catch (Exception ex)
            {
                txtNotif.Text=($"An error occurred: {ex.Message}");
            }
        }
    }
}
