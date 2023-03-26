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
        public ManipulationForm()
        {
            InitializeComponent();
            LoadSelectedAssemblies();
        }

        //константа, идентифицирующая сообщение WM_SETTEXT
        const uint WM_SETTEXT = 0x0C;
        //импорт ф-ции SendMessage из биб. user32.dll
        [DllImport("user32.dll")] //атрибут импорта с входящим параметром с относ. именем

        //функция библииотеки, имя можно менять через entry point конструктора DllImport
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, int uParam,
            [MarshalAs(UnmanagedType.LPStr)] string lParam);
        //Marshaling refers to the process of converting data from one format to another,
        //so that it can be passed between different execution environments.

        //список,  в котором будут храниться объекты, описывающие дочерние процессы приложения
        List<Process> Processes = new List<Process>();
        //счетчик запущенных процессов
        int counter = 0;
        
        //метод загружающий доступные исполняемые файлы из директории проекта
        void LoadSelectedAssemblies() 
        {
            //название файла сборки текущего запущенного приложения
            //-полное имя файла приложения. С помощью биб-ки System.IO
            string except = new FileInfo(Application.ExecutablePath).Name;
            
            //получаем название файла без расширения
            //(получаем подстроку из полного имени до точки)
            except = except.Substring(0, except.IndexOf("."));
            
            //получаем все exe файлы дом. директории запущенного процесса
            string[] files = Directory.GetFiles(Application.StartupPath, "*.exe");

            foreach (var file in files)//проходимся по получ. массиву
            {
                //получаем имя каждого файла массива
                string fileName = new FileInfo(file).Name;

                //если имя файла не содержит имени исполняемого файла проекта,
                //(новый, не относится к родительскому процессу)
                //то оно добавляется в список (SelectAssemblies - listbox гл.формы
                if (fileName.IndexOf(except) == -1)
                    SelectAssemblies.Items.Add(fileName);
            }
        }
        
        //метод, запускающий процесс на исполнение и сохраняющий объект, его описывающий
        void RunProcess(string AssemblyName)//принимает файл, который будет запускаться 
        {
            //запуск процесса на основании исполняемого файла 
            Process proc = Process.Start(AssemblyName);
            //добавляем запущенный процесс в список Processes объявленный ранее
            Processes.Add(proc);
            //проверяем, стал ли созданный процесс дочерним, по отношению к текущему,
            //и, если стал, выводим MessageBox
            if (Process.GetCurrentProcess().Id == GetParentProcessId(proc.Id))
            {


            }    









        }

    }
}
