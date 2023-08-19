using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AgendaPessoal
{
    public partial class Form1 : Form
    {
        public Random random;

        public Form1()
        {
            InitializeComponent();

            random = new Random();
            cmbAssunto.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDataHora.Text = DateTime.Now.ToLongDateString() +" - "+ DateTime.Now.ToLongTimeString();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id =int.Parse(txtId.Text);

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    lblId.ForeColor = Color.Red;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = BDagenda.GetID(id);
                    DataRow row = dt.Rows[0];

                    if (row["ID"].ToString() == txtId.Text)
                    {
                        DialogResult result = MessageBox.Show("Deseja exculir ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            lblId.ForeColor = Color.FromArgb(64,64,64);
                            BDagenda.DeleteId(id);

                            txtId.Clear();
                            txtHorario.Clear();
                            txtTitulo.Clear();
                            txtObs.Clear();
                            cmbAssunto.SelectedIndex = -1;

                            int numeroAleatorio = random.Next(1, 10001);
                            txtId.Text = numeroAleatorio.ToString();

                            EXIBIRDADOS();

                            MessageBox.Show("Registro deletado com sucesso !");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não há registro ativo para esse ID");
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: "+EX.Message);
            }
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(dtpData.Text))
                {
                    lblId.ForeColor = Color.Red;
                    lblData.ForeColor = Color.Red;
                }
                else
                {
                    lblId.ForeColor = Color.FromArgb(64, 64, 64);
                    lblData.ForeColor = Color.FromArgb(64, 64, 64);

                    GSagenda gSagenda = new GSagenda();
                    gSagenda.ID = int.Parse(txtId.Text);
                    gSagenda.DATA = dtpData.Text;
                    gSagenda.HORARIO = txtHorario.Text;
                    gSagenda.ASSUNTO = cmbAssunto.Text;
                    gSagenda.TITULO = txtTitulo.Text;
                    gSagenda.OBS = txtObs.Text;

                    BDagenda.Add(gSagenda);

                    txtId.Clear();
                    txtHorario.Clear();
                    txtTitulo.Clear();
                    txtObs.Clear();
                    cmbAssunto.SelectedIndex = -1;

                    int numeroAleatorio = random.Next(1, 10001);
                    txtId.Text = numeroAleatorio.ToString();

                    EXIBIRDADOS();

                    MessageBox.Show("Entrada efetuada com sucesso !");
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: "+EX.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    lblId.ForeColor = Color.Red;   
                }
                else
                {
                    lblId.ForeColor = Color.FromArgb(64, 64, 64);

                    DataTable dt = new DataTable();
                    int id = int.Parse(txtId.Text);
                    dt = BDagenda.GetID(id);

                    dgvAgenda.DataSource = dt;
                    dgvAgenda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    dgvAgenda.DefaultCellStyle.ForeColor = Color.Black;
                    dgvAgenda.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                    dgvAgenda.DefaultCellStyle.BackColor = Color.Silver;
                    dgvAgenda.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                    dgvAgenda.EnableHeadersVisualStyles = false;
                    dgvAgenda.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                    dgvAgenda.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dgvAgenda.GridColor = Color.Black;
                    dgvAgenda.AutoResizeColumns();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: "+EX.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dtpData.Text))
                {
                    lblData.ForeColor = Color.Red;
                }
                else
                {
                    lblData.ForeColor = Color.FromArgb(64, 64, 64);

                    DataTable dt = new DataTable();
                    string data = txtId.Text;
                    dt = BDagenda.GetData(data);

                    dgvAgenda.DataSource = dt;
                    dgvAgenda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    dgvAgenda.DefaultCellStyle.ForeColor = Color.Black;
                    dgvAgenda.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                    dgvAgenda.DefaultCellStyle.BackColor = Color.Silver;
                    dgvAgenda.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                    dgvAgenda.EnableHeadersVisualStyles = false;
                    dgvAgenda.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                    dgvAgenda.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dgvAgenda.GridColor = Color.Black;
                    dgvAgenda.AutoResizeColumns();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: " + EX.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblData.ForeColor = Color.FromArgb(64, 64, 64);
            lblId.ForeColor = Color.FromArgb(64, 64, 64);

            txtId.Clear();
            txtHorario.Clear();
            txtTitulo.Clear();
            txtObs.Clear();
            cmbAssunto.SelectedIndex = -1;

            int numeroAleatorio = random.Next(1, 10001);
            txtId.Text = numeroAleatorio.ToString();

            EXIBIRDADOS();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    lblId.ForeColor = Color.Red;
                }
                else
                {
                    lblId.ForeColor = Color.FromArgb(64, 64, 64);

                    DialogResult result = MessageBox.Show("Deseja editar o registro ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        GSagenda gSagenda = new GSagenda();
                        gSagenda.ID = int.Parse(txtId.Text);
                        gSagenda.DATA = dtpData.Text;
                        gSagenda.HORARIO = txtHorario.Text;
                        gSagenda.ASSUNTO = cmbAssunto.Text;
                        gSagenda.TITULO = txtTitulo.Text;
                        gSagenda.OBS = txtObs.Text;

                        BDagenda.Update(gSagenda);
                        EXIBIRDADOS();

                        txtId.Clear();
                        txtHorario.Clear();
                        txtTitulo.Clear();
                        txtObs.Clear();
                        cmbAssunto.SelectedIndex = -1;

                        int numeroAleatorio = random.Next(1, 10001);
                        txtId.Text = numeroAleatorio.ToString();

                        MessageBox.Show("Registro editado com sucesso !");
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: "+EX.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BDagenda.CriarBancoSQLite();
            BDagenda.CriarTabelaSQLite();

            int numeroAleatorio = random.Next(1, 10001);
            txtId.Text = numeroAleatorio.ToString();

            EXIBIRDADOS();
        }

        private void EXIBIRDADOS()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = BDagenda.GetContatos();

                dgvAgenda.DataSource = dt;
                dgvAgenda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvAgenda.DefaultCellStyle.ForeColor = Color.Black;
                dgvAgenda.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                dgvAgenda.DefaultCellStyle.BackColor = Color.Silver;
                dgvAgenda.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                dgvAgenda.EnableHeadersVisualStyles = false;
                dgvAgenda.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                dgvAgenda.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                dgvAgenda.GridColor = Color.Black;
                dgvAgenda.AutoResizeColumns();

            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: " + EX.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja sair ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                Application.Exit();            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("calc.exe");
            }
            catch (Exception EX)
            {

                MessageBox.Show("ERRO: "+EX.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("notepad.exe");
            }
            catch (Exception EX)
            {
                MessageBox.Show("ERRO: "+EX.Message);
            }
        }

        private void txtHorario_TextChanged(object sender, EventArgs e)
        {
            txtHorario.TextChanged -= txtHorario_TextChanged;

            string numeroTexto = new string(txtHorario.Text.Where(char.IsDigit).ToArray());

            if (numeroTexto.Length > 4)
            {
                numeroTexto = numeroTexto.Substring(0, 4);
            }

            string numeroFormatado = "";
            for (int i = 0; i < numeroTexto.Length; i++)
            {
                numeroFormatado += numeroTexto[i];
                if ((i + 1) % 2 == 0 && i != numeroTexto.Length - 1)
                {
                    numeroFormatado += ":";
                }
            }

            txtHorario.Text = numeroFormatado;

            txtHorario.SelectionStart = txtHorario.Text.Length;

            txtHorario.TextChanged += txtHorario_TextChanged;
        }
    }
}
