namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatform : IFreelancePlatform
    {
        public IJobManager JobManager { get; }

        public FreelancePlatform(IJobManager jobManager)
        {
            this.JobManager = jobManager;
        }
    }
}
