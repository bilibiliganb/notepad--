using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 记事本
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //加载程序时隐藏panel
            panel1.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //点击按钮时隐藏panel
            panel1.Visible = false;
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //点击按钮时隐藏panel
            panel1.Visible = false;
        }

        List<string> list = new List<string>();


        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择要打开的文件";
            ofd.InitialDirectory = @"D:\下载";
            ofd.Multiselect = true;
            ofd.Filter = "文本文件|*.txt|所有文件|*.*";
            ofd.ShowDialog();

            //获得用户选中的文件路径
            string path = ofd.FileName;

            //用户选中文件的文件名保存到fileName中
            string fileName = Path.GetFileName(path);
            //将文件名添加到listBox中
            listBox1.Items.Add(fileName);
            if (path == "")
            {
                return;
            }
            //将文件全路径加入到list中
            list.Add(path);
            using (FileStream fsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = fsRead.Read(buffer, 0, buffer.Length);
                textBox1.Text = Encoding.Default.GetString(buffer, 0, r);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"D:\下载";
            sfd.Title = "请选择要保存的文件路径";
            sfd.Filter = "文本文件|*.txt|所有文件|*.*";
            sfd.ShowDialog();
            //获得用户要保存的文件路径
            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }
            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                fsWrite.Write(buffer, 0, buffer.Length);


            }
            MessageBox.Show("保存成功");
        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (自动换行ToolStripMenuItem.Text == "☆自动换行")
            {
                textBox1.WordWrap = true;
                自动换行ToolStripMenuItem.Text = "★取消自动换行";
            }
            else if (自动换行ToolStripMenuItem.Text == "★取消自动换行")
            {
                textBox1.WordWrap = false;
                自动换行ToolStripMenuItem.Text = "☆自动换行";
            }
            else
            {
                自动换行ToolStripMenuItem.Text = "☆自动换行";
            }
        }

        private void 格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //默认自动换行
            自动换行ToolStripMenuItem.Text = "☆自动换行";
        }
        /// <summary>
        /// 双击打开对应文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string path = list[listBox1.SelectedIndex];
            using (FileStream fsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = fsRead.Read(buffer, 0, buffer.Length);
                textBox1.Text = Encoding.Default.GetString(buffer, 0, r);
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowDialog();
            textBox1.Font = fd.Font;
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            textBox1.ForeColor = cd.Color;
        }
    }
}
