using HospitalAppointmentManagement.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped(sp => new AppointmentService(connectionString));

var app = builder.Build();

app.MapGet("/appointments", (AppointmentService service) =>
{
    return service.GetAllAppointments();
});

app.MapGet("/appointments/{id}", (int id, AppointmentService service) =>
{
    var app = service.GetAppointmentById(id);
    if (app == null)
        return Results.NotFound("Not found");
    return Results.Ok(app);
});

app.MapPost("/appointments", (string patientName, string doctorName, DateTime date, AppointmentService service) =>
{
    service.AddAppointment(patientName, doctorName, date);
    return Results.Ok("Added");
});

app.MapPut("/appointments/{id}", (int id, string patientName, string doctorName, string status, AppointmentService service) =>
{
    service.UpdateAppointment(id, patientName, doctorName, status);
    return Results.Ok("Updated");
});

app.MapDelete("/appointments/{id}", (int id, AppointmentService service) =>
{
    service.DeleteAppointment(id);
    return Results.Ok("Deleted");
});

app.Run();

