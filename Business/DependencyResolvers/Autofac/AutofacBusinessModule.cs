using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnnouncementManager>().As<IAnnouncementService>().SingleInstance();
            builder.RegisterType<EfAnnouncementDal>().As<IAnnouncementDal>().SingleInstance();

            builder.RegisterType<BranchManager>().As<IBranchService>().SingleInstance();
            builder.RegisterType<EfBranchDal>().As<IBranchDal>().SingleInstance();

            builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();
            builder.RegisterType<EfCommentDal>().As<ICommentDal>().SingleInstance();

            builder.RegisterType<CreaterManager>().As<ICreaterService>().SingleInstance();
            builder.RegisterType<EfCreaterDal>().As<ICreaterDal>().SingleInstance();

            builder.RegisterType<MessageManager>().As<IMessageService>().SingleInstance();
            builder.RegisterType<EfMessageDal>().As<IMessageDal>().SingleInstance();

            builder.RegisterType<RoadMapOfStepManager>().As<IRoadMapOfStepService>().SingleInstance();
            builder.RegisterType<EfRoadMapOfStepDal>().As<IRoadMapOfStepDal>().SingleInstance();

            builder.RegisterType<RoadMapManager>().As<IRoadMapService>().SingleInstance();
            builder.RegisterType<EfRoadMapDal>().As<IRoadMapDal>().SingleInstance();

            builder.RegisterType<SourceManager>().As<ISourceService>().SingleInstance();
            builder.RegisterType<EfSourceDal>().As<ISourceDal>().SingleInstance();

            builder.RegisterType<StepManager>().As<IStepService>().SingleInstance();
            builder.RegisterType<EfStepDal>().As<IStepDal>().SingleInstance();

            builder.RegisterType<StepOfBranchManager>().As<IStepOfBranchService>().SingleInstance();
            builder.RegisterType<EfStepOfBranchDal>().As<IStepOfBranchDal>().SingleInstance();

            //Aspects Load
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
