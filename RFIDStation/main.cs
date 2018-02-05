using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RFIDStation
{
    public partial class main : Form
    {
        private int serialDevice;                   //�����豸
        private int hHR2000DLLModule;               //dll���
        private System.Timers.Timer readerTimer;    //��ʱ��
        private bool bOperatingSerial;              //�Ƿ����ڲ�������
        private bool bSerialOpen;                   //�����Ƿ񱻴�

        public main()
        {
            InitializeComponent();

            hHR2000DLLModule = 0;
            hHR2000DLLModule = HR2000Dll.LoadLibrary("HR2000.dll");

            if (hHR2000DLLModule <= 0)
            {
                MessageBox.Show("װ��HR2000.dll�ļ�ʧ�ܣ���ȷ��HR2000.dll�ļ��Ƿ����");
                System.Environment.Exit(0);
            }

            serialDevice = -1;
            bSerialOpen = false;
            bOperatingSerial = false;

            this.comboBoxBaudrate.SelectedIndex = 1;
            this.comboBoxComPort.SelectedIndex = 0;
            this.comboBoxTool.SelectedIndex = 0;
            this.comboBoxTagOp.SelectedIndex = 0;
            this.comboBoxDeviceType.SelectedIndex = 0;



            //��ʼ����ʱ����
//             readerTimer = new System.Timers.Timer(300);//ʵ����Timer�࣬���ü��ʱ��Ϊ100���룻
//             readerTimer.Elapsed += new System.Timers.ElapsedEventHandler(readerTimerOut); //����ʱ���ʱ��ִ���¼��� 
//             readerTimer.AutoReset = true;  //������ִ��һ�Σ�false������һֱִ��(true)�� 
        }

        private void buttonOpenSerial_Click(object sender, EventArgs e)
        {
            if (serialDevice <= 0)
            {
                serialDevice = HR2000Dll.openPort(this.comboBoxComPort.SelectedItem.ToString(), this.comboBoxBaudrate.SelectedItem.ToString());
                if (serialDevice > 0)
                {
                    this.buttonOpenSerial.Text = "�ر�";
                    this.comboBoxComPort.Enabled = false;
                    this.comboBoxBaudrate.Enabled = false;

                    //������ʱ��
                    //readerTimer.Enabled = true;     //�Ƿ�ִ��System.Timers.Timer.Elapsed�¼��� 
                }
                else
                {
                    MessageBox.Show("�򿪴���ʧ��", "��ʾ");
                }
            }
            else
            {
                int op = HR2000Dll.closePort(serialDevice);
                if (op >= 0)
                {
                    this.buttonOpenSerial.Text = "��";
                    this.comboBoxComPort.Enabled = true;
                    this.comboBoxBaudrate.Enabled = true;

                    //�رն�ʱ��
                    //readerTimer.Enabled = false;     //�Ƿ�ִ��System.Timers.Timer.Elapsed�¼��� 
                }
                serialDevice = -1;
            }
        }


    }
}