﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku_Projesi
{
    public partial class Form1 : Form
    {
        public static Form1 frm;
        kontrolEt kontroll;
        private int saniye = 0, dk = 0, saat = 0;
        public static Boolean dogrumu = true;
        sqlBaglanti b = new sqlBaglanti();
        public Form1()
        {
            InitializeComponent();
            frm = this;
        }
        private int[,] matris = new int[9, 9];
        Boolean kontroleKontrol = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db_sudokuDataSet.kistatistik' table. You can move, or remove it, as needed.
            this.kistatistikTableAdapter.Fill(this.db_sudokuDataSet.kistatistik);
            this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Ascending);
            b.sc.Open();            
            baslangicDegerleri baslangic = new baslangicDegerleri();
            kullanici();
            label1.Text = "Geçen süre: " + saat.ToString() + ":" + dk.ToString() + ":" + saniye.ToString();
            timer1.Interval = 1000;
            timer1.Start();
        }
        public void kullanici()
        {
            label2.Text = uye_girisi.k_adi;
            if (uye_girisi.k_adi == "")
            {
                linkLabel4.Text = "Lütfen giriş yapınız!";
            }
        }
        private void matriseAl()
        {
            try
            {
                for (int i = 1; i <= 9; i++)
                    for (int j = 1; j <= 9; j++)
                        matris[i - 1, j - 1] = int.Parse(((TextBox)this.Controls["textbox" + i + j]).Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen tüm alanları doldurup yeniden deneyiniz.");
                kontroleKontrol = true;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            matriseAl();
            if (kontroleKontrol == false)
            {
                kontroll = new kontrolEt(matris);
                MessageBox.Show(kontroll.text);
            }
            else
                kontroleKontrol = false;
            if (dogrumu == false && label2.Text != "")
            {
                MessageBox.Show("Tebrikler, sonuç doğru!");
                b.komut.CommandText = "INSERT INTO kistatistik(k_adi,skor) VALUES ('" + Convert.ToString(uye_girisi.k_adi) + "','" + saat.ToString() + ":" + dk.ToString() + ":" + saniye.ToString() + "')";
                b.dr = b.komut.ExecuteReader();
                b.dr.Read();
            }
            else
                dogrumu = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            main.mai.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            baslangicDegerleri b = new baslangicDegerleri();
            saat = 0;
            dk = 0;
            saniye = 0;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void timer1_Tick_3(object sender, EventArgs e)
        {
            saniye++;
            label1.Text = "SÜRE: " + saat.ToString() + ":" + dk.ToString() + ":" + saniye.ToString();
            if (saniye == 60)
            {
                saniye = 0;
                dk++;
            }
            if (dk == 60)
            {
                saat++;
                dk = 0;
            }
            kullanici();
        }

        private void linkLabel4_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label2.Text != "")
            {
                linkLabel4.Text = "Lütfen giriş yapınız!";
                label2.Text = "";
                uye_girisi.k_adi = "";
                MessageBox.Show("Başarıyla çıkış yaptınız");
            }
            else
            {
                uye_girisi uye_ekrani = new uye_girisi();
                uye_ekrani.Show();
                uye_ekrani.groupBox1.BringToFront();
                uye_ekrani.groupBox2.SendToBack();
                kullanici();
                linkLabel4.Text = "Çıkış Yap";
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            baslangicDegerleri baslan = new baslangicDegerleri();
            saniye = 0;
            dk = 0;
            saat = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            main.mai.BringToFront();
        }
        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

        }

    }

}
