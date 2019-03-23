using CodeFirst.DAL.ORM.Context;
using CodeFirst.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProjectContext db = new ProjectContext();
        public void TextBoxEraser()
        {
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";

                }
            }

            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";

                }
            }

            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";

                }
            }
        }

        public void AppUserTakeList()
        {
            dataGridView1.DataSource = db.AppUsers.Where(x => x.Status == DAL.ORM.Enum.Status.Active ||
            x.Status == DAL.ORM.Enum.Status.Update).ToList();

        }
        
        
        int id;

        private void Form1_Load(object sender, EventArgs e)
        {
            AppUserTakeList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUpdateName.Text = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
            txtUpdateLastName.Text = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
            txtUpdatePhoneNumber.Text = dataGridView1.CurrentRow.Cells["PhoneNumber"].Value.ToString();
            id = int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            txtUserID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
        }
        public void UserFind()
        {
            dataGridView1.DataSource = db.AppUsers.ToList().Where(x=>x.FullName.ToLower().Contains
              (txtFullName.Text.ToLower()) && x.Status != DAL.ORM.Enum.Status.Delete).OrderBy(x => x.FullName).Select(y => new
              {
                  y.FullName,
                  y.ID,
                  y.FirstName,
                  y.LastName,
                  y.PhoneNumber,
                  y.AddDate,
                  y.UpdateDate,
                  y.DeleteDate,
                  y.Status

              }).ToList();
        }

       
       
        private void btnAdd_Click_1(object sender, EventArgs e)
        {

            AppUser appuser = new AppUser();
            appuser.FirstName = txtFirstName.Text;
            appuser.LastName = txtLastName.Text;
            appuser.PhoneNumber = txtPhoneNumber.Text;
            db.AppUsers.Add(appuser);
            db.SaveChanges();

            AppUserTakeList();
            TextBoxEraser();

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            AppUser appuser = db.AppUsers.FirstOrDefault(x => x.ID == id);
            appuser.FirstName = txtUpdateName.Text;
            appuser.LastName = txtUpdateLastName.Text;
            appuser.PhoneNumber = txtUpdatePhoneNumber.Text;
            appuser.Status = DAL.ORM.Enum.Status.Update;

            db.SaveChanges();
            AppUserTakeList();
            TextBoxEraser();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AppUser appuser = db.AppUsers.FirstOrDefault(x => x.ID == id);
            appuser.Status = DAL.ORM.Enum.Status.Delete;
            db.SaveChanges();

            TextBoxEraser();
            AppUserTakeList();
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            UserFind();

        }
    }
}
