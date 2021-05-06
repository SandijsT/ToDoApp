﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDo.Data;

namespace ToDo.Data.Migartions
{
    [DbContext(typeof(TaskContext))]
    partial class TaskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ToDo.Models.Entities.ATask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LabelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TaskListId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UrgentTaskListId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LabelId");

                    b.HasIndex("TaskListId");

                    b.HasIndex("UrgentTaskListId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ToDo.Models.Entities.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Color")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("ToDo.Models.Entities.TaskList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ListsUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ListsUserId");

                    b.ToTable("TaskLists");
                });

            modelBuilder.Entity("ToDo.Models.Entities.UrgentTaskList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UrgentTaskLists");
                });

            modelBuilder.Entity("ToDo.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<int?>("UrgentTasksId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UrgentTasksId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDo.Models.Entities.ATask", b =>
                {
                    b.HasOne("ToDo.Models.Entities.Label", "Label")
                        .WithMany()
                        .HasForeignKey("LabelId");

                    b.HasOne("ToDo.Models.Entities.TaskList", "TaskList")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskListId");

                    b.HasOne("ToDo.Models.Entities.UrgentTaskList", null)
                        .WithMany("Tasks")
                        .HasForeignKey("UrgentTaskListId");

                    b.Navigation("Label");

                    b.Navigation("TaskList");
                });

            modelBuilder.Entity("ToDo.Models.Entities.Label", b =>
                {
                    b.HasOne("ToDo.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDo.Models.Entities.TaskList", b =>
                {
                    b.HasOne("ToDo.Models.Entities.User", "ListsUser")
                        .WithMany()
                        .HasForeignKey("ListsUserId");

                    b.Navigation("ListsUser");
                });

            modelBuilder.Entity("ToDo.Models.Entities.User", b =>
                {
                    b.HasOne("ToDo.Models.Entities.UrgentTaskList", "UrgentTasks")
                        .WithMany()
                        .HasForeignKey("UrgentTasksId");

                    b.Navigation("UrgentTasks");
                });

            modelBuilder.Entity("ToDo.Models.Entities.TaskList", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ToDo.Models.Entities.UrgentTaskList", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
