using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Admin
{
    public partial class FormBooks : Form
    {
        public FormBooks()
        {
            InitializeComponent();
        }

        private void книгиBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.книгиBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.libraryDataSet);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormBooks_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "libraryDataSet.Книги". При необходимости она может быть перемещена или удалена.
            this.книгиTableAdapter.Fill(this.libraryDataSet.Книги);

        }

        private static FormBooks f;

        public static FormBooks fb
        {
            get
            {
                if (f == null || f.IsDisposed) f = new FormBooks();
                return f;
            }
        }

        string GetSelectedFieldName()
        {
            return книгиDataGridView.Columns[книгиDataGridView.CurrentCell.ColumnIndex].DataPropertyName;
        }

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxFind.Text == "")
            {
                MessageBox.Show("Вы ничего не задали", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int indexPos;
            try
            {
                indexPos =
                книгиBindingSource.Find(GetSelectedFieldName(),
                toolStripTextBoxFind.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка поиска \n" + err.Message);
                return;
            }
            if (indexPos > -1)
                книгиBindingSource.Position = indexPos;
            else
            {
                MessageBox.Show("Таких книг нет", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                книгиBindingSource.Position = 0;
            }

        }

        private void checkBoxFind_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFind.Checked)
            {
                if (toolStripTextBoxFind.Text == "")
                    MessageBox.Show("Вы ничего не задали", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    try
                    {
                        книгиBindingSource.Filter =
                        GetSelectedFieldName() + "='" + toolStripTextBoxFind.Text + "'";
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Ошибка фильтрации \n" +
                        err.Message);
                    }
            }
            else
                книгиBindingSource.Filter = "";
            if (книгиBindingSource.Count == 0)
            {
                MessageBox.Show("Нет таких");
                книгиBindingSource.Filter = "";
                checkBoxFind.Checked = false;
            }

        }

        private void toolStripButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /*string bookCurrent = "";        
        public string ShowSelectForm(string book)
        {
            toolStripButtonOK.Visible = true;
            bookCurrent = book;
            if (ShowDialog() == DialogResult.OK)
                return
                (string)((DataRowView)книгиBindingSource.Current)["id"];
            else
                return "";
        }*/

        /*private void FormBooks_Shown(object sender, EventArgs e)
        {
            книгиBindingSource.Position = книгиBindingSource.Find("id", bookCurrent);
        }*/

        public void ShowForm()
        {
            Show();
            Activate();
        }
    }
}
