namespace FoodWriting.Domain.Interfaces;

public interface IUnityOfWork
{
    Task Commit();
}