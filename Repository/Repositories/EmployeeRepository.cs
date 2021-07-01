using System;
using System.Collections.Generic;
using System.Data;
using Core.Abstractions.Repositories;
using Core.Entities;
using Dapper.Contrib.Extensions;

namespace Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public EmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<Employee> Retrieve()
        {
            IEnumerable<Employee> employees;

            _connection.Open();
            employees = _connection.GetAll<Employee>();
            _connection.Close();

            return employees;
        }

        public Employee Retrieve(int id)
        {
            Employee employee = new Employee();

            _connection.Open();
            employee = _connection.Get<Employee>(id);
            _connection.Close();

            return employee;
        }

        public void Create(Employee employee)
        {
            _connection.Open();
            using (_transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Insert<Employee>(employee);
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }

        public void Update(Employee employee)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Update<Employee>(employee);
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Delete<Employee>(new Employee { Id = id });
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }
    }
}