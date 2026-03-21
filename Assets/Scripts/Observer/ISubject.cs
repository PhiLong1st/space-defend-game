public interface ISubject<T> where T : class
{
  void AddObserver(IObserver<T> observer);
  void RemoveObserver(IObserver<T> observer);
  void NotifyObservers(T data);
}