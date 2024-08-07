﻿using System.Web.Helpers;
using CourseCalendarApp.Models;
using CourseCalendarApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Stylet;
using StyletIoC;
using Syncfusion.Licensing;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace CourseCalendarApp;

public class Bootstrapper : Bootstrapper<MainWindowViewModel>
{
    private IStyletIoCBuilder _builder;

    public Bootstrapper()
    {
        SyncfusionLicenseProvider.RegisterLicense(
            "Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxcc3RRQ2NYWExwVkQ=");
    }

    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        builder.Bind<DatabaseContext>().ToFactory(_ =>
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseLazyLoadingProxies()
               .UseSqlite("Data Source=course_calendar.db")
               .Options;

            var db = new DatabaseContext(options);

            db.Database.EnsureCreated();

            if (db.Users.Any()) return db;

            db.Users.Add(new User
            {
                CanvasToken = "9822~oENjkeVuxfNMRP3kdsJtQXRRPhNeyCKWkvL9VMCwaRaFM8RHzepHGQi1FsEWJhqc",
                Email = "202110577@fit.edu.ph",
                Name = "Admin",
                AccessType = "Admin",
                Username = "admin",
                Password = Crypto.HashPassword("password"),
                StudentId = 202110577,
                PortalPassword = "2002-01-07"
            });

            db.SaveChanges();

            return db;
        });

        builder.Bind<IThemeService>().To<ThemeService>().InSingletonScope();
        builder.Bind<ISnackbarService>().To<SnackbarService>().InSingletonScope();
        builder.Bind<IDialogService>().To<DialogService>().InSingletonScope();
        builder.Bind<IStyletIoCBuilder>().ToInstance(builder);

        _builder = builder;
    }
}