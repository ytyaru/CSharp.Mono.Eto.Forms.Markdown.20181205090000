using System;
using System.IO;
using Eto.Forms;
using Eto.Drawing;
using HeyRed.MarkdownSharp;
namespace HelloMarkdown
{
    public partial class MainForm : Form
    {
        RichTextArea richTextAreaMd;
        RichTextArea richTextAreaHtml;
        WebView previewer;
        HeyRed.MarkdownSharp.Markdown mdParser = new Markdown();
        public MainForm()
        {
            Title = "Markdown Previewer";
            ClientSize = new Size(1024, 768);
            Create();
        }
        private void Create()
        {
            richTextAreaMd = new RichTextArea() { Width=320, Height=600 };
            richTextAreaHtml = new RichTextArea() { Width=320, Height=600 };
            previewer = new WebView() { Width=320, Height=600 };

            richTextAreaMd.Focus();
            richTextAreaMd.TextChanged += (object sender, EventArgs e) =>
            {
                richTextAreaHtml.Text = mdParser.Transform((sender as RichTextArea).Text);
                previewer.LoadHtml(richTextAreaHtml.Text);
            };
            richTextAreaMd.Text = new StreamReader(new FileStream("/tmp/work/Projects/HelloMarkdown/HelloMarkdown/HelloMarkdown/DefaultMarkdown.md", FileMode.Open)).ReadToEnd();
            
            Content = new TableLayout
            {
                Padding = 0, // padding around cells
                Spacing = new Size(0, 0), // spacing between each cell
                Rows =
                {
                    new TableRow(richTextAreaMd, richTextAreaHtml, previewer),
                }
            };
        }
    }
}
