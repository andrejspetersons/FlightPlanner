namespace FlightPlanner.Services
{
    public static class SharedLock
    {
        public static readonly object LockObject = new object();
    }
}
