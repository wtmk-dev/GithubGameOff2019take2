public interface IPoolable
{
    void Init(GameObjectPooler goPooler);
    void Dress(int id);
    void Act();
}