using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

public class MongoDBService
{
    private readonly IMongoCollection<Student> _studentsCollection;

    public MongoDBService(IOptions<MongoDBSetting> mongoDBSettings)
    {
        var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _studentsCollection = mongoDatabase.GetCollection<Student>(mongoDBSettings.Value.CollectionName);
    }

    public List<Student> GetStudents() => _studentsCollection.Find(student => true).ToList();

    public Student GetStudent(string id) => _studentsCollection.Find(student => student.Id == id).FirstOrDefault();

    public Student CreateStudent(Student student)
    {
        _studentsCollection.InsertOne(student);
        return student;
    }

    public void UpdateStudent(string id, Student student) => _studentsCollection.ReplaceOne(student => student.Id == id, student);

    public void DeleteStudent(string id) => _studentsCollection.DeleteOne(student => student.Id == id);
}
