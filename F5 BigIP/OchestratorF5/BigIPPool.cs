using Opalis.QuickIntegrationKit;

namespace OchestratorF5
{
    [OpalisData("F5 Pool")]
    public class F5Pool
    {
        private string name = string.Empty;

        internal F5Pool(string poolName)
        {
            this.name = poolName;
        }

        [OpalisOutput(Description = "Pool Names"), OpalisFilter]
        public string PoolName
        {
            get { return name; }
        }
        
    }
}
