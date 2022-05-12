using Autofac;
using StudentManagementV2.Business.Services;
using StudentManagementV2.Business.Validators;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementV2.Business.BusinessModules
{
    public class BusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(DepartmentService))
                .As(typeof(IDepartmentService))
                .As(typeof(IService<Department, string>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(AssignClassService))
                .As(typeof(IAssignClassService))
                .As(typeof(IService<AssignClass, string>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(StudentService))
                .As(typeof(IStudentService))
                .As(typeof(IService<Student, string>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(RoleService))
                .As(typeof(IRoleService))
                .As(typeof(IService<Role, string>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(SemesterService))
                .As(typeof(ISemesterService))
                .As(typeof(IService<Semester, string>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(SubjectService))
                .As(typeof(ISubjectService))
                .As(typeof(IService<Subject, string>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(SubjectRegisterService))
                .As(typeof(ISubjectRegisterService))
                .As(typeof(IService<SubjectRegister, object>))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType(typeof(CommonValidator))
                .As(typeof(ICommonValidator))
                .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
