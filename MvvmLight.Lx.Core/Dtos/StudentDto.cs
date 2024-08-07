using CommunityToolkit.Mvvm.ComponentModel;
using MvvmLight.Lx.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Dtos
{
    public class StudentDto : ObservableObject
    {
        private readonly Student student;

        public StudentDto(Student student) => this.student = student;

        public string Name
        {
            get => student.Name;
            set => SetProperty(student.Name, value, student, (u, n) => u.Name = n);
        }

        public int Id
        {
            get => student.Id;
            set => SetProperty(student.Id, value, student, (u, n) => u.Id = n);
        }
    }
}
