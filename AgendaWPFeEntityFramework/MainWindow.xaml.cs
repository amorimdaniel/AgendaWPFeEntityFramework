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

            using (agendaEntities ctx = new agendaEntities())
            {
                ctx.contatos.Add(contato);
                ctx.SaveChanges();
            }
        }
    }
}