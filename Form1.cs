namespace sphw1
{
    public partial class Form1 : Form
    {
        private Thread copyThread;

        public Form1()
        {
            InitializeComponent();
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "TXT files|*.txt" })
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = fileDialog.FileName;
                    if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        button3.Enabled = true;
                    }    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "TXT files|*.txt" })
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = fileDialog.FileName;
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        button3.Enabled = true;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            if (!(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text)))
            {
                copyThread = new Thread(Copy);
                copyThread.Start();
            }
        }

        private void Copy()
        {
            int fileLength = (int)new FileInfo(textBox1.Text).Length;
            string text = File.ReadAllText(textBox1.Text);
            int copiedCount = 0;
            progressBar1.Value = copiedCount;
            progressBar1.Maximum = fileLength;
            while (copiedCount < fileLength)
            {
                File.AppendAllText(textBox2.Text, text[copiedCount].ToString());
                copiedCount++;
                progressBar1.Value = copiedCount;
                Thread.Sleep(100);
            }
            button3.Enabled = true;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (copyThread != null && copyThread.IsAlive)
            {
                copyThread.Abort();
            }
        }
    }
}
