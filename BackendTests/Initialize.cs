namespace BackendTests;

public class Initialize
{
    public HogwartsContext HogwartsContext; 
    public ISession session = new MockHttpSession();
    public Initialize()
    {
        var options = new DbContextOptionsBuilder<HogwartsContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HogwartsTest;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
        this.HogwartsContext = new HogwartsContext(options);
        DbInitializer.Initialize(HogwartsContext);

        SessionHelper.SetObjectAsJson(session, "username", "tester");
    }

}