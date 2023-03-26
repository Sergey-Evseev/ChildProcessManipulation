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
    //делегат для метода вызова процесса по имени, декларирует принятие процесса
    delegate void ProcessDelegate(Process proc);
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
                MessageBox.Show(proc.ProcessName + " действительно дочерний процесс" +
                    " текущего процесса");
            }
            
            //указываем, что процесс должен генерить события
            proc.EnableRaisingEvents = true;
            
            //добавляем обработчик на события завершения процесса
            proc.Exited += (sender, e) =>//запись метода обработчика через лямбда-выражение
            {
                //типизирование параметра sender как объект Process (cast to a Process)
                var pr = sender as Process;
                //на событие убираем процесс из списка запущенных приложений
                StartedAssemblies.Items.Remove(pr.ProcessName);
                //и добавляем процесс в список доступных приложений (возвращаем в 1-й листобокс)
                SelectAssemblies.Items.Add(pr.ProcessName);
                //уменьшаем счетчик дочерних процессов на 1
                counter--;
                int index = 0;

                //меняем текст для главных окон всех дочерних процессов
                foreach (var r in Processes) //по списку дочерних процессов
                    SetChildWindowText(r.MainWindowHandle, 
                        "Child process =" + (++index));
            }; //end of proc.Exited += (sender, e)


            
            //**********этот блок возможно не нужен *******************************
            //устанавливаем новый текст главному окну дочернего процесса
            SetChildWindowText(proc.MainWindowHandle, "Child process =" + (++counter));
            //проверяем запускали ли мы экземпляр такого приложения и,
            //если нет, то добавляем в список запущенных приложений
            if (!StartedAssemblies.Items.Contains(proc.ProcessName))
                StartedAssemblies.Items.Add(proc.ProcessName);

            //убираем приложение из списка доступых приложений
            SelectAssemblies.Items.Remove(SelectAssemblies.SelectedItem);
            //***********этот блок выше возможно не нужен *******************************



        }//=============end of void RunProcess(string AssemblyName)=============


        //метод проходящий по всем дочерним процессам с заданным переданным именем,
        //и выполняющий для этих процессов заданный делегатом метод
        void ExecuteOnProcessesByName(string ProcessName, ProcessDelegate act)
        {
            //получаем в массиве список запущенных в ОС процессов
            Process[] processes = Process.GetProcessesByName(ProcessName);

            //проходимся по каждому найденному процессу
            foreach (var r in processes)
            //если PID родительского процесса равен PID текущего процесса
            if (Process.GetCurrentProcess().Id == GetParentProcessId(r.Id))
            {
                act(r); //то запускаем метод
            }

        }

        
        //метод обертывания для отправки сообщения
        void SetChildWindowText(IntPtr Handle, string text)
        {
            SendMessage(Handle, WM_SETTEXT, 0, text); //WM_SETTEXT константа
                                                      //что сообщение будет менять заголовок окна
        }

        
        //--------------------------------
        //вспомогательный метод, получающий (интовый) ID родительского процесса
        //от переданного ему в качестве аргумента процесса
        int GetParentProcessId(int Id)
        {
            int parentId = 0;
            //параметр "win32_process.handle" - получ.доступа к процессам объектов из ОС
            //здесь формируется запрос к управляемому объекту
            using (ManagementObject obj = new ManagementObject("win32_process.handle=" +
                Id.ToString()))
            {
                obj.Get();
                //по имени процесса через внутренний индексатор получаем его номер
                parentId = Convert.ToInt32(obj["ParentProcessId"]);
            }
            return parentId; //и возвращаем его номер
                             //если родительский процесс не найден вернется 0
        }//-------------------------------


        //Кнопка Start запускает процесс, который мы выбрали из списка 
        private void StartButton_Click(object sender, EventArgs e)
        {
            RunProcess(SelectAssemblies.SelectedItem.ToString()); ;
        }

        //метод завершения процесса
        void Kill(Process p) => p.Kill();

        //обработчик события кнопки Stop
        private void StopButton_Click(object sender, EventArgs e)
        {
            ExecuteOnProcessesByName(StartedAssemblies).
                SelectedItem.ToString(), Kill);
        }
    }//end of public partial class ManipulationForm : Form
}
