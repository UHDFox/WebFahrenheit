namespace Application;

public abstract class TObject
{
    public TObject(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}