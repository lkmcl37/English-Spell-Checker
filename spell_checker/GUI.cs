using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SqlLiteHelper;

namespace Spell_Checker
{
    public partial class User_Interface : Form
    {
        public User_Interface()
        {
            InitializeComponent();
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            //StringCompute stringcompute1 = new StringCompute();
            //stringcompute1.SpeedyCompute(txt_1.Text, txt_2.Text);    // compute similarity rate
            //decimal rate = stringcompute1.ComputeResult.Rate;
            //lbl_similarity.Text = string.Format("Similarity rate：{0}", rate);

            StringCompute stringcompute1 = new StringCompute();
            var dt = Sql.ExecuteSelectSqlDataTable("select ID,word,explain,ping,difficult,pronunciation from wordlist");
            decimal max = 0;
            int length = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stringcompute1.SpeedyCompute(txt_1.Text, dt.Rows[i]["word"].ToString()); 
                decimal rate = stringcompute1.ComputeResult.Rate;
                if (rate == 1)
                {
                    length = i;
                    max = Convert.ToDecimal("1.000");
                    break;
                }
                else
                {

                    if (max < rate)
                    { max = rate; length = i; }

                }
            }
            lbl_similarity.Text = string.Format("Similarity rate：{0} .Spelling Suggestion：{1}", max.ToString().Substring(0, max.ToString().Length >= 4 ? 4 : max.ToString().Length), dt.Rows[length]["word"].ToString());


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Sql.CreateDataBase();
        }

        private void btn_importword_Click(object sender, EventArgs e)
        {
            var dt = DAL.SqlDbHelper.ExecuteSelectSqlDataTable("select ID,word,explain,ping,difficult,pronunciation from word ");
            string sql = "insert into world(ID,word,explain,ping,difficult,pronunciation) values(@ID,@word,@explain,@ping,@difficult,@pronunciation)";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<Paramete> parame = new List<Paramete>();
                parame.Add(new Paramete("ID", dt.Rows[i][0]));
                parame.Add(new Paramete("word", dt.Rows[i][1]));
                parame.Add(new Paramete("explain", dt.Rows[i][2]));
                parame.Add(new Paramete("ping", dt.Rows[i][3]));
                parame.Add(new Paramete("difficult", dt.Rows[i][4]));
                parame.Add(new Paramete("pronunciation", dt.Rows[i][5]));
                Sql.ExecuteNonQuery(sql, parame.ToArray());
            }
            MessageBox.Show("Done！");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
