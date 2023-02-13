using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ef_crudIslemleri_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static void listBoxEkle(ListBox lstbox,Pizza pizza)
        {
            lstbox.Items.Add(pizza.adı+" "+pizza.fiyati);
        }
        static List<Pizza> pizzalariGetir()
        {
            PizzaDBEntities veritabani = new PizzaDBEntities();
            return veritabani.Pizzas.ToList();
        }
        static void listBoxDoldur(List<Pizza> pizzalar,ListBox listBox)
        {
            foreach (var item in pizzalar)
            {
                listBox.Items.Add(item.adı + " " + item.fiyati);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            PizzaDBEntities veritabani = new PizzaDBEntities();
            Pizza pizza = new Pizza();
            pizza.adı = txtAd.Text;
            pizza.fiyati = Convert.ToDecimal(txtFiyat.Text);
            pizza.hamurTipi = txtHamur.Text;
            pizza.boyutu = txtBuyukluk.Text;
            pizza.Malzemeler = txtMalzemeler.Text;
            pizza.domatesSosluMu = Convert.ToBoolean(txtDomatesSosu.Text);

            veritabani.Pizzas.Add(pizza);
            veritabani.SaveChanges();
            listBoxEkle(listbox1,pizza);
            pizzalariGetir();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBoxDoldur(pizzalariGetir(), listbox1);
        }

        Pizza arananBilgi;
        PizzaDBEntities veritabani = new PizzaDBEntities();

        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAd.Enabled = false;
            string secilen=listbox1.SelectedItem.ToString();
            string pizzaAdi=secilen.Split(' ')[0];
            
            PizzaDBEntities veritabani = new PizzaDBEntities();
            arananBilgi=veritabani.Pizzas.Where(p=> p.adı==pizzaAdi).FirstOrDefault();
            txtAd.Text = arananBilgi.adı;
            txtHamur.Text = arananBilgi.hamurTipi;
            txtBuyukluk.Text = arananBilgi.boyutu;
            txtDomatesSosu.Text = arananBilgi.domatesSosluMu.ToString();
            txtFiyat.Text = arananBilgi.fiyati.ToString();
            txtMalzemeler.Text = arananBilgi.Malzemeler;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Pizza pizza = veritabani.Pizzas.Where(p => p.adı == arananBilgi.adı).FirstOrDefault();

            pizza.adı = txtAd.Text;
            pizza.fiyati = Convert.ToDecimal(txtFiyat.Text);
            pizza.boyutu = txtBuyukluk.Text;
            pizza.domatesSosluMu = Convert.ToBoolean(txtDomatesSosu.Text);
            pizza.hamurTipi = txtHamur.Text;
            pizza.Malzemeler = txtMalzemeler.Text;
            veritabani.SaveChanges();
            MessageBox.Show("güncellendi :)");
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            Pizza pizza = veritabani.Pizzas.Where(p => p.adı == arananBilgi.adı).FirstOrDefault();
            veritabani.Pizzas.Remove(pizza);
            veritabani.SaveChanges();
            MessageBox.Show("Silindi :");
        }
    }
}
