namespace AppMVCBasica.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected  Entity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
