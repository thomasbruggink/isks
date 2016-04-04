using Autofac;
using Business.Repositories.Blog;

namespace Business
{
    /// <summary>
    /// This class is responsible for configuring and setting up all the repositories
    /// </summary>
    public class BusinessRepositoryContainer
    {
        private ContainerBuilder _containerBuilder;
        private IContainer _container;

        public static BusinessRepositoryContainer Instance { get; private set; }

        /// <summary>
        /// Create a new instance of the repositorycontainer
        /// </summary>
        public BusinessRepositoryContainer()
        {
            _containerBuilder = new ContainerBuilder();
            Instance = this;
        }

        /// <summary>
        /// Configure and setup the containerbuilder with all the repositories
        /// </summary>
        public void Setup()
        {
            _containerBuilder.RegisterType<BlogRepository>().AsImplementedInterfaces();

            _container = _containerBuilder.Build();
        }


        /// <summary>
        /// This will use the containerbuilder to update the already created controller
        /// Making sure newley added types are added to the container
        /// </summary>
        public void Update()
        {
            _containerBuilder.Update(_container);
        }

        /// <summary>
        /// Returns the container to be configured manually
        /// </summary>
        /// <returns></returns>
        public IContainer GetContainer()
        {
            return _container;
        }

        /// <summary>
        /// Overwrites a registered type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void OverwriteType<T>(bool singleInstance = false)
        {
            var containerBuilder = new ContainerBuilder();
            if (singleInstance)
                containerBuilder.RegisterType<T>().AsImplementedInterfaces().SingleInstance();
            else
                containerBuilder.RegisterType<T>().AsImplementedInterfaces();
            containerBuilder.Update(_container);
        }

        /// <summary>
        /// Returns the containerbuilder to manually add repositories
        /// </summary>
        /// <returns></returns>
        public ContainerBuilder GetContainerBuilder()
        {
            if (_container != null)
                _containerBuilder = new ContainerBuilder();
            return _containerBuilder;
        }

        /// <summary>
        /// Resolves a repository from the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return Instance._container.Resolve<T>();
        }
    }
}
