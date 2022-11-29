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

namespace AgendaWPFeEntityFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string operacao;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            contato contato = new contato();
            contato.nome = txtNome.Text;
            contato.email = txtEmail.Text;
            contato.telefone = txtTelefone.Text;

            if(operacao == "inserir")
            {
                using (agendaEntities ctx = new agendaEntities())
                {
                    ctx.contatos.Add(contato);
                    ctx.SaveChanges();
                }
            }

            listarContatos();
            alterarBotoes(1);
            limparCampos();

        }

        private void btInserir_Click(object sender, RoutedEventArgs e)
        {
            operacao = "inserir";
            alterarBotoes(2);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listarContatos();
            alterarBotoes(1);
            txtID.Text = "";
            txtID.IsEnabled = false;
        }

        private void listarContatos()
        {
            agendaEntities ctx = new agendaEntities();
            var consulta = ctx.contatos;
            dgDados.ItemsSource = consulta.ToList();
        }
        
        private void alterarBotoes (int opcao)
        {
            btAlterar.IsEnabled = false;
            btInserir.IsEnabled = false;
            btExcluir.IsEnabled = false;
            btCancelar.IsEnabled = false;
            btLocalizar.IsEnabled = false;
            btSalvar.IsEnabled = false;

            if (opcao == 1)
            {
                btInserir.IsEnabled = true;
                btLocalizar.IsEnabled = true;
            }
            if (opcao == 2)
            {
                btSalvar.IsEnabled = true;
                btCancelar.IsEnabled = true;
            }if (opcao == 3)
            {
                btAlterar.IsEnabled = true;
                btExcluir.IsEnabled = true;
            }
        }

        private void limparCampos()
        {
            txtID.IsEnabled = true;
            txtID.Clear();
            txtEmail.Clear();
            txtNome.Clear();
            txtTelefone.Clear();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            alterarBotoes(1);
            limparCampos();
        }

        private void btLocalizar_Click(object sender, RoutedEventArgs e)
        {
            if(txtID.Text.Trim().Count() > 0)
            {
                try
                {
                    int id = Convert.ToInt32(txtID.Text);
                    using (agendaEntities ctx = new agendaEntities())
                    {
                        contato c = ctx.contatos.Find(id);
                        dgDados.ItemsSource = new contato[1] { c };
                    }
                }
                catch { }
            }
            if(txtNome.Text.Trim().Count() > 0)
            {
                try
                {
                    using (agendaEntities ctx = new agendaEntities())
                    {
                        var consulta = from c in ctx.contatos
                                where c.nome.Contains(txtNome.Text)
                                select c;
                        dgDados.ItemsSource = consulta.ToList();
                    }
                }
                catch { }
            }
            if (txtEmail.Text.Trim().Count() > 0)
            {
                try
                {
                    using (agendaEntities ctx = new agendaEntities())
                    {
                        var consulta = from c in ctx.contatos
                                       where c.email.Contains(txtEmail.Text)
                                       select c;
                        dgDados.ItemsSource = consulta.ToList();
                    }
                }
                catch { }
            }
            if (txtTelefone.Text.Trim().Count() > 0)
            {
                try
                {
                    using (agendaEntities ctx = new agendaEntities())
                    {
                        var consulta = from c in ctx.contatos
                                       where c.telefone.Contains(txtTelefone.Text)
                                       select c;
                        dgDados.ItemsSource = consulta.ToList();
                    }
                }
                catch { }
            }
        }
    }
}