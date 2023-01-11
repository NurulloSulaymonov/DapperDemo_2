using Domain.Entites;
using Infrastructure.Services;

var studentService = new StudentService();
Update();
Show();


void Add()
{
    var st = new Student()
    {
        Phone = "13344444",
        Email = "st@gmail.com",
        FirstName = "Ardasher",
        LastName = "Sattori",
    };
    studentService.AddStudent(st);
}


void Update()
{
    var st = new Student()
    {
        Phone = "13344444",
        Email = "st@gmail.com",
        FirstName = "Abdullah",
        Id = 3,
        LastName = "Sheralizoda"
    };
   var result =  studentService.UpdateStudent(st);
    if (result) Console.WriteLine("Updated");
    else Console.WriteLine("not found");
}

void Delete(int id)
{
    studentService.DeleteStudent(id);
}

void Show()
{
    var allStudents = studentService.GetStudents();
    Console.WriteLine("Id        firstname     lastname   email    phone");
    foreach (var st in allStudents)
    {
        System.Console.WriteLine($"{st.Id}     {st.FirstName}        {st.LastName}     {st.Email}     {st.Phone}");
    }
}
