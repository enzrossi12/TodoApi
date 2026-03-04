using TodoApi.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Services;

public class EfTaskService : ITaskService{

    private readonly AppDbContext _db;

    public EfTaskService(AppDbContext db){
        _db = db;
    }

}