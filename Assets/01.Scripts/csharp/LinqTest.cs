using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class LinqTest : MonoBehaviour
{
    private void Start()
    {
        List<Student> students = new List<Student>()
        {
            new Student() { Name = "허정범", Age = 28, Gender = "남" },
            new Student() { Name = "박수현", Age = 26, Gender = "여" },
            new Student() { Name = "박진혁", Age = 29, Gender = "남" },
            new Student() { Name = "이상진", Age = 28, Gender = "남" },
            new Student() { Name = "서민주", Age = 25, Gender = "여" },
            new Student() { Name = "전태준", Age = 27, Gender = "남" },
            new Student() { Name = "박순홍", Age = 27, Gender = "남" },
            new Student() { Name = "양성일", Age = 29, Gender = "남" },
        };

        // var mans = from student in students where student.Gender == "남" select student;
        // var girls = from student in students
        //     where student.Gender == "여" 
        //     orderby student.Age descending
        //     select student;
        var all = students.Where((student) => true);
        var girls = students.Where((student) => student.Gender == "여").OrderBy(student => student.Age);

        foreach (var student in all)
        {
            Debug.Log(student);
        }

        foreach (var girl in girls)
        {
            Debug.Log(girl);
        }

        int manCount = students.Count(student => student.Gender == "남");
        Debug.Log(manCount);
    }
}