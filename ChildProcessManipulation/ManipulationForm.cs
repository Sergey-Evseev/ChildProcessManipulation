using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//добавляем директивы using для нескольких пространств имен (библиотек):
using System.Diagnostics; //библиотека управления процессами
using System.Runtime.InteropServices; //библиотека для работы с функциями неупр.кода
using System.IO;
using System.Reflection; //доступ к типам библиотек (Assembly)
using System.Management; //доп.возможности управления процессами



namespace ChildProcessManipulation
{
    public partial class ManipulationForm : Form
    {
        //константа, идентифицирующая сообщение WM_SETTEXT
        const uint WM_SETTEXT = 0x0C;
        //импорт ф-ции SendMessage из биб. user32.dll
        [DllImport("user32.dll")] //атрибут импорта с входящим параметром с относ. именем

        //функция библииотеки, имя можно менять через entry point конструктора DllImport
        public static extern IntPtr SendMessage();

        
        public ManipulationForm()
        {
            InitializeComponent();
        }
        
    }
}
