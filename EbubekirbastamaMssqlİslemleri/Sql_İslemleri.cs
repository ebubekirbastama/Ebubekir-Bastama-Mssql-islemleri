using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EbubekirbastamaMssqlİslemleri
{
    public class Sql_İslemleri
    {
        //*İletişim ve Eğitim Adreslerimiz..*//
        //*---------------------------------*//
        //Contact : +90 5554128854;
        //Whatshapp Contact : +90 5554128854;
        //Email Contact : ebubekirbastama@elmalicesmekuruyemis.com
        //Website Contact : https://www.ebubekirbastama.com
        //Education https://www.youtube.com/user/yazilimegitim
        //Education https://www.youtube.com/channel/UC240A7DHqgAR8bEMakGdqWg/videos?view_as=subscriber

        #region Değişkenler
        private static string svrname { get; set; }
        private static string dbname { get; set; }
        private static SqlCommand kmt { get; set; }
        private static SqlConnection con { get; set; }
        private static SqlDataReader reader { get; set; }
        private static SqlDataAdapter adapter { get; set; }
        private static ArrayList aray { get; set; }
        private static DataTable datatable;
        private static DataSet dataset;
        #endregion

        #region Metodlar
        public SqlConnection Connect()
        {
            con = new SqlConnection(@"Server=" + svrname + "; Integrated Security=true; Database=" + dbname + "");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public SqlConnection DisConnect()
        {
            con = new SqlConnection(@"Server=" + svrname + "; Integrated Security=true; Database=" + dbname + "");

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
        public int Command_İnsert_Update_Delete(string Kmt)
        {
            kmt = new SqlCommand(Kmt, Connect());
            int returndgr = kmt.ExecuteNonQuery();
            DisConnect();
            return returndgr;
        }
        public ArrayList Reader(string Kmt, params string[] Dgr)
        {
            aray = new ArrayList();
            kmt = new SqlCommand(Kmt, Connect());
            reader = kmt.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < Dgr.Length; i++)
                {
                    aray.Add(reader[Dgr[i].ToString()].ToString());
                }
            }
            return aray;
        }
        public DataTable Tablo(string Kmt, DataGridView rapordt)
        {
            adapter = new SqlDataAdapter(Kmt, Connect());
            datatable = new DataTable();
            try
            {
                adapter.Fill(datatable);
                rapordt.DataSource = datatable;
                GC.Collect(); //Çöp Toplayıcısı
                GC.WaitForPendingFinalizers();  //Çöp Yakıcısı
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            adapter.Dispose();
            DisConnect();
            return datatable;
        }
        public DataSet GetDataSet(string Kmt)
        {
            adapter = new SqlDataAdapter(Kmt, Connect());
            dataset = new DataSet();      
            try
            {
                adapter.Fill(dataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu...!\nHatanız: " + ex.Message);
            }
            adapter.Dispose();
            DisConnect();
            return dataset;
        }
        #endregion
    }
}