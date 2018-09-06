using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XactTodo.Models
{
    public class Matter : INotifyPropertyChanged
    {
        public Matter():this("")
        {
        }

        public Matter(string subject)
        {
            Subject = subject;
            CreationTime = DateTime.Now;
        }

        private string subject;
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                NotifyPropertyChanged(nameof(Subject));
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
