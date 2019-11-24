using System.Collections.Generic;
public interface IPooler
{
    void SetPoolables(IPoolable poolable);
    IPoolable GetPoolable();
}