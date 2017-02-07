using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegistryWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSN.Text))
            {
                MessageBox.Show(@"请输入需要注册的SN号");
                return;
            }

            RegeditItem();
            if (IsRegeditKeyExit())
            {
                MessageBox.Show(@"注册成功");
            }
            else
            {
                MessageBox.Show(@"注册失败");
            }

        }
        //判断是否注册成功
        private bool IsRegeditKeyExit()
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE\\AwareTec\\AwareTag");
            if (software!=null)
            {
                subkeyNames = software.GetValueNames();
                //取得该项下所有键值的名称的序列，并传递给预定的数组中
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == "MachineSN") //判断键值的名称
                    {
                        hkml.Close();
                        return true;
                    }
                }
                hkml.Close();
                return false;
            }
            else
            {
                return false;
            }
        }

        //注册表注册
        private void RegeditItem()
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey software = key.CreateSubKey("SOFTWARE\\AwareTec\\AwareTag");
            RegistryKey software1 = key.OpenSubKey("SOFTWARE\\AwareTec\\AwareTag", true); //该项必须已存在
            software1.SetValue("MachineSN", txtSN.Text);
            key.Close();
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')||e.KeyChar==(Char)8)
            {

            }
            else
            {
                e.Handled = true;
                MessageBox.Show(@"SN号只能为英文或数字");
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            RegistryKey Key;
            Key = Registry.LocalMachine;
            var myreg = Key.OpenSubKey("SOFTWARE\\AwareTec\\AwareTag");
            if (myreg == null)
            {
                MessageBox.Show(@"未注册");
            }
            else
            {
                var regValue = myreg.GetValue("MachineSN");
                if (regValue == null)
                {
                    myreg.Close();
                    MessageBox.Show(@"未注册");
                }
                else
                {
                    MessageBox.Show(myreg.GetValue("MachineSN").ToString());
                  
                }
            }
        }




    }
}
